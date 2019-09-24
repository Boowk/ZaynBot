﻿using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.Core.Entidades;

namespace ZaynBot.Core.Comandos
{
    public class PrefixComando : BaseCommandModule
    {
        [Command("prefix")]
        [Description("Permite modificar o prefix do bot no servidor atual. Prefix antigo ainda continuará funcionando.")]
        [UsoAtributo("prefix [pp]")]
        [ExemploAtributo("prefix !")]
        [RequireUserPermissions(Permissions.Administrator)]
        public async Task PrefixComandoAb(CommandContext ctx, string prefix = null)
        {
            if (prefix.Length > 3)
            {
                await ctx.RespondAsync("O prefix não pode passar de 3 caracteres!").ConfigureAwait(false);
                return;
            }
            if (string.IsNullOrWhiteSpace(prefix))
            {
                ModuloBanco.ServidorDel(ctx.Guild.Id);
                await ctx.RespondAsync("Prefix removido!").ConfigureAwait(false);
            }
            else
            {
                ModuloBanco.ServidorDel(ctx.Guild.Id);
                ModuloBanco.ServidorColecao.InsertOne(new ServidorCore()
                {
                    Id = ctx.Guild.Id,
                    Prefix = prefix,
                });
                await ctx.RespondAsync("Prefix alterado!").ConfigureAwait(false);
            }
        }
    }
}
