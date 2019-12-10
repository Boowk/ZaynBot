//using DSharpPlus.CommandsNext;
//using DSharpPlus.CommandsNext.Attributes;
//using System.Threading.Tasks;
//using ZaynBot.Core.Atributos;
//using ZaynBot.RPG.Entidades.Mapa;

//namespace ZaynBot.RPG.Comandos.Viajar
//{
//    public class SulComando : BaseCommandModule
//    {
//        [Command("sul")]
//        [Aliases("s")]
//        [Description("Explora a área Sul.")]
//        [Cooldown(1, 6, CooldownBucketType.User)]
//        public async Task Sul(CommandContext ctx)
//        {
//            await new Viajar().ViajarAbAsync(ctx, DirecaoEnum.Sul);
//            await Task.CompletedTask;
//        }
//    }
//}
