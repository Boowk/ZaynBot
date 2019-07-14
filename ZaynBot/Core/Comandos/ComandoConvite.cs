using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;
using ZaynBot.Core.Entidades;

namespace ZaynBot.Core.Comandos
{
    public class ComandoConvite
    {
        [Command("convite")]
        [Description("Exibe um link para estar adicionando o bot no seu servidor.\n\n" +
            "Uso: z!convite")]
        public async Task ComandoConviteAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithColor(DiscordColor.Blue);
            embed.Description = "[Clique aqui para adicionar o bot no seu servidor](https://discordapp.com/api/oauth2/authorize?client_id=459873132975620134&permissions=469887175&scope=bot)\n";
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
