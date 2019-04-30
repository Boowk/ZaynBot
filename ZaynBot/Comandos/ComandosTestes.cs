using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaynBot.Entidades;

namespace ZaynBot.Comandos
{
    public class ComandosTestes
    {
        [Command("testeEmoji")]
        [RequireOwner]
        public async Task TesteEmoji(CommandContext ctx)
        {
            DiscordEmojiConverter conversorEmoji = new DiscordEmojiConverter();
            conversorEmoji.TryConvert("<:tacerto:550295427883466793>", ctx, out DiscordEmoji emojiOutroCanal);

            var emoji = DiscordEmoji.FromName(ctx.Client, ":ping_pong:").ToString();

            var emoji3 = DiscordEmoji.FromUnicode(ctx.Client, "<:monikaping:417380874297016331>").ToString();

            var emoji4 = DiscordEmoji.FromUnicode("<:monikaping:417380874297016331>").ToString();

            var emoji6 = DiscordEmoji.FromGuildEmote(ctx.Client, 550295427883466793).ToString();

            await ctx.RespondAsync(DiscordEmoji.FromGuildEmote(ctx.Client, 550295427883466793).ToString());

            await ctx.RespondAsync(DiscordEmoji.FromGuildEmote(ctx.Client, 550295427883466793).ToString());

            await ctx.RespondAsync($"{emojiOutroCanal.ToString()}, {emoji}, {emoji3}, {emoji4},, {emoji6}");
        }

        Usuario dep;
        public ComandosTestes(Usuario d)
        {
            dep = d;
        }

        [Command("purgeBot")]
        [RequireOwner]
        public async Task Jogando(CommandContext ctx)
        {
            List<DiscordMessage> messageList = (await ctx.Channel.GetMessagesAsync(100)).ToList();
            foreach (DiscordMessage msg in messageList)
            {
                if (msg.Author.Id == 459873132975620134)
                {
                    await msg.DeleteAsync();
                }
            }
        }

        [Command("fff")]
        [RequireOwner]
        public async Task fff(CommandContext ctx)
        {
            await ctx.RespondAsync(string.Format(new Eventos.MensagensEventoBemVindo().Sortear(), ctx.User.Mention));
        }
    }
}
