using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.Core.Entidades;

namespace ZaynBot.Core.Comandos
{
    public class ComandoPrefixo : BaseCommandModule
    {
        [Command("prefixo")]
        [Description("Permite modificar o prefixo do bot no servidor atual.  Note que o Prefix antigo ainda continuará funcionando")]
        [ComoUsar("prefixo [p|]")]
        [Exemplo("prefixo !")]
        [RequireUserPermissions(Permissions.Administrator)]
        public async Task ComandoPrefixoAb(CommandContext ctx, string prefix = null)
        {
            ServidorCore server = ModuloBanco.GetServidor(ctx.Guild.Id);
            if (string.IsNullOrWhiteSpace(prefix))
            {
                server.Prefix = "";
                server.Salvar();
                await ctx.RespondAsync("Prefix removido!").ConfigureAwait(false);
            }
            else
            {
                if (prefix.Length > 3)
                {
                    await ctx.RespondAsync("O prefixo não pode passar de 3 caracteres!").ConfigureAwait(false);
                    return;
                }
                server.Prefix = prefix;
                server.Salvar();
                await ctx.RespondAsync("Prefixo alterado!").ConfigureAwait(false);
            }
        }
    }
}
