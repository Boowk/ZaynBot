using ZaynBot.RPG.Entidades;

namespace ZaynBot.Data.Racas
{
    public class HumanoRaca
    {
        public static RacaRPG Bretao()
              => new RacaRPG(0, "Bretão", 40, 50, 50, 30, 30);

        public static RacaRPG Bosmorio()
            => new RacaRPG(1, "Bosmório", 30, 40, 30, 50, 40);

        public static RacaRPG Argoniano()
            => new RacaRPG(2, "Argoniano", 40, 40, 30, 50, 30);

        public static RacaRPG Altameriao()
            => new RacaRPG(3, "Altamerião", 30, 50, 40, 40, 40);

        public static RacaRPG Dunmerio()
            => new RacaRPG(4, "Dunmerio", 40, 40, 30, 40, 40);
        public static RacaRPG Imperial()
            => new RacaRPG(5, "Imperial", 40, 40, 30, 30, 40);
        public static RacaRPG Nordico()
            => new RacaRPG(6, "Nordico", 50, 30, 30, 40, 50);

        public static RacaRPG Orc()
            => new RacaRPG(7, "Orc", 45, 30, 50, 35, 50);
    }
}
