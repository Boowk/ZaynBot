using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
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
        }
    }
}
