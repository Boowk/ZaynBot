using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoExaminar : BaseCommandModule
    {
        [Command("examinar")]
        [Description("Mostra a descrição de um item")]
        [ComoUsar("examinar [item nome]")]
        [Exemplo("examinar moeda")]
        [Cooldown(1, 4, CooldownBucketType.User)]
        public async Task ComandoExaminarAb(CommandContext ctx, [RemainingText] string nomeItem = "")
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
            if (string.IsNullOrWhiteSpace(nomeItem))
            {
                await ctx.ExecutarComandoAsync("ajuda examinar");
                return;
            }
            if (usuario.Personagem.Mochila.Itens.TryGetValue(nomeItem.ToLower(), out RPGMochilaItemData itemData))
            {
                RPGItem item = ModuloBanco.GetItem(itemData.Id);
                DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Item", ctx);
                embed.WithTitle($"**{item.Nome}**");
                embed.WithDescription(item.Descricao == "" ? "Sem descrição" : item.Descricao);
                embed.AddField("Tipo".Titulo(), item.Tipo.ToString(), true);
                embed.AddField("Venda por".Titulo(), Convert.ToInt32(item.Preco * 0.3).ToString(), true);
                embed.AddField("Compre por".Titulo(), (item.Preco * 10).ToString(), true);
                StringBuilder str = new StringBuilder();
                if (item.AtaqueFisico != 0)
                    str.AppendLine($"Ataque físico: {item.AtaqueFisico}".Bold());
                if (item.AtaqueMagico != 0)
                    str.AppendLine($"Ataque mágico: {item.AtaqueMagico}".Bold());
                if (item.DefesaFisica != 0)
                    str.AppendLine($"Defesa física: {item.DefesaFisica}".Bold());
                if (item.DefesaMagica != 0)
                    str.AppendLine($"Defesa mágica: {item.DefesaMagica}".Bold());
                if (item.FomeRestaura != 0)
                    str.AppendLine($"Restaura {item.FomeRestaura} de fome.");
                if (item.MagiaRestaura != 0)
                    str.AppendLine($"Retaura {item.MagiaRestaura} de magia.");
                if (item.VidaRestaura != 0)
                    str.AppendLine($"Restaura {item.VidaRestaura} de vida.");
                embed.AddField("Outros".Titulo(), str.ToString());
                embed.WithColor(DiscordColor.Green);
                await ctx.RespondAsync(embed: embed.Build());
                return;
            }
            await ctx.RespondAsync($"Você não tem o item [{nomeItem}] para poder estar examinando {ctx.User.Mention}!".Bold());
        }
    }
}
