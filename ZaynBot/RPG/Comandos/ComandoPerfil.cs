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
        public async Task ComandoPerfilAb(CommandContext ctx, DiscordMember membro = null)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario usuario = Banco.ConsultarUsuario(ctx.User.Id);
            if (membro == null)
            {
                await ctx.RespondAsync(embed: GerarPerfil(ctx.Member, usuario).Build());
                return;
            }
            if (membro.IsBot)
            {
                if (membro.Id != CoreBot.Id)
                {
                    await ctx.RespondAsync($"{ctx.User.Mention}, não gosto dos outros bots! Porquê você não pergunta sobre mim? :(");
                    return;
                }
                await ctx.RespondAsync($"{ctx.User.Mention}, sou uma garotinha legal! e gosto bastante de você! Mesmo me abusando bastante com esses comandos.... estranhos...");
                return;
            }

            RPGUsuario usuarioRequisitado = Banco.ConsultarUsuario(membro.Id);
            await ctx.RespondAsync(embed: GerarPerfil(membro, usuarioRequisitado).Build());
        }

        private DiscordEmbedBuilder GerarPerfil(DiscordMember membro, RPGUsuario usuario)
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
