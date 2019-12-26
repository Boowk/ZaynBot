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
                ItensRPG.ItemMoedaDeZeoin(),
                ItensRPG.ItemEspadaEnferrujada(),
                ItensRPG.ItemOssos(),
                ItensRPG.ItemCarneDeCoelhoCru(),
                ItensRPG.ItemCouroDeVaca(),
                ItensRPG.ItemCarneDeVacaCru(),
                ItensRPG.ItemCarneDeGalinhaCru(),
                ItensRPG.ItemPenas(),
                ItensRPG.ItemFrascoVermelho()
            };
            ModuloBanco.ItemColecao.InsertMany(itens);
        }
    }
}
