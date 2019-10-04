using System.Collections.Generic;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.Data.Racas
{
    public class TodasAsRacas
    {
        public TodasAsRacas()
        {
            ModuloBanco.Database.DropCollection("racas");
            List<RacaRPG> racas = new List<RacaRPG>()
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
            ModuloBanco.RacaColecao.InsertMany(racas);
        }
    }
}
