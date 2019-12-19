using DSharpPlus.CommandsNext;
using System;
using System.Collections.Generic;
using System.Text;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Exceptions;

namespace ZaynBot.RPG
{
    public static class Util
    {
        ///// <summary>
        ///// Verifica se o usuario atual é o lider do grupo.
        ///// </summary>
        ///// <param name="ctx"></param>
        ///// <param name="usuario"></param>
        //public static void VerificarLiderGrupo(CommandContext ctx, RPGUsuario usuario)
        //{
        //    if (usuario.Personagem.Batalha.LiderGrupo == 0)
        //        throw new MensagemException($"{ctx.User.Mention}, você deve criar um grupo ou entrar em um existe antes de executar este comando!");

        //    if (usuario.Personagem.Batalha.LiderGrupo != ctx.User.Id)
        //        throw new MensagemException($"{ctx.User.Mention}, somente o lider do grupo pode usar este comando!");
        //}
    }
}
