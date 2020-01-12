using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoLoja : BaseCommandModule
    {
        [Command("loja")]
        [Description("Permite ver os itens que estão sendo vendido na região atual.")]
        [ComoUsar("loja")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoLojaAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
            RPGRegiao regiaoAtual = ModuloBanco.GetRegiaoData(usuario.Personagem.RegiaoAtualId);

            if (regiaoAtual.LojaItensId.Count == 0)
            {
                await ctx.RespondAsync($"Não tem [itens] a venda nesta região {ctx.User.Mention}!".Bold());
                return;
            }

            StringBuilder str = new StringBuilder();
            foreach (var i in regiaoAtual.LojaItensId)
            {
                RPGItem item = ModuloBanco.GetItem(i);
                str.AppendLine($"[{item.Nome.Bold()}] - {item.Preco * 10} Zeoin");
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Loja", ctx);
            embed.WithDescription(str.ToString());
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}