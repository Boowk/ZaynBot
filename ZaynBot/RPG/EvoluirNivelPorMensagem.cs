using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG
{
    public static class EvoluirNivelPorMensagem
    {
        public static async Task ReceberXPNivelMensagens(MessageCreateEventArgs e)
        {
            RPGUsuario user = ModuloBanco.UsuarioConsultar(e.Author.Id);
            if (DateTime.UtcNow >= user.DataUltimaMensagemEnviada)
            {
                user.DataUltimaMensagemEnviada = DateTime.UtcNow.AddMinutes(2).AddSeconds(30);
                Tuple<bool, int> result = user.AdicionarExp();
                if (user.Personagem != null)
                {
                    user.RegeneraçãoMana();
                    user.RegeneraçãoVida();
                }
                ModuloBanco.UsuarioAlterar(user);
                if (result.Item1 == true)
                {
                    e.Client.DebugLogger.LogMessage(LogLevel.Info, e.Guild.Name, $"{e.Author.Username} evoluiu regen para o nível {user.Nivel}.", DateTime.Now);
                    await e.Channel.SendMessageAsync($"Parabéns {e.Author.Mention}! A sua regeneração de vida e mana aumentou para o nível {user.Nivel}! :beginner:");
                }
                else
                {
                    e.Client.DebugLogger.LogMessage(LogLevel.Info, e.Guild.Name, $"{e.Author.Username} recebeu {result.Item2} de exp.", DateTime.Now);
                }
            }
        }
    }
}
