using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ZaynBot.Comandos.Informações
{
    [Group("info")]
    [Description("Comandos informativos.")]
    public class ComandosInfo
    {
        [Command("ping")]
        [Description("Exibe o tempo de resposta do bot ao servidor do discord.")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task ComandoPing(CommandContext ctx)
        {
            await Ping.PingAb(ctx);
        }
    }
}
