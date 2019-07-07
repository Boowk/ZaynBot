﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Comandos.Viajar
{
    public class ComandoOeste
    {
        [Command("oeste")]
        [Aliases("o")]
        [Description("Explora a área ao Oeste.\n\n" +
            "Uso: z!oeste")]
        public async Task Oeste(CommandContext ctx)
        {
            await new Viajar().ViajarAbAsync(ctx, EnumDirecoes.Oeste, "oeste");
            await Task.CompletedTask;
        }
    }
}
