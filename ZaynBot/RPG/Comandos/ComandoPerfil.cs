using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;
using ZaynBot.Core.Entidades;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoPerfil
    {
        [Command("perfil")]
        [Description("Exibe o seu perfil. E permite visualizar os de outros usuarios.\n\n" +
            "Uso: z!perfil [nome]\n" +
            "Exemplo: z!perfil\n" +
            "Exemplo: z!perfil @usuario\n" +
            "Exemplo: z!perfil 87604980344721408")]
        public async Task ComandoPerfilAb(CommandContext ctx, DiscordUser user = null)
        {
            if (user == null)
            {
                RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioBaseAsync(ctx);
                await ctx.RespondAsync(embed: GerarPerfil(ctx.Member, usuario).Build());
                return;
            }
            if (user.IsBot)
            {
                await ctx.RespondAsync("Não pergunte sobre mim.");
                return;
            }
             RPGUsuario usuarioRequisitado = RPGUsuario.GetRPGUsuario(user);
             await ctx.RespondAsync(embed: GerarPerfil(user, usuarioRequisitado).Build());
        }

        private DiscordEmbedBuilder GerarPerfil(DiscordUser membro, RPGUsuario usuario)
        {
            return new DiscordEmbedBuilder()
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = membro.Username,
                    IconUrl = membro.AvatarUrl
                },
                Title = ":star: " + "Estrelas",
                Description = usuario.Estrelas.ToString(),
                Color = DiscordColor.Green,
                Timestamp = DateTime.Now,
                ThumbnailUrl = membro.AvatarUrl,
            };
        }
    }
}
