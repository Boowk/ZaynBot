using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoExaminar : BaseCommandModule
    {
        [Command("examinar")]
        [Description("Permite exibir a descrição de um item.")]
        [ComoUsar("examinar [nome]")]
        [Exemplo("examinar moeda")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoExaminarAb(CommandContext ctx, [RemainingText] string nome = "")
        {
            await ctx.TriggerTypingAsync();
            if (string.IsNullOrWhiteSpace(nome))
            {
                await ctx.ExecutarComandoAsync("ajuda examinar");
                return;
            }
            var usuario = ModuloBanco.GetJogador(ctx);
            nome = nome.ToLower();
            if (usuario.Mochila.TryGetValue(nome, out var itemData))
            {
                if (ModuloBanco.TryGetItem(nome, out var item))
                {
                    DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Item", ctx);
                    embed.WithTitle($"**{item.Nome.FirstUpper()}**");
                    embed.WithDescription(item.Descricao == "" ? "Sem descrição" : item.Descricao);
                    embed.AddField("Tipo".Titulo(), item.Tipo.ToString(), true);
                    embed.AddField("Venda por".Titulo(), Convert.ToInt32(item.PrecoCompra * 0.3).ToString(), true);
                    embed.AddField("Compre por".Titulo(), (item.PrecoCompra * 10).ToString(), true);
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
                    if (!string.IsNullOrEmpty(str.ToString()))
                        embed.AddField("Outros".Titulo(), str.ToString());
                    embed.WithColor(DiscordColor.Green);
                    await ctx.RespondAsync(embed: embed.Build());
                } else
                await ctx.RespondAsync($"Item [{nome.FirstUpper()}] não adicionado no banco de dados, será adicionado em breve {ctx.User.Mention}!".Bold());
            }
            else
                await ctx.RespondAsync($"Item [{nome.FirstUpper()}] não encontrado na mochila examinar {ctx.User.Mention}!".Bold());
        }
    }
}
