using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Comandos.Viajar
{
    public class ComandoLeste
    {
        [Command("leste")]
        [Aliases("l")]
        [Description("Explora a área ao leste.\n\n" +
            "Uso: z!leste")]
        public async Task Leste(CommandContext ctx)
        {
            new Viajar().ViajarAbAsync(ctx, EnumDirecoes.Leste, "leste");
            await Task.CompletedTask;
        }
    }
}
