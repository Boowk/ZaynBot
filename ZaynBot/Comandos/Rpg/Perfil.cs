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
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithColor(DiscordColor.Yellow);
            embed.WithTitle($"**{ctx.User.Username}**");
            //embed.WithDescription($"");                 
            embed.AddField($":regional_indicator_l: Nível [138s]", $"{_userDep.Nivel} [EXP: {_userDep.ExperienciaAtual}/{_userDep.ExperienciaProximoNivel}]", true);
            embed.WithThumbnailUrl(ctx.User.AvatarUrl);
            embed.WithTimestamp(DateTime.Now);
            embed.WithFooter("Perfil", ctx.User.AvatarUrl);

            await ctx.RespondAsync(ctx.User.Mention, embed: embed.Build());
        }
    }
}
