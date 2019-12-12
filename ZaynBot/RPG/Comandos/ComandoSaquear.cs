using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoSaquear : BaseCommandModule
    {
        [Command("saquear")]
        [Description("Saqueia os inimigos mortos, finalizando assim a batalha.")]
        [UsoAtributo("saquear")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task ComandoSaquearAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario lider);


            if (lider.Personagem.Batalha.LiderGrupo == 0)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você deve criar um grupo ou entrar em um antes de saquear inimigos!");
                return;
            }

            if (lider.Personagem.Batalha.LiderGrupo != ctx.User.Id)
            {
                await ctx.RespondAsync($"Somente o lider do grupo pode usar este comando, {ctx.User.Mention}!");
                return;
            }

            if (lider.Personagem.Batalha.MobsVivos.Count != 0)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, ainda tem inimigos vivos! Mate-os para poder saquear!");
                return;
            }
            if (lider.Personagem.Batalha.MobsMortos.Count == 0)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, parece que não tem nenhum inimigo para você saquear!");
                return;
            }

            await ctx.RespondAsync($"Distribuindo itens/xp");
            //temp
            lider.Personagem.Batalha.MobsMortos = new Dictionary<string, RPGMob>();
            lider.Personagem.Batalha.Turno = 0;
            RPGUsuario.Salvar(lider);
        }
    }
}
