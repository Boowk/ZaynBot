using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using static DSharpPlus.CommandsNext.CommandsNextExtension;

namespace ZaynBot.Core.Comandos
{
    public class ComandoAjuda : BaseCommandModule
    {
        [Command("ajuda")]
        [Aliases("h", "?", "help")]
        [Description("Explica como usar um comando, suas abreviações e exemplos.")]
        [ComoUsar("ajuda [comando|]")]
        [Exemplo("ajuda personagem")]
        [Exemplo("ajuda")]

        public async Task AjudaComandoAb(CommandContext ctx, params string[] comando)
        {
            await ctx.TriggerTypingAsync();
            await new DefaultHelpModule().DefaultHelpAsync(ctx, comando);
        }
    }
}
