using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using MongoDB.Driver;
using System;
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


        [Command("testep")]
        [Hidden]
        [RequireOwner]
        public async Task Fggff(CommandContext ctx)
        {
            var ie = ctx.Client.GetInteractivityModule();
            List<Page> h = new List<Page>();
            h.Add(new Page() { Content = "Pag 1" });
            h.Add(new Page() { Content = "Pag 2" });
            h.Add(new Page() { Content = "Pag 3" });

            await ie.SendPaginatedMessage(ctx.Channel, ctx.User, h, timeoutoverride: TimeSpan.FromSeconds(20));
        }


        [Command("testef")]
        [Hidden]
        [RequireOwner]
        public async Task Fff(CommandContext ctx)
        {

            //var ie = ctx.Client.GetInteractivityModule();
            //var pgs = ie.GeneratePagesInEmbeds("SADPJADSODSAJIODSJIOAJIOD ASOIJD OAISJD OIASJD OIJDS OIADJOSAJID OAISJD OISJD OIASJD OIASJD OIASJD O" +
            //    "SADIOJOIASJ DOJDS AODJ SAIODJOIASDJ OIASJD OIJD OIASJD OIASJDO IASDJOIASJD OISJD OASIDJ OASDOASIDJOASIDJOASIJDOIASJ DOIASJD OAISDJ " +
            //    "DASOJ ODSAIJ DOIAJS DOIASJD OISAJD OSIAJD OASIJD OASIJD OASIJD OASIJD OASIJD OISAJD OASIJDOAISDJOIASDJ OISDJ OISDJ OSAIJD DISJ " +
            //    "DASOJ ODSAIJ DOIAJS DOIASJD OISAJD OSIAJD OASIJD OASIJD OASIJD OASIJD OASIJD OISAJD OASIJDOAISDJOIASDJ OISDJ OISDJ OSAIJD DISJ " +
            //    "DASOJ ODSAIJ DOIAJS DOIASJD OISAJD OSIAJD OASIJD OASIJD OASIJD OASIJD OASIJD OISAJD OASIJDOAISDJOIASDJ OISDJ OISDJ OSAIJD DISJ " +
            //    "OIDSAJ DOSAIJ DASOIJDS AOIDJAS ODIJAS ODIJAS DOIASJ OASDIJOXICJZCOIAJCP9ASIUDPASUDDPOI SADIOP ASIHAHA UID ASUID IASODH AISUO DOAIUD" +
            //    "");
            //await ie.SendPaginatedMessage(ctx.Channel, ctx.User, pgs, timeoutoverride: TimeSpan.FromSeconds(20));


            DiscordMessage mensagem = await ctx.RespondAsync("Página 1");

            //var emojis = new PaginationEmojis(ctx.Client)
            //{
            //    Left = DiscordEmoji.FromName(ctx.Client, ":joy:")
            //};
            var interacao = ctx.Client.GetInteractivityModule();


            Func<DiscordEmoji, bool> g = x => x.GetDiscordName() == ":gem:";
            Func<DiscordEmoji, bool> y = x => x.GetDiscordName() == ":eyes:" || x.Name == "f";

            var f = await Task.WhenAny(
     interacao.WaitForMessageReactionAsync(g, mensagem).ContinueWith(x => ctx.RespondAsync("Emote g recebido")),
     interacao.WaitForMessageReactionAsync(y, mensagem).ContinueWith(x => ctx.RespondAsync("Emote yes recebido"))

     );         
            //ReactionContext gg = await interacao.WaitForMessageReactionAsync(g, mensagem, timeoutoverride: TimeSpan.FromSeconds(30));
            //await ctx.RespondAsync("E");

        }
    }
}
