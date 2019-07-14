using DiscordBotsList.Api;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Entidades;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.Core.Comandos
{
    public class ComandoVotar
    {
        [Command("votar")]
        [Description("Exibe um link para poder estar votando.\n\n" +
            "Uso: z!votar")]
        public async Task ComandoAdmBotJogando(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithColor(DiscordColor.Blue);
            embed.Description = "[Clique aqui para votar no bot](https://discordbots.org/bot/459873132975620134/vote)\n";
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
