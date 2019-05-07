using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Threading.Tasks;
using ZaynBot.Entidades;

namespace ZaynBot.Eventos
{
    public static class MensagemNovaRecebida
    {
        public static async Task ReceberXPNivelMensagens(MessageCreateEventArgs e)
        {
            Usuario user = Banco.ConsultarUsuario(e.Author.Id);
            if (DateTime.UtcNow >= user.DataUltimaMensagemEnviada)
            {
                user.DataUltimaMensagemEnviada = DateTime.UtcNow.AddMinutes(2).AddSeconds(30);
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
