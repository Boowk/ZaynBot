using System.Collections.Generic;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.Data.Itens
{
    public class TodosOsItens
    {
        public TodosOsItens()
        {
            ModuloBanco.Database.DropCollection("itens");
            List<RPGItem> itens = new List<RPGItem>()
            {
               ItensRPG.MoedaDeZeoinItem(),
               ItensRPG.EspadaEnferrujadaItem(),
               ItensRPG.OssosItem(),
               ItensRPG.GosmaItem(),
               ItensRPG.CarneDeCoelhoAssadaItem(),
               ItensRPG.CarneDeCoelhoItem(),
               ItensRPG.OssoAfiadoItem(),
               ItensRPG.FrascoVermelhoItem()
            };
            ModuloBanco.ItemColecao.InsertMany(itens);
        }
    }
}
