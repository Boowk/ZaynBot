using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZaynBot.Eventos
{
    public static class UsuarioNovoEntrouGuilda
    {
        public static async Task EventoBemVindoAsync(GuildMemberAddEventArgs e)
        {
            //if (e.Guild.Id == 508615273515843584)
            //{
            //    DiscordChannel f = e.Guild.GetChannel(551469878268395530);
            //    await f.SendMessageAsync(string.Format(new MensagensEventoBemVindo().Sortear(), e.Member.Mention) + $"\nNão se esqueça de ler as regras!");
            //}
            if (e.Guild.Id == 420044060720627712)
            {
                DiscordChannel f = e.Guild.GetChannel(423347465912320000);
                await f.SendMessageAsync(string.Format(MensagensEventoBemVindo.Sortear(), e.Member.Mention));
            }
            await Task.CompletedTask;
        }
    }

    public static class MensagensEventoBemVindo
    {
        public static string Sortear()
        {
            List<string> Mensagens = new List<string>
            {
                "Batatinha quando nasce espalha rama pelo chão, {0} chegou na área para trazer animação!",
                "{0} acabou de entrar. Finjam que estão ocupados!",
                "{0} ouviu o zunzunzum sobre a festa de ontem?"   ,
                "Estavamos esperando você {0} ( ͡° ͜ʖ ͡°)",
                "Bem vindo {0}! Deixe as suas armas na porta.",
                "{0} acabou de chegar. Parece OP - nerfa por favor."  ,
                "Muhahaha! {0} chegou ativando minha armadilha!!"
            };
            Random r = new Random();
            return Mensagens[r.Next(0, Mensagens.Count)];
        }
    }
}
