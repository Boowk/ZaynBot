using DSharpPlus.EventArgs;
using System;
using System.Threading.Tasks;

namespace ZaynBot.Eventos
{
    public static class Voice
    {
        public static Task VoiceJoin(VoiceStateUpdateEventArgs e)
        {
            if (e.User.IsBot) return Task.CompletedTask;

            var usuario = ModuloBanco.GetUsuario(e.User.Id);
            var msg = usuario.Conquistas[Enuns.EnumConquistas.CanalDeVoz];
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
