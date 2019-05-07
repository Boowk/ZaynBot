using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace ZaynBot.Comandos.ComandosInformação
{
    public class ComandoConvite
    {
        [Command("convite")]
        [Description("Envia uma serie de link sobre o bot.")]
        public async Task ComandoConviteAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithColor(DiscordColor.Blue);
            embed.Description = "[Servidor Oficial](https://discord.gg/GGRnMQu)\n" +
                "[Adicionar bot](https://discordapp.com/api/oauth2/authorize?client_id=459873132975620134&permissions=469887175&scope=bot)\n" +
                "[Vote como melhor bot](https://discordbots.org/bot/459873132975620134/vote)\n" +
                "[Repositorio no Github](https://github.com/ZaynBot/ZaynBot)";
            embed.WithThumbnailUrl("https://cdn.discordapp.com/attachments/570310203040595997/573688403971801098/18acb6595257356c7544bf6265fe5896.png");
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
