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
        [Description("Crie um anúncio no mercado para vender a outros jogadores. Quanto maior o preço, mais demorado é para vender.")]
        [ComoUsar("vender [+quantidade] [+preço cada unidade] [nome do item]")]
        [Exemplo("vender 2 20 ossos")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoVenderAb(CommandContext ctx, int quantidade = -1, int preco = -1, [RemainingText] string itemNome = "")
        {
            await ctx.TriggerTypingAsync();

            if (quantidade < 1)
            {
                await ctx.ExecutarComandoAsync("ajuda vender");
                return;
            }

            if (preco < 1)
            {
                await ctx.ExecutarComandoAsync("ajuda vender");
                return;
            }

            if (string.IsNullOrEmpty(itemNome))
            {
                await ctx.ExecutarComandoAsync("ajuda vender");
                return;
            }

            itemNome = itemNome.ToLower();
            if (itemNome == "zayn")
            {
                await ctx.RespondAsync($"Não é possível vender moeda por moeda {ctx.User.Mention}!");
                return;
            }

            var jogador = ModuloBanco.GetJogador(ctx);

            if (jogador.Mochila.TryGetValue(itemNome, out var quantidadeMochila))
            {
                if (quantidade > quantidadeMochila)
                {
                    await ctx.RespondAsync($"{ctx.User.Mention} você somente tem {quantidadeMochila} [{itemNome.FirstUpper()}] na mochila!".Bold());
                    return;
                }
                ModuloBanco.TryGetVenda(ctx.User.Id, out var vendas);
                if (vendas.Count >= jogador.SlotsVenda)
                {
                    await ctx.RespondAsync($"Você já anúnciou, remova a venda anterior para anúnciar novamente {ctx.User.Mention}!");
                    return;
                }

                jogador.Mochila.RemoverItem(itemNome, quantidade);
                new RPGVenda(ctx.User.Id, preco, quantidade, itemNome);
                jogador.Salvar();

                await ctx.RespondAsync($"{ctx.User.Mention} você anunciou no mercado {quantidade} [{itemNome.FirstUpper()}] por {preco} Zayn cada únidade! Aguarde algum jogador comprar o seu produto.".Bold());
            }
            else
                await ctx.RespondAsync($"{ctx.User.Mention} item [{itemNome.FirstUpper()}] não foi encontrado na mochila!".Bold());
        }
    }
}
