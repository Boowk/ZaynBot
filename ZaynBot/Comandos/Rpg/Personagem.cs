using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Entidades;

namespace ZaynBot.Comandos.Rpg
{
    public class Personagem
    {
        private Usuario _userDep;
        public Personagem(Usuario userDep)
        {
            _userDep = userDep;
        }


        [Command("personagem")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public static async Task PingAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync($" Pong! Ping: {ctx.Client.Ping}ms");
        }
    }
}
