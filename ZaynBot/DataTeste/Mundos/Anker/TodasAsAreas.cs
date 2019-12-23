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
               AreaAnker.CidadePrincipal(),
            };
            ModuloBanco.RegiaoColecao.InsertMany(regioes);
        }
    }
}
