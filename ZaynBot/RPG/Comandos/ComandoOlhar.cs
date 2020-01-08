using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoOlhar : BaseCommandModule
    {
        [Command("olhar")]
        [Description("Permite olhar para muitas coisas.")]
        [ComoUsar("olhar [|nome]")]
        [Exemplo("olhar rato")]
        [Exemplo("olhar")]
        [Cooldown(1, 8, CooldownBucketType.User)]
        public async Task ComandoLocalAb(CommandContext ctx, [RemainingText] string alvo = "")
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);

            if (string.IsNullOrEmpty(alvo))
            {
                RPGRegiao localAtual = RPGRegiao.GetRegiao(usuario.Personagem.RegiaoAtualId);

                DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Região", ctx);
                embed.WithTitle($"{localAtual.Nome.Titulo()} - {localAtual.Id}");
                embed.WithDescription(localAtual.Descrição);

                StringBuilder conexoesDisponiveis = new StringBuilder();
                foreach (var reg in localAtual.SaidasRegioes)
                    conexoesDisponiveis.Append($"{reg.Direcao.ToString().Bold()}, ");

                if (!string.IsNullOrWhiteSpace(conexoesDisponiveis.ToString()))
                    embed.AddField("Direções obvias".Titulo(), conexoesDisponiveis.ToString());

                embed.WithColor(DiscordColor.Azure);

                await ctx.RespondAsync(embed: embed.Build());
                return;
            }

            if (usuario.Personagem.Batalha.Mob.Nome.ToLower().Equals(alvo.ToLower()))
                await ctx.RespondAsync(usuario.Personagem.Batalha.Mob.Descricao.Bold().Italic());
            else
                await ctx.RespondAsync($"Você procura, mas não encontra nada parecido com '{alvo}' {ctx.User.Mention}!");
        }
    }
}
