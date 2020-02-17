using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ZaynBot
{
    public static class Extensoes
    {
        public static string Titulo(this string titulo)
            => "**⌈" + titulo + "⌋**";

        public static DiscordEmbedBuilder Padrao(this DiscordEmbedBuilder embed)
        {
            embed.WithFooter("Se estiver com dúvidas, escreva ..ajuda");
            embed.Timestamp = DateTime.Now;
            return embed;
        }

        public static DiscordEmbedBuilder Padrao(this DiscordEmbedBuilder embed, string titulo, CommandContext ctx)
            => embed.Padrao().WithAuthor($"{titulo} - {ctx.User.Username}", iconUrl: ctx.User.AvatarUrl);

        public static DiscordEmbedBuilder Padrao(this DiscordEmbedBuilder embed, string titulo, DiscordUser user)
            => embed.Padrao().WithAuthor($"{titulo} - {user.Username}", iconUrl: user.AvatarUrl);

        public static string FirstUpper(this string texto)
            => texto.First().ToString().ToUpper() + texto.Substring(1);

        public static string Text(this double numero)
          => string.Format("{0:N2}", numero);

        public static string Underline(this string texto)
            => $"__{texto}__";

        public static string Bold(this string texto)
            => $"**{texto}**";

        public static string Italic(this string texto)
            => $"*{texto}*";

        public static string RemoverAcentos(this string texto)
            => Regex.Replace(texto, @"[^\u0000-\u007F]+", string.Empty);
        public static async Task ExecutarComandoAsync(this CommandContext ctx, string comando)
        {
            var cmd = ctx.CommandsNext.FindCommand(comando, out var args);
            var cfx = ctx.CommandsNext.CreateContext(ctx.Message, "z!", cmd, args);
            await ctx.CommandsNext.ExecuteCommandAsync(cfx);
        }

        public static string EntrarPasta(string nome)
        {
            StringBuilder raizProjeto = new StringBuilder();
#if DEBUG
            raizProjeto.Append(Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\")));
            raizProjeto.Replace(@"/", @"\");
            return raizProjeto + nome + @"\";
            // linux
#else
            raizProjeto.Append(Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"../../../../")));
            raizProjeto.Replace(@"\", @"/");
            return raizProjeto + nome + @"/";
#endif
        }
    }
}
