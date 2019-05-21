using System;

namespace ZaynBot
{
    public static class Bot
    {
        public const ulong Id = 459873132975620134;
        public static string DiscordBotsApiKey { get; set; }
        public static int QuantidadeServidores { get; set; }
        public static int QuantidadeMembros { get; set; }
        public static int QuantidadeCanais { get; set; }
        public static DateTime TempoAtivo { get; set; } = DateTime.Now;
    }
}
