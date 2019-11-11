//using DSharpPlus.CommandsNext;
//using DSharpPlus.CommandsNext.Attributes;
//using System.Threading.Tasks;
//using ZaynBot.Core.Atributos;
//using ZaynBot.RPG.Entidades.Mapa;

//namespace ZaynBot.RPG.Comandos.Viajar
//{
//    public class OesteComando : BaseCommandModule
//    {
//        [Command("oeste")]
//        [Aliases("o")]
//        [Description("Explora a área Oeste.")]
//        [Cooldown(1, 6, CooldownBucketType.User)]
//        public async Task Oeste(CommandContext ctx)
//        {
//            await new Viajar().ViajarAbAsync(ctx, DirecaoEnum.Oeste);
//            await Task.CompletedTask;
//        }
//    }
//}
