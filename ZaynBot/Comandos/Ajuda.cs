using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZaynBot.Comandos
{
    public class Ajuda
    {
        [Command("ajuda")]
        [Aliases("h", "?")]
        [Description("Exibe os comandos, a descrição do comando, suas abreviações e argumentos.")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task HelpAsync(CommandContext ctx, [Description("Nome do comando")]params string[] comando)
        {
            await ctx.CommandsNext.DefaultHelpAsync(ctx, comando);
        }
    }
}
