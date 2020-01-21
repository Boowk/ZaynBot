using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoMochila : BaseCommandModule
    {
        [Command("mochila")]
        [Aliases("m")]
        [Description("Permite exibir os itens da mochila do seu personagem. A cada 10 itens diferentes, incrementar 1 página.")]
        [ComoUsar("mochila [+página]")]
        [Exemplo("mochila 2")]
        [Exemplo("mochila")]
        [Cooldown(1, 15, CooldownBucketType.User)]

        public async Task ComandoMochilaAb(CommandContext ctx, int pagina = 0)
        {
            await ctx.TriggerTypingAsync();

            if (pagina < 0)
            {
                await ctx.ExecutarComandoAsync("ajuda mochila");
                return;
            }

            var jogador = ModuloBanco.GetJogador(ctx);

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Mochila", ctx);
            embed.WithColor(DiscordColor.Purple);
            if (jogador.Mochila == null)
                jogador.Mochila = new RPGMochila();
            if (jogador.Mochila.Count == 0)
                embed.WithDescription("Nem um farelo dentro.");
            else
            {
                StringBuilder str = new StringBuilder();
                int index = pagina * 10;
                int quantidades = 0;

                for (int i = pagina * 10; i < jogador.Mochila.Count; i++)
                {
                    var item = jogador.Mochila.Values[index];
                    str.AppendLine($"{item} - {jogador.Mochila.Keys[index].FirstUpper()}".Bold());
                    index++;
                    quantidades++;
                    if (quantidades == 10)
                        break;
                }
                embed.WithDescription(str.ToString());
                embed.WithFooter($"Página {pagina} | {jogador.Mochila.Count} Itens diferentes");
            }
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
