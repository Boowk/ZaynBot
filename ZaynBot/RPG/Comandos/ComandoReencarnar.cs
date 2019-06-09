using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoReencarnar
    {
        [Command("reencarnar")]
        [Description("Reencarnar criando um persoonagem novo.")]
        public async Task Reencarnar(CommandContext ctx)
        {
            RPGUsuario usuario = await Banco.ConsultarUsuarioAsync(ctx);
            if (usuario.Personagem != null)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você precisa morrer antes de tentar reencarnar novamente!");
                return;
            }
            RPGPersonagem personagem = usuario.Personagem;
        }
    }
}
