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
                bool evoluiu = user.AdicionarExp(random.Next(5, 26));
                Banco.AlterarUsuario(user);

                if (evoluiu == true) await e.Channel.SendMessageAsync($"Parabéns {e.Author.Mention}! O seu corpo evoluiu para o nível {user.Nivel}! Regeneração de vida e mana melhoradas!");
            }
            await Task.CompletedTask;
        }
    }
}
