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

        public RPGEmbed()
        {
            Embed.WithFooter("Se estiver com dúvidas, escreva z!ajuda");
            Embed.WithColor(DiscordColor.Green);
            Embed.Timestamp = DateTime.Now;
        }

        public RPGEmbed(CommandContext ctx, string titulo = null) : this()
        {
            Embed.WithAuthor($"{titulo} {ctx.User.Username}", icon_url: ctx.User.AvatarUrl);
        }

        public RPGEmbed(CommandContext ctx, string titulo, RPGNpc npc) : this()
        {
            Embed.WithAuthor($"{titulo} {ctx.User.Username} com {npc.Nome}", icon_url: ctx.User.AvatarUrl);
        }


        public DiscordEmbed Build()
        {
            return Embed.Build();
        }

        public void DescricaoFala(RPGNpc npc, string descricao)
        {
            Embed.WithDescription($"{npc.Nome} diz: - '{descricao}'");
        }

        public void Titulo(string titulo)
        {
            Embed.WithTitle(titulo.Titulo());
        }
    }
}
