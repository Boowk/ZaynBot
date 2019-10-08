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
               ItensRPG.MoedaDeCobreItem(),
               ItensRPG.EspadaEnferrujadaItem(),
               ItensRPG.Ossositem(),
               ItensRPG.GosmaItem(),
               ItensRPG.CarneDeCoelhoAssada(),
               ItensRPG.CarneDeCoelho(),
               ItensRPG.OssoAfiado()
            };
            ModuloBanco.ItemColecao.InsertMany(itens);
        }
    }
}
