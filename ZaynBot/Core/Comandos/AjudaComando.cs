using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using static DSharpPlus.CommandsNext.CommandsNextExtension;

namespace ZaynBot.Core.Comandos
{
    public class AjudaComando : BaseCommandModule
    {
        [Command("ajuda")]
        [Aliases("h", "?", "help")]
        [Description("Explica como usar um comando, suas abreviações e exemplos.")]
        [UsoAtributo("ajuda [comando|]")]
        [ExemploAtributo("ajuda personagem")]
        [ExemploAtributo("ajuda")]

        public async Task AjudaComandoAb(CommandContext ctx, params string[] comando)
        {
            await ctx.TriggerTypingAsync();
            await new DefaultHelpModule().DefaultHelpAsync(ctx, comando);
        }
    }
}
