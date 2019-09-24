using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ZaynBot.RPG.Entidades;

namespace ZaynBot
{
    public static class Extensoes
    {
        public static string Titulo(this string titulo)
            => "⌈" + titulo + "⌋";

        public static DiscordEmbedBuilder Padrao(this DiscordEmbedBuilder embed)
        {
            embed.WithFooter("Se estiver com dúvidas, escreva z!ajuda");
            embed.WithColor(DiscordColor.Green);
            embed.Timestamp = DateTime.Now;
            return embed;
        }

        public static DiscordEmbedBuilder Padrao(this DiscordEmbedBuilder embed, string titulo, CommandContext ctx)
        {
            embed.Padrao();
            embed.WithAuthor($"{titulo} - {ctx.User.Username}", iconUrl: ctx.User.AvatarUrl);
            return embed;
        }

        public static void Add(this Dictionary<string, ItemRPG> inventario, ItemRPG item)
            => inventario.Add(item.Nome.ToLower(), item);

        public static bool IsNullOrEmpty(this Array array)
            => array == null || array.Length == 0;

        public static string PrimeiraLetraMaiuscula(this string texto)
            => texto.First().ToString().ToUpper() + texto.Substring(1);

        public static string Texto2Casas(this float numero)
            => string.Format("{0:N2}", numero);

        public static string Texto2Casas(this double numero)
          => string.Format("{0:N2}", numero);
    }
}
