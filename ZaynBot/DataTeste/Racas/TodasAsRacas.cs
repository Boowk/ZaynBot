using System;
using System.Collections.Generic;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.Data.Racas
{
    public class TodasAsRacas
    {
        public static List<RacaRPG> Racas { get; set; }
        public TodasAsRacas()
        {
            Racas = new List<RacaRPG>()
            {
                HumanoRaca.Bretao(),
                HumanoRaca.Bosmorio(),
                HumanoRaca.Argoniano(),
                HumanoRaca.Altameriao(),
                HumanoRaca.Dunmerio(),
                HumanoRaca.Imperial(),
                HumanoRaca.Nordico(),
                HumanoRaca.Orc(),
            };
        }

        public static RacaRPG RacaGetRandom()
        {
            Random r = new Random();
            int i = r.Next(0, Racas.Count);
            return Racas[i];
        }
    }
}
