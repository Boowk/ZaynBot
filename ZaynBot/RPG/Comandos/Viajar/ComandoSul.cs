using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Comandos.Viajar
{
    public class ComandoSul
    {
        [Command("sul")]
        [Aliases("s")]
        [Description("Explora a área ao Sul.")]
        public async Task Sul(CommandContext ctx)
        {
            new Viajar().ViajarAbAsync(ctx, EnumDirecoes.Sul, "sul");
            await Task.CompletedTask;
        }
    }
}
