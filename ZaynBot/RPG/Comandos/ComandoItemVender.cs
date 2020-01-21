using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoItemVender : BaseCommandModule
    {
        [Command("vender")]
        [Aliases("v")]
        [Description("Permite vender itens que estão na sua mochila.")]
        [ComoUsar("vender [+quantidade] [+preço] [item nome]")]
        [Exemplo("vender 2 20 frasco vermelho")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoVenderAb(CommandContext ctx, int quantidade = -1, int preco = -1, [RemainingText] string itemNome = "")
        {
            await ctx.TriggerTypingAsync();

            if (quantidade <= 0)
            {
                await ctx.ExecutarComandoAsync("ajuda vender");
                return;
            }

            if (preco <= 0)
            {
                await ctx.ExecutarComandoAsync("ajuda vender");
                return;
            }

            if (string.IsNullOrEmpty(itemNome))
            {
                await ctx.ExecutarComandoAsync("ajuda vender");
                return;
            }

            var jogador = ModuloBanco.GetJogador(ctx);
            itemNome = itemNome.ToLower();

            if (jogador.Mochila.TryGetValue(itemNome, out var quantidadeMochila))
            {
                if (quantidade > quantidadeMochila)
                {
                    await ctx.RespondAsync($"{ctx.User.Mention} você somente tem {quantidadeMochila} [{itemNome.FirstUpper()}] na mochila!".Bold());
                    return;
                }
                ModuloBanco.TryGetVenda(ctx.User.Id, out var vendas);
                if (vendas.Count > 0)
                {
                    await ctx.RespondAsync($"Você somente pode vender 1 item por vez {ctx.User.Mention}!");
                    return;
                }

                jogador.Mochila.RemoverItem(itemNome, quantidade);
                new RPGVenda(ctx.User.Id, preco, quantidade, itemNome);
                jogador.Salvar();

                await ctx.RespondAsync($"{ctx.User.Mention} você anunciou no mercado {quantidade} [{itemNome.FirstUpper()}] por {preco} Zayn!".Bold());
            }
            else
                await ctx.RespondAsync($"{ctx.User.Mention} [{itemNome.FirstUpper()}] não foi encontrado na mochila!".Bold());
        }
    }
}
