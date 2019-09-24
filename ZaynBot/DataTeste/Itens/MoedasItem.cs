using System;
using System.Collections.Generic;
using System.Text;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.Data.Itens
{
    public class MoedasItem
    {
        public static ItemRPG MoedaDeCobre()
        {
            return new ItemRPG(RPG.Entidades.Enuns.TipoItemEnum.Moeda, 0)
            {
                Nome = "Moeda de cobre",
                Descricao = "Uma moeda de cobre, vale 1 sense",
                Id = 0,
            };
        }

        public static ItemRPG TesteItem()
        {
            return new ItemRPG(RPG.Entidades.Enuns.TipoItemEnum.Arma, 6)
            {
                Nome = "Espada de teste agora",
                Descricao = "Espada de teste, não viu?",
                Id = 1,
                AtaqueFisico = 100,
                DefesaFisica = 200,
            };
        }
    }
}
