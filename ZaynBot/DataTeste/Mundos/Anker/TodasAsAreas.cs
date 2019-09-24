using System.Collections.Generic;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.RPG.Data.Mundos.Anker
{
    public class TodasAsAreas
    {
        public TodasAsAreas()
        {
            ModuloBanco.Database.DropCollection("regioes");
            List<RegiaoRPG> regioes = new List<RegiaoRPG>()
            {
               new AreaAnker().LugarDesconhecido0(),
                new AreaAnker().LugarDesconhecido1(),
            };
            ModuloBanco.RegiaoColecao.InsertMany(regioes);
            regioes = null;
        }
    }
}
