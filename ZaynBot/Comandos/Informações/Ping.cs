using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace ZaynBot.Comandos.Informações
{
    public static class Ping
    {
        public static async Task PingAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var emoji = DiscordEmoji.FromName(ctx.Client, ":ping_pong:");
            await ctx.RespondAsync($"{emoji} Pong! Ping: {ctx.Client.Ping}ms");
        }
    }
}
