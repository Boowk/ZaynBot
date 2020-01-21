using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using MongoDB.Driver;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoItemComprar : BaseCommandModule
    {
        [Command("comprar")]
        [Description("Permite comprar os itens que estão sendo vendido na região atual.")]
        [ComoUsar("comprar [+quantidade|] [nome]")]
        [Exemplo("comprar 1 frasco vermelho")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoComprarAb(CommandContext ctx, int quantidade = 1, [RemainingText] string nomeItem = "")
        {
            await ctx.TriggerTypingAsync();
            //Verificador se ele digitou corretamente o comando
            if (string.IsNullOrEmpty(nomeItem))
            {
                await ctx.ExecutarComandoAsync("ajuda comprar");
                return;
            }
            if (quantidade <= 0)
            {
                await ctx.ExecutarComandoAsync("ajuda comprar");
                return;
            }

            var usuario = ModuloBanco.GetJogador(ctx);
            nomeItem = nomeItem.ToLower();

            var fd = Builders<RPGItem>.Filter;
            var filter = fd.Where(x => x.Nome.ToLower() == nomeItem);
            var item = ModuloBanco.ColecaoItem.Find(filter).FirstOrDefault();

            if (item == null)
            {
                await ctx.RespondAsync($"O item [{nomeItem.FirstUpper()}] não foi encontrado {ctx.User.Mention}!".Bold());
                return;
            }

            if (usuario.Mochila.TryGetValue("moeda de zeoin", out var moedasUsuario))
            {
                int precoTotal = quantidade * (item.PrecoCompra * 10);
                if (precoTotal > moedasUsuario)
                    await ctx.RespondAsync($"{ctx.User.Mention} você não tem Zeoin o suficiente para comprar essa quantidade!".Bold());
                else
                {
                    usuario.Mochila.AdicionarItem(item, quantidade);
                    moedasUsuario -= precoTotal;
                    if (moedasUsuario == 0)
                        usuario.Mochila.Remove("moeda de zeoin");
                    await ctx.RespondAsync($"{ctx.User.Mention} você acabou de comprar {quantidade} [{item.Nome.FirstUpper()}]!".Bold());
                    usuario.Salvar();
                }
            }
            else
                await ctx.RespondAsync($"{ctx.User.Mention} você não tem Zeoin para estar comprando!".Bold());
        }

        [Command("comprar")]
        [ComoUsar("comprar [nome]")]
        [Exemplo("comprar frasco vermelho")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoComprarErroAb(CommandContext ctx, [RemainingText] string nomeItem = "")
            => await ComandoComprarAb(ctx, 1, nomeItem);
    }
}
