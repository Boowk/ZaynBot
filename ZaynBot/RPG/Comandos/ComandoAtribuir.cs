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
    public class ComandoAtribuir
    {
        [Command("atribuir")]
        [Description("Permite atribuir pontos na proficiência desejada.")]
        [ComoUsar("atribuir [|quantidade] [proficiência]")]
        [Exemplo("atribuir 1 forca")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task ComandoAtribuirAb(CommandContext ctx, int quantidade, [RemainingText] string proficienciaText)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario usuario = ModuloBanco.GetUsuario(ctx.User.Id);

            if (usuario.Personagem.ProficienciaPontos == 0)
            {
                await ctx.RespondAsync($"Você não tem pontos o suficiente para estar atribuindo {ctx.User.Mention}!".Bold());
                return;
            }
        }
    }
}
