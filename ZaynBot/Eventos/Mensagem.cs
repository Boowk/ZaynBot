using DSharpPlus.Enums;
using DSharpPlus.EventArgs;
using System;
using System.Threading.Tasks;

namespace ZaynBot.Eventos
{
    public static class Mensagem
    {
        public static Task Criada(MessageCreateEventArgs e)
        {
            if (e.Message?.Author?.IsBot == true) return Task.CompletedTask;
            if (e.Message.MessageType == MessageType.GuildMemberJoin) return Task.CompletedTask;

            var usuario = ModuloBanco.GetUsuario(e.Author.Id);
            var msg = usuario.Conquistas[Enuns.EnumConquistas.MensagensCriadas];
            if (msg.ProxTrigger <= DateTime.UtcNow)
            {
                msg.AdicionarProgresso(TimeSpan.FromMinutes(1));
                usuario.AdicionarReal(0.52083m);
                usuario.Salvar();
            }
            return Task.CompletedTask;
        }

        public static Task Editada(MessageUpdateEventArgs e)
        {
            if (e.Message?.Author?.IsBot == true) return Task.CompletedTask;
            if (e.Message.MessageType == MessageType.ChannelPinnedMessage) return Task.CompletedTask;

            var usuario = ModuloBanco.GetUsuario(e.Author.Id);
            var msg = usuario.Conquistas[Enuns.EnumConquistas.MensagensEditadas];
            if (msg.ProxTrigger <= DateTime.UtcNow)
            {
                msg.AdicionarProgresso(TimeSpan.FromMinutes(1));
                usuario.AdicionarReal(0.52083m);
                usuario.Salvar();
            }
            return Task.CompletedTask;
        }

        public static Task Apagada(MessageDeleteEventArgs e)
        {
            if (e.Message?.Author?.IsBot == true) return Task.CompletedTask;

            var usuario = ModuloBanco.GetUsuario(e.Message.Author.Id);
            var msg = usuario.Conquistas[Enuns.EnumConquistas.MensagensDeletadas];
            if (msg.ProxTrigger <= DateTime.UtcNow)
            {
                msg.AdicionarProgresso(TimeSpan.FromMinutes(1));
                usuario.AdicionarReal(0.52083m);
                usuario.Salvar();
            }
            return Task.CompletedTask;
        }
    }
}
