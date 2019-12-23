using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ZaynBot
{
    public static class Extensoes
    {
        public static string Titulo(this string titulo)
            => "**⌈" + titulo + "⌋**";

        public static DiscordEmbedBuilder Padrao(this DiscordEmbedBuilder embed)
        {
            embed.WithFooter("Se estiver com dúvidas, escreva z!ajuda");
            embed.Timestamp = DateTime.Now;
            return embed;
        }

        public static DiscordEmbedBuilder Padrao(this DiscordEmbedBuilder embed, string titulo, CommandContext ctx)
            => embed.Padrao().WithAuthor($"{titulo} - {ctx.User.Username}", iconUrl: ctx.User.AvatarUrl);

        public static string FirstUpper(this string texto)
            => texto.First().ToString().ToUpper() + texto.Substring(1);

        public static string Text(this double numero)
          => string.Format("{0:N2}", numero);

        public static string Underline(this string texto)
            => $"__{texto}__";

        public static string Bold(this string texto)
            => $"**{texto}**";

        public static async Task ExecutarComandoAsync(this CommandContext ctx, string comando)
        {
            var cmd = ctx.CommandsNext.FindCommand(comando, out var args);
            var cfx = ctx.CommandsNext.CreateContext(ctx.Message, "z!", cmd, args);
            await ctx.CommandsNext.ExecuteCommandAsync(cfx);
        }
    }
}
