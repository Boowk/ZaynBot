using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Entidades;

namespace ZaynBot.Comandos
{
    public class ComandosTestes
    {
        [Command("testeEmoji")]
        [Hidden]
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
        [Hidden]
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
        [Hidden]
        [RequireOwner]
        public async Task Fff(CommandContext ctx)
        {

            IMongoClient client = new MongoClient("mongodb://localhost");
            IMongoDatabase database = client.GetDatabase("zaynbot");
            IMongoCollection<Usuario> col = database.GetCollection<Usuario>("usuarios");
            StringBuilder g = new StringBuilder();
            StringBuilder gg = new StringBuilder();

            List<ulong> ids = new List<ulong>();
            List<int> niveis = new List<int>();
            List<DiscordUser> usuarios = new List<DiscordUser>();

            await col.Find(FilterDefinition<Usuario>.Empty).Limit(3).Sort("{Nivel: -1}")
                .ForEachAsync(x =>
            {
                ids.Add(x.Id);
                niveis.Add(x.Nivel);

                g.Append($"{x.Nivel}, {x.Id}\n");
            }).ConfigureAwait(false);
            int index = 0;
            foreach (var item in ids)
            {
                DiscordUser u = await ctx.Client.GetUserAsync(item);
                gg.Append($"{u.Username}, Nivel: {niveis[index]}");
                index++;
            }


            await ctx.RespondAsync(g.ToString() + "KKKKK\n"+ gg.ToString());
        }
    }
}
