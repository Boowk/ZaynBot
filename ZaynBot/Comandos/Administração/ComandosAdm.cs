using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace ZaynBot.Comandos.Administração
{
    [Group("adm")]
    [Description("Comandos administrativos.")]
    public class ComandosAdm
    {
        [Command("botjogando")]
        [RequireOwner]
        [Hidden]
        public async Task ComandoBotJogando(CommandContext ctx, [RemainingText] string texto = "")
        {
            await Cliente.Client.UpdateStatusAsync(new DiscordGame(texto));
            await ctx.RespondAsync("Status jogando alterado com sucesso!");
        }

        [Command("foryou")]
        [RequireOwner]
        [Hidden]
        public async Task foryou(CommandContext ctx, DiscordGuild f, DiscordChannel g, [RemainingText] string texto)
        {
            DiscordGuild ff = await Cliente.Client.GetGuildAsync(f.Id);
            DiscordChannel gg = ff.GetChannel(g.Id);
            await gg.SendMessageAsync(texto);
        }
    }
}
