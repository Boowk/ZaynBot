﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
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

        [Command("teste")]
        [RequireOwner]
        public async Task Jogando(CommandContext ctx, [RemainingText] string mensagem)
        {
            await ctx.RespondAsync($"{dep.Id}, {dep.Nome}");
        }

        [Command("nivel")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task Nivel(CommandContext ctx)
        {
            await ctx.RespondAsync($"{ctx.User.Mention}, o seu nível atual é {dep.Nivel}! Você ganha exp escrevendo no chat!");
        }
    }
}
