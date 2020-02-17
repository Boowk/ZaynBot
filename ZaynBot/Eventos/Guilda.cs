using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Threading.Tasks;

namespace ZaynBot.Eventos
{
    public static class Guilda
    {
        public static int ServerCount { get; private set; }

        public static Task Disponivel(GuildCreateEventArgs e)
        {
            e.Client.DebugLogger.LogMessage(LogLevel.Info, "ZAYN", $"Guilda {e.Guild.Name.RemoverAcentos()} on!", DateTime.Now);
            ServerCount++;
            return Task.CompletedTask;
        }
    }
}
