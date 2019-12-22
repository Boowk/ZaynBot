using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
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
                await ctx.RespondAsync($"{ctx.User.Mention}, você precisa informar um [item] para examinar!".Bold());
                return;
            }
            if (usuario.Personagem.Mochila.Itens.TryGetValue(nomeItem.ToLower(), out RPGMochilaItemData itemData))
            {
                RPGItem item = ModuloBanco.GetItem(itemData.Id);
                DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Item", ctx);
                embed.WithTitle($"**{item.Nome.FirstUpper()}**");
                embed.WithDescription((item.Descricao) != "" ? item.Descricao : "Sem descrição");
                embed.AddField("Tipo".Titulo(), item.TipoItem.ToString(), true);
                //if (item.TipoItem == TipoItemEnum.Arma  || item.TipoItem == TipoItemEnum.Armadura
                //{
                //    embed.AddField("Durabilidade".Titulo(), $"{itemData.Durabilidade}/{item.Durabilidade}", true);
                //    if (item.AtaqueFisico != 0)
                //        embed.AddField("Ataque físico".Titulo(), item.AtaqueFisico.Texto2Casas(), true);
                //    if (item.AtaqueMagico != 0)
                //        embed.AddField("Ataque mágico".Titulo(), item.AtaqueMagico.Texto2Casas(), true);
                //    if (item.DefesaFisica != 0)
                //        embed.AddField("Defesa física".Titulo(), item.DefesaFisica.Texto2Casas(), true);
                //    if (item.DefesaMagica != 0)
                //        embed.AddField("Defesa mágica".Titulo(), item.DefesaMagica.Texto2Casas(), true);
                //}
                embed.WithColor(DiscordColor.Green);
                await ctx.RespondAsync(embed: embed.Build());
                return;
            }
            await ctx.RespondAsync($"{ctx.User.Mention}, você não tem o [{nomeItem}] para poder estar examinando!".Bold());
        }
    }
}
