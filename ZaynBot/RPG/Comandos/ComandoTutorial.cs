using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;

namespace ZaynBot.RPG.Comandos
{
    [Group("tutorial")]
    [Description("Tutoriais.")]
    [ComoUsar("tutorial [pagina]")]
    [Exemplo("tutorial 1")]
    public class ComandoTutorial : BaseCommandModule
    {
        [GroupCommand]
        public async Task GroupCommandAsync(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithDescription("Escreva z!tutorial [pagina] para ver um tutorial.\n" +
                "1. Combate");
            await ctx.RespondAsync(embed: embed.Build());
        }

        [Command("1")]
        public async Task Pagina1(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithDescription("Antes de iniciar um combate você precisa encontrar algum inimigo para batalhar! " +
                "Escreva `z!explorar` para encontrar algum inimigo na sua região. Depois use `z!atacar` até ter " +
                "acabo com a vida do infeliz!");
            embed.WithImageUrl("https://cdn.discordapp.com/attachments/651848690033754113/657235744217104405/unknown.png");
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
