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
    public class ComandoLoja : BaseCommandModule
    {
        [Command("loja")]
        [Description("Permite ver as ofertas de um determinado item.")]
        [ComoUsar("loja [item nome]")]
        [Exemplo("loja frasco vermelho")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoLojaAb(CommandContext ctx, [RemainingText] string nomeItem = "")
        {
            await ctx.TriggerTypingAsync();

            if (string.IsNullOrEmpty(nomeItem))
            {
                await ctx.ExecutarComandoAsync("ajuda loja");
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
                        str.AppendLine($"{item.Quantidade} - ID: `{item.JogadorId}#{item.Slot}` - {item.Preco} Zeoin");
                    }
                }
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Loja", ctx);
            embed.WithTitle(nomeItem.FirstUpper().Titulo());
            embed.WithDescription(str.ToString());
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}