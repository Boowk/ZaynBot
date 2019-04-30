using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
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
        public async Task Nivel(CommandContext ctx)
        {
            await ctx.RespondAsync($"{ctx.User.Mention}, o seu nível atual é {_userDep.Nivel}! Você ganha exp escrevendo no chat!");
        }
    }
}
