using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoComprar : BaseCommandModule
    {
        [Command("comprar")]
        [Description("Permite comprar os itens que estão sendo vendido na região atual.")]
        [ComoUsar("comprar [quantidade|] [nome]")]
        [Exemplo("comprar 1 frasco vermelho")]
        [Exemplo("comprar frasco vermelho")]
        [Cooldown(1, 2, CooldownBucketType.User)]
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
                await ctx.RespondAsync($"{ctx.User.Mention} você não pode comprar o vento!");
                return;
            }

            StringBuilder str = new StringBuilder();
            foreach (var i in regiaoAtual.LojaItensId)
            {
                RPGItem item = ModuloBanco.GetItem(i);
                str.AppendLine($"[{item.Nome}] - {item.PrecoCompra} Zeoin".Bold());
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Loja", ctx);
            embed.WithDescription(str.ToString());
            await ctx.RespondAsync(embed: embed.Build());
        }

        [Command("comprar")]
        [Cooldown(1, 2, CooldownBucketType.User)]
        public async Task ComandoComprarErroAb(CommandContext ctx, [RemainingText] string nomeItem = "")
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync($"Você está usando o comando de forma errada {ctx.User.Mention}!".Bold());
        }
    }
}
