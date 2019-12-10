using System.Collections.Generic;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Data.Mundos.Anker
{
    public class TodasAsAreas
    {
        public TodasAsAreas()
        {
            ModuloBanco.Database.DropCollection("regioes");
            List<RPGRegiao> regioes = new List<RPGRegiao>()
            {
               AreaAnker.LugarDesconhecido0(),
                AreaAnker.LugarDesconhecido1(),
                AreaAnker.LugarDesconhecido2(),
                AreaAnker.LugarDesconhecido3(),
                AreaAnker.SaidaDaCaverna4(),
            };
            ModuloBanco.RegiaoColecao.InsertMany(regioes);
        }
    }
}
