using System.Collections.Generic;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.Data.Itens
{
    public class TodosOsItens
    {
        public TodosOsItens()
        {
            ModuloBanco.Database.DropCollection("itens");
            List<ItemRPG> itens = new List<ItemRPG>()
            {
                MoedasItem.MoedaDeCobre(),
                MoedasItem.TesteItem(),
            };
            ModuloBanco.ItemColecao.InsertMany(itens);
            itens = null;
        }
    }
}
