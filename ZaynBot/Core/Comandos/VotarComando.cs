using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;

namespace ZaynBot.Core.Comandos
{
    public class VotarComando : BaseCommandModule
    {
        [Command("votar")]
        [Description("Exibe um link, permitindo a votar no bot. (Futuramente terá recompensas)")]
        [UsoAtributo("votar")]
        public async Task VotarComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithColor(DiscordColor.Blue);
            embed.Description = "[Clique aqui para votar no bot](https://discordbots.org/bot/459873132975620134/vote)\n";
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
