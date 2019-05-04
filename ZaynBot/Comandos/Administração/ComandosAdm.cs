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
        public async Task foryou(CommandContext ctx,  DiscordChannel f, [RemainingText] string texto = "")
        {
            await f.SendMessageAsync(texto);
        }
    }
}
