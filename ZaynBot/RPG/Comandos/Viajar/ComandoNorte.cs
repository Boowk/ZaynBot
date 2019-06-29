using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Comandos.Viajar
{
    public class ComandoNorte
    {
        [Command("norte")]
        [Aliases("n")]
        [Description("Explora a área ao Norte.\n\n" +
            "Uso: z!norte")]
        public async Task Norte(CommandContext ctx)
        {
            await new Viajar().ViajarAbAsync(ctx, EnumDirecoes.Norte, "norte");
            await Task.CompletedTask;
        }
    }
}
