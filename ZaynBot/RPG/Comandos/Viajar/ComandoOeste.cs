using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Comandos.Viajar
{
    public class ComandoOeste
    {
        [Command("oeste")]
        [Aliases("e")]
        [Description("Explora a área ao Oeste.")]
        public async Task Oeste(CommandContext ctx)
        {
            new Viajar().ViajarAbAsync(ctx, EnumDirecoes.Oeste, "oeste");
            await Task.CompletedTask;
        }
    }
}
