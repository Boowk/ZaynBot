using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Comandos.Exibir
{
    public class StatusComando : BaseCommandModule
    {
        [Command("status")]
        [Description("Exibe os status do seu personagem.")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task StatusComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.GetPersonagem(ctx, out UsuarioRPG usuario, false);
            if (usuario == null)
            {
                usuario = new UsuarioRPG(ctx.User.Id);
                ModuloBanco.UsuarioColecao.InsertOne(usuario);
                try
                {
                    DiscordGuild MundoZayn = await ModuloCliente.Client.GetGuildAsync(420044060720627712);
                    DiscordChannel CanalRPG = MundoZayn.GetChannel(519176927265947689);
                    await CanalRPG.SendMessageAsync($"*{ctx.User.Username} nasceu como `{usuario.Personagem.Raca.Nome.PrimeiraLetraMaiuscula()}`.*");
                }
                catch { }
            }

            PersonagemRPG personagem = usuario.Personagem;
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Status", ctx);
            embed.WithColor(DiscordColor.PhthaloGreen);
            DiscordEmoji pv = DiscordEmoji.FromGuildEmote(ModuloCliente.Client, 631907691467636736);
            DiscordEmoji pp = DiscordEmoji.FromGuildEmote(ModuloCliente.Client, 631907691425562674);
            DiscordEmoji fo = DiscordEmoji.FromGuildEmote(ModuloCliente.Client, 631907691421237288);
            DiscordEmoji ps = DiscordEmoji.FromGuildEmote(ModuloCliente.Client, 631915624494399499);
            embed.AddField($"{pv}**Pontos de Vida**", $"{personagem.VidaAtual.Texto2Casas()}/{personagem.VidaMax.Texto2Casas()}", true);
            embed.AddField($"{pp}**Pontos de Poder**", $"{personagem.MagiaAtual.Texto2Casas()}/{personagem.MagiaMax.Texto2Casas()}", true);
            embed.AddField($"{fo}**Pontos de Fome**", $"{personagem.FomeAtual.Texto2Casas()}/{personagem.FomeMax.Texto2Casas()}", true);
            embed.AddField($"{ps}**Peso**", $"{personagem.FomeAtual.Texto2Casas()}/{personagem.FomeMax.Texto2Casas()}", true);
            await ctx.RespondAsync(embed: embed.Build());
        }

        public void GerarEquips(StringBuilder sr, string nomeExibicao, TipoItemEnum itemEnum, PersonagemRPG personagem)
        {
            sr.Append($"**{nomeExibicao}:** ");
            bool isItem = personagem.Inventario.Equipamentos.TryGetValue(itemEnum, out ItemRPG item);
            if (isItem)
                sr.AppendLine($"{item.Nome}({item.Id}) - *Durab. {item.Durabilidade}/{ModuloBanco.ItemGet(item.Id).Durabilidade}*");
            else
                sr.AppendLine("Nenhum");
        }
    }
}


//AddField("Raça".Titulo(), $"{personagem.Raca.Nome.PrimeiraLetraMaiuscula()}", true)
//            .AddField("Nível".Titulo(), $"Nv.{personagem.NivelAtual}", true)
//            .AddField("Experiencia".Titulo(), $"{personagem.ExpAtual.Texto2Casas()}/{personagem.ExpMax.Texto2Casas()}", true)
