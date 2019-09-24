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
                HumanoRaca.Bretoes(),
            };
            ModuloBanco.RacaColecao.InsertMany(racas);
            racas = null;
        }
    }
}
