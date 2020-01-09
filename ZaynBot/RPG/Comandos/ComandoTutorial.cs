using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoTutorial : BaseCommandModule
    {
        [Command("tutorial")]
        [Description("Redireciona para o nosso site contendo o tutorial.")]
        [ComoUsar("tutorial")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoTutorialAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithDescription("[Clique aqui para ver o tutorial.](https://zaynrpg.gitbook.io/zaynrpg/comecando/como-funciona)");
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
