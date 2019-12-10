//using DSharpPlus.CommandsNext;
//using DSharpPlus.CommandsNext.Attributes;
//using System.Threading.Tasks;
//using ZaynBot.Core.Atributos;
//using ZaynBot.RPG.Entidades.Mapa;

//namespace ZaynBot.RPG.Comandos
//{
//    public class LesteComando : BaseCommandModule
//    {
//        [Command("leste")]
//        [Aliases("l")]
//        [Description("Explora a área leste.")]
//        [Cooldown(1, 6, CooldownBucketType.User)]
//        public async Task Leste(CommandContext ctx)
//        {
//            await new Viajar().ViajarAbAsync(ctx, DirecaoEnum.Leste);
//            await Task.CompletedTask;
//        }
//    }
//}
