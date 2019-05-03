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
    [Cooldown(1, 10, CooldownBucketType.User)]
    public class ComandosInfo
    {
        [Command("ping")]
        [Description("Exibe o tempo de resposta do bot ao servidor do discord.")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task ComandoPing(CommandContext ctx)
        {
            await Ping.PingAb(ctx);
        }

        [Group("ranque")]
        [Description("Comandos comparativos.")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public class GrupoRanque
        {
            [Command("nivel")]
            [Description("Exibe os maiores niveis do server ou global.")]
            [Cooldown(1, 10, CooldownBucketType.User)]
            public async Task ComandoRanqueNivel(CommandContext ctx, [Description("Servidor ou Global")] string server = "servidor")
            {
                await TopRanque.TopRanqueAb(ctx, server);
            }
        }
    }
}
