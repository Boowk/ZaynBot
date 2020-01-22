﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoItemComprar : BaseCommandModule
    {
        [Command("comprar")]
        [Description("Compre itens que foram anúnciado no mercado. Use o ID da venda que é disponibilidado ao usar o comando `mercado`.")]
        [ComoUsar("comprar [+quantidade|] [id da venda]")]
        [Exemplo("comprar 2 88445#0")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoComprarAb(CommandContext ctx, int quantidade = -1, [RemainingText] string itemId = "")
        {
            await ctx.TriggerTypingAsync();
            if (quantidade <= 0)
            {
                await ctx.ExecutarComandoAsync("ajuda comprar");
                return;
            }
            if (string.IsNullOrEmpty(itemId))
            {
                await ctx.ExecutarComandoAsync("ajuda comprar");
                return;
            }

            var array = itemId.Split('#');
            ulong.TryParse(array[0], out var userid);
            if (userid == ctx.User.Id)
            {
                await ctx.RespondAsync($"Não pode comprar do seu proprio anúncio {ctx.User.Mention}!".Bold());
                return;
            }
            if(array.Length == 1)
            {
                await ctx.ExecutarComandoAsync("ajuda comprar");
                return;
            }
            int.TryParse(array[1], out var userslot);
            if (ModuloBanco.TryGetVenda(userid, userslot, out var venda))
            {
                var jogador = ModuloBanco.GetJogador(ctx);
                if (quantidade > venda.Quantidade)
                {
                    await ctx.RespondAsync($"Não tem quantidade o suficiente a venda para comprar essa quantidade {ctx.User.Mention}!".Bold());
                    return;
                }
                if (jogador.Mochila.RemoverItem("zayn", quantidade * venda.Preco))
                {
                    jogador.Mochila.AdicionarItem(venda.ItemNome, quantidade);
                    venda.Quantidade -= quantidade;
                    venda.QuantidadeParaColetar += quantidade * venda.Preco;
                    venda.Salvar();
                    jogador.Salvar();
                    await ctx.RespondAsync($"{ctx.User.Mention} você acabou de comprar {quantidade} [{venda.ItemNome.FirstUpper()}]!".Bold());
                }
                else
                    await ctx.RespondAsync($"{ctx.User.Mention} você não tem [Zayn] o suficiente para comprar essa quantidade!".Bold());
            }
            else
                await ctx.RespondAsync($"Venda não encontrada {ctx.User.Mention}!".Bold());
        }

        [Command("comprar")]
        [Cooldown(1, 1, CooldownBucketType.User)]
        public async Task ComandoComprarAb(CommandContext ctx, [RemainingText] string itemId = "")
        {
            await ctx.ExecutarComandoAsync("ajuda comprar");
        }
    }
}
