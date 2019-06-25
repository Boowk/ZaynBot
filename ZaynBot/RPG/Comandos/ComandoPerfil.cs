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
            "Uso: z!perfil {nome}\n\n" +
            "Exemplo: z!perfil\n\n" +
            "Exemplo: z!perfil @usuario\n\n" +
            "Exemplo: z!perfil 87604980344721408")]
        public async Task ComandoPerfilAb(CommandContext ctx, DiscordUser user = null)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario usuario = ModuloBanco.UsuarioConsultar(ctx.User.Id);
            if (user == null)
            {
                await ctx.RespondAsync(embed: GerarPerfil(ctx.Member, usuario).Build());
                return;
            }
            if (user.IsBot)
            {
                if (user.Id != CoreBot.Id)
                {
                    await ctx.RespondAsync($"{ctx.User.Mention}, não gosto dos outros bots! Porquê você não pergunta sobre mim? :(");
                    return;
                }
                await ctx.RespondAsync($"{ctx.User.Mention}, sou uma garotinha legal! e gosto bastante de você! Mesmo me abusando bastante com esses comandos.... estranhos...");
                return;
            }

            RPGUsuario usuarioRequisitado = ModuloBanco.UsuarioConsultar(user.Id);
            await ctx.RespondAsync(embed: GerarPerfil(user, usuarioRequisitado).Build());
        }

        private DiscordEmbedBuilder GerarPerfil(DiscordUser membro, RPGUsuario usuario)
        {
            string guildaNome = "Nenhuma";
            //if (usuario.IdGuilda.ToString() != Banco.ObjectIDNulo)
            //{
            //    guildaNome = Banco.ConsultarGuilda(usuario.IdGuilda).Nome;
            //}

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
            }.AddField("Guilda", $"{guildaNome}");
        }

    }
}
