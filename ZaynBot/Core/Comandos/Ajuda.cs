using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace ZaynBot.Core.Comandos
{
    public class Ajuda
    {
        [Command("ajuda")]
        [Aliases("h", "?")]
        [Description("Exibe os comandos, a descrição do comando, suas abreviações e argumentos.")]    
        public async Task HelpAsync(CommandContext ctx, [Description("Nome do comando")]params string[] comando)
        {
            await ctx.TriggerTypingAsync();
            await ctx.CommandsNext.DefaultHelpAsync(ctx, comando);
        }
    }
}
