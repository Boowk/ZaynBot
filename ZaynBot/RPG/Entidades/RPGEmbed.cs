using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.RPG.Entidades
{
    public class RPGEmbed
    {
        public DiscordEmbedBuilder Embed { get; set; } = new DiscordEmbedBuilder();

        public RPGEmbed(CommandContext ctx, string titulo = null)
        {
            Embed.WithAuthor($"{titulo} {ctx.User.Username}", icon_url: ctx.User.AvatarUrl);
            Embed.WithFooter("Se estiver com dúvidas, escreva z!ajuda");
            Embed.WithColor(DiscordColor.Green);
            Embed.Timestamp = DateTime.Now;
        }

        public DiscordEmbed Build()
        {
            return Embed.Build();
        }
    }
}
