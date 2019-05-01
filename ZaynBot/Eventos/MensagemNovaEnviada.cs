using DSharpPlus;
using DSharpPlus.EventArgs;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZaynBot.Entidades;
using ZaynBot.Funções;

namespace ZaynBot.Eventos
{
    public static class MensagemNovaEnviada
    {
        public static async Task XpUsuario(MessageCreateEventArgs e, Usuario user)
        {
            if (DateTime.UtcNow >= user.DataMensagemEnviada)
            {
                user.DataMensagemEnviada = DateTime.UtcNow.AddMinutes(2).AddSeconds(30);
                Random random = new Random();
                int quantiaSorteada = random.Next(5, 26);
                bool evoluiu = user.AdicionarExp(quantiaSorteada);
                Banco.AlterarUsuario(user);

                if (evoluiu == true)
                {
                    e.Client.DebugLogger.LogMessage(LogLevel.Info, e.Guild.Name, $"{e.Author.Username} evoluiu regen para o nível {user.Nivel}.", DateTime.Now);
                    await e.Channel.SendMessageAsync($"Parabéns {e.Author.Mention}! A sua regeneração de vida e mana aumentou para o nível {user.Nivel}! :beginner:");
                }
                else
                {
                    e.Client.DebugLogger.LogMessage(LogLevel.Info, e.Guild.Name, $"{e.Author.Username} recebeu {quantiaSorteada} de exp.", DateTime.Now);
                }
            }
        }
    }
}
