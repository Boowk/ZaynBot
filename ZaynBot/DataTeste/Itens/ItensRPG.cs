using System;
using System.Collections.Generic;
using System.Text;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.Data.Itens
{
    public class ItensRPG
    {
        public static ItemRPG MoedaDeCobreItem()
        {
            return new ItemRPG(0, "Moeda de cobre", TipoItemEnum.Moeda, TipoExpEnum.Nenhum, 0, 0)
            {
                Descricao = "Uma moeda de cobre, vale 1 sense.",
            };
        }

        public static ItemRPG EspadaEnferrujadaItem()
        {
            return new ItemRPG(1, "Espada enferrujada de bronze", TipoItemEnum.Arma, TipoExpEnum.Perfurante, 64, 2)
            {
                Descricao = "Uma espada de bronze muito degastada, devido ao uso constante.",
                AtaqueFisico = 20,
            };
        }

        public static ItemRPG Ossositem()
        {
            return new ItemRPG(2, "Ossos", TipoItemEnum.Recurso, TipoExpEnum.Nenhum, 0, 1)
            {
                Descricao = "Ossos.. isso dá um arrepio."
            };
        }

        public static ItemRPG GosmaItem()
        {
            return new ItemRPG(3, "Gosma", TipoItemEnum.Recurso, TipoExpEnum.Nenhum, 0, 3)
            {
                Descricao = "Muito gosmento, onde gruda não sai com facilidade."
            };
        }
    }
}
