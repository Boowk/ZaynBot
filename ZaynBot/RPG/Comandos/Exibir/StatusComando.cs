using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Comandos.Exibir
{
    public class StatusComando : BaseCommandModule
    {
        [Command("status")]
        [Description("Exibe os status e os equipamentos do seu personagem.")]
        [Cooldown(1, 3, CooldownBucketType.User)]
        public async Task StatusComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.TryGetPersonagemRPG(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Status", ctx);
            embed.WithColor(DiscordColor.Blue);

            StringBuilder sr = new StringBuilder();
            sr.AppendLine($"**Vida:** {personagem.VidaAtual.Texto2Casas()}/{personagem.VidaMax.Texto2Casas()}");
            sr.AppendLine($"**Mágia:** {personagem.MagiaAtual.Texto2Casas()}/{personagem.MagiaMax.Texto2Casas()}");
            sr.AppendLine($"**Fome:** {personagem.FomeAtual.Texto2Casas()}/{personagem.FomeMax.Texto2Casas()}");
            GerarEquips(sr, "Arma", TipoItemEnum.Arma, personagem);
            GerarEquips(sr, "Escudo", TipoItemEnum.Escudo, personagem);
            GerarEquips(sr, "Helmo", TipoItemEnum.Helmo, personagem);
            GerarEquips(sr, "Couraça", TipoItemEnum.Couraca, personagem);
            GerarEquips(sr, "Luvas", TipoItemEnum.Luvas, personagem);
            GerarEquips(sr, "Botas", TipoItemEnum.Botas, personagem);
            embed.WithDescription(sr.ToString());
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
