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
        [Description("Exibe o que tem dentro da mochila do seu personagem e a capacidade atual/max.")]
        [ComoUsar("mochila [pagina|]")]
        [Exemplo("mochila 2")]
        [Exemplo("mochila")]
        [Cooldown(1, 3, CooldownBucketType.User)]

        public async Task ComandoMochilaAb(CommandContext ctx, int pagina = 0)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Mochila", ctx);
            embed.WithColor(DiscordColor.Purple);
            if (usuario.Personagem.Mochila == null)
                usuario.Personagem.Mochila = new RPGMochila();
            if (usuario.Personagem.Mochila.Itens.Count == 0)
                embed.WithDescription("Nem um farelo dentro.");
            else
            {
                StringBuilder str = new StringBuilder();
                int index = pagina * 10;
                int quantidades = 0;

                for (int i = pagina * 10; i < usuario.Personagem.Mochila.Itens.Count; i++)
                {
                    RPGItem itemData = ModuloBanco.GetItem(usuario.Personagem.Mochila.Itens.Values[index].Id);
                    RPGMochilaItemData item = usuario.Personagem.Mochila.Itens.Values[index];
                    str.Append($"**{item.Quantidade}** - ");
                    str.Append($"{usuario.Personagem.Mochila.Itens.Keys[index].FirstUpper()}".Bold());
                    str.Append("\n");
                    index++;
                    quantidades++;
                    if (quantidades == 10)
                        break;
                }
                embed.WithDescription(str.ToString());
                embed.WithFooter($"Página {pagina} | {usuario.Personagem.Mochila.Itens.Count} Itens diferentes");
            }
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
