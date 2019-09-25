using System;
using System.Collections.Generic;
using System.Text;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.Data.Itens
{
    public class MoedasItem
    {
        public static ItemRPG MoedaDeCobre()
        {
            return new ItemRPG(0, "Moeda de cobre", TipoItemEnum.Moeda, TipoExpEnum.Nenhum, 0, 0)
            {
                Descricao = "Uma moeda de cobre, vale 1 sense",
            };
        }

        public static ItemRPG TesteItem()
        {
            return new ItemRPG(1, "Espada de teste", TipoItemEnum.Arma, TipoExpEnum.Perfurante, 10, 4)
            {
                Descricao = "Espada de teste, não viu?",
                AtaqueFisico = 20,
            };
        }
    }
}
