﻿//using DSharpPlus.CommandsNext;
//using DSharpPlus.CommandsNext.Attributes;
//using System.Threading.Tasks;
//using ZaynBot.Core.Atributos;
//using ZaynBot.RPG.Entidades.Mapa;

//namespace ZaynBot.RPG.Comandos
//{
//    public class NorteComando : BaseCommandModule
//    {
//        [Command("norte")]
//        [Aliases("n")]
//        [Description("Explora a área Norte.")]
//        [Cooldown(1, 6, CooldownBucketType.User)]
//        public async Task Norte(CommandContext ctx)
//        {
//            await new Viajar().ViajarAbAsync(ctx, DirecaoEnum.Norte);
//            await Task.CompletedTask;
//        }
//    }
//}
