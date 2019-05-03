using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;
using ZaynBot.Entidades;
using ZaynBot.Funções;

namespace ZaynBot.Comandos.Informações
{
    public class Perfil
    {
        private Usuario _userDep;
        public Perfil(Usuario userDep)
        {
            _userDep = userDep;
        }

        [Command("perfil")]                         
        public async Task VerPerfil(CommandContext ctx, DiscordMember membro = null)
        {
            if (membro == null)
            {
                await ctx.RespondAsync(embed: GerarPerfil(ctx.Member, _userDep).Build());
                return;
            }
            if (membro.IsBot)
            {
                if (membro.Id != 459873132975620134)
                {
                    await ctx.RespondAsync($"{ctx.User.Mention}, não gosto dos outros bots! Porquê você não pergunta sobre mim? :(");
                    return;
                }
                await ctx.RespondAsync($"{ctx.User.Mention}, sou uma garotinha legal! e gosto bastante de você! Mesmo me abusando bastante com esses comandos.... estranhos...");
                return;
            }

            Usuario usuarioRequisitado = Banco.ConsultarUsuario(membro);
            await ctx.RespondAsync(embed: GerarPerfil(membro, usuarioRequisitado).Build());
        }

        private DiscordEmbedBuilder GerarPerfil(DiscordMember membro, Usuario usuario)
        {
            return new DiscordEmbedBuilder()
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = membro.Username,
                    IconUrl = membro.AvatarUrl
                },
                Title = ":beginner: Regeneração",
                Description = $"Nivel {usuario.Nivel} [Exp {usuario.ExperienciaAtual}/{usuario.ExperienciaProximoNivel}]",
                Color = DiscordColor.Green,
                Timestamp = DateTime.Now,
                ThumbnailUrl = membro.AvatarUrl,
            };
        }

    }
}
