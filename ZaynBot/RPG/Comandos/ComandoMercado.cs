using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoMercado : BaseCommandModule
    {
        [Command("mercado")]
        [Description("Veja as ofertas de venda de um determinado item. Será exibido as ofertas mais baratas atualmente.")]
        [ComoUsar("mercado [nome do item]")]
        [Exemplo("mercado ossos")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoMercadoAb(CommandContext ctx, [RemainingText] string nomeItem = "")
        {
            await ctx.TriggerTypingAsync();

            if (string.IsNullOrEmpty(nomeItem))
            {
                await ctx.ExecutarComandoAsync("ajuda mercado");
                return;
            }

            var sort = Builders<RPGVenda>.Sort.Ascending(x => x.Preco);
            FindOptions<RPGVenda> options = new FindOptions<RPGVenda>
            {
                Limit = 10,
                NoCursorTimeout = false,
                Sort = sort
            };

            StringBuilder str = new StringBuilder();
            using (IAsyncCursor<RPGVenda> cursor = await ModuloBanco.ColecaoVenda.FindAsync(x => x.ItemNome == nomeItem && x.Quantidade > 0, options))
            {
                while (await cursor.MoveNextAsync())
                {
                    IEnumerable<RPGVenda> itens = cursor.Current;
                    foreach (RPGVenda item in itens)
                    {
                        str.AppendLine($"Quant: {item.Quantidade} | ID: `{item.JogadorId}#{item.Slot}` | {item.Preco} Zeoin");
                    }
                }
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Mercado", ctx);
            embed.WithTitle(nomeItem.FirstUpper().Titulo());
            if (string.IsNullOrEmpty(str.ToString()))
                embed.WithDescription("Este item não tem nada anúnciado! Seja o primeiro a anúnciar com o comando `vender`");
            else
                embed.WithDescription(str.ToString());
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}