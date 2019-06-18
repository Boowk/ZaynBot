using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Entidades;

namespace ZaynBot.Core.Comandos
{
    public class GrupoCargos
    {
        [Group("cargo", CanInvokeWithoutSubcommand = true)]
        [Description("Cargos para selecionar do servidor.")]

        public class GrupoGuilda
        {
            public async Task ExecuteGroupAsync(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();
                CoreServidor servidor = ModuloBanco.ServidorConsulta(ctx.Guild.Id);
                if(servidor.IdCargos.Count == 0)
                {
                    await ctx.RespondAsync("Esse servidor não tem cargos disponíveis para selecionar.");
                    return;
                }
                await Task.CompletedTask;
            }

            [Command("lista")]
            [Description("Exibe os cargos disponíveis para selecionar da guilda")]
            public async Task ComandoGuildaInfo(CommandContext ctx)
            {
                await ctx.TriggerTypingAsync();
                await ctx.RespondAsync("Comando em construção");
            }
        }
    }
}
