using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoComprar : BaseCommandModule
    {
        [Command("comprar")]
        [Description("Permite comprar os itens que estão sendo vendido na região atual.")]
        [ComoUsar("comprar [+quantidade|] [nome]")]
        [Exemplo("comprar 1 frasco vermelho")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoComprarAb(CommandContext ctx, int quantidade = 1, [RemainingText] string nomeItem = "")
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
            RPGRegiao regiaoAtual = ModuloBanco.GetRegiaoData(usuario.Personagem.RegiaoAtualId);

            if (regiaoAtual.LojaItensId.Count == 0)
            {
                await ctx.RespondAsync($"Não tem [itens] a venda nesta região {ctx.User.Mention}!".Bold());
                return;
            }

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
            nomeItem = nomeItem.ToLower();

            RPGItem item = null;
            foreach (var i in regiaoAtual.LojaItensId)
            {
                item = ModuloBanco.GetItem(i);
                if (item.Nome.ToLower() == nomeItem)
                    break;
                else
                    item = null;
            }

            if (item == null)
            {
                await ctx.RespondAsync($"O item [{nomeItem.FirstUpper()}] não está sendo vendido {ctx.User.Mention}!".Bold());
                return;
            }

            if (usuario.Personagem.Mochila.Itens.TryGetValue("moeda de zeoin", out RPGMochilaItemData moedasUsuario))
            {
                int precoTotal = quantidade * item.PrecoCompra;
                if (precoTotal > moedasUsuario.Quantidade)
                    await ctx.RespondAsync($"{ctx.User.Mention} você não tem Zeoin o suficiente para comprar essa quantidade!".Bold());
                else
                {
                    usuario.Personagem.Mochila.AdicionarItem(item, quantidade);
                    moedasUsuario.Quantidade -= precoTotal;
                    if (moedasUsuario.Quantidade == 0)
                        usuario.Personagem.Mochila.Itens.Remove("moeda de zeoin");
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
