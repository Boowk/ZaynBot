using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace ZaynBot.Core.Comandos
{
    public class ComandoAjuda
    {
        [Command("ajuda")]
        [Aliases("h", "?", "help")]
        [Description("Exibe os comandos, a descrição, suas abreviações e exemplos.\n\n" +
            "Uso: z!ajuda [comando]\n\n" +
            "Exemplo: z!ajuda ajuda")]
        public async Task ComandoAjudaAb(CommandContext ctx, [Description("Nome do comando")]params string[] comando)
        {
            await ctx.TriggerTypingAsync();
            await ctx.CommandsNext.DefaultHelpAsync(ctx, comando); 
        }
    }
}
