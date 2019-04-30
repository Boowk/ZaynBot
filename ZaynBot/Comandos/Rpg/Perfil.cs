using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Entidades;

namespace ZaynBot.Comandos.Rpg
{
    public class Perfil
    {
        private Usuario _userDep;
        public Perfil(Usuario userDep)
        {
            _userDep = userDep;
        }

        [Command("perfil")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task VerPerfil(CommandContext ctx)
        {
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = ctx.User.Username,
                    IconUrl = ctx.User.AvatarUrl
                },
                Title = ":muscle: Corpo",
                Description = $"Nivel {_userDep.Nivel} [Exp {_userDep.ExperienciaAtual}/{_userDep.ExperienciaProximoNivel}]",
                Color = DiscordColor.Green,
                Timestamp = DateTime.Now,
                ThumbnailUrl = ctx.User.AvatarUrl,
            };         

            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
