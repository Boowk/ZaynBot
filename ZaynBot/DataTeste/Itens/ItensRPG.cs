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
            => new ItemRPG(0, "Moeda de cobre", TipoItemEnum.Moeda)
            {
                Descricao = "Uma moeda de cobre, vale 1 sense.",
            };


        public static ItemRPG EspadaEnferrujadaItem()
            => new ItemRPG(1, "Espada enferrujada de bronze", TipoItemEnum.Arma, 100, 2, TipoExpEnum.Perfurante)
            {
                Descricao = "Uma espada de bronze muito desgastada, devido ao uso constante.",
                AtaqueFisico = 20,
            };

        public static ItemRPG Ossositem()
            => new ItemRPG(2, "Ossos", TipoItemEnum.Recurso, 1)
            {
                Descricao = "Ossos.. isso dá um arrepio."
            };


        public static ItemRPG GosmaItem()
            => new ItemRPG(3, "Gosma", TipoItemEnum.Recurso, 3)
            {
                Descricao = "Muito gosmento, onde gruda não sai com facilidade."
            };

        public static ItemRPG CarneDeCoelhoAssada()
            => new ItemRPG(4, "Carne de Coelho Assada", TipoItemEnum.Usavel, 4)
            {
                Descricao = "Uma carne bem cheirosa!",
                FomeRestaura = 2,
                VidaRestaura = 5,
            };

        public static ItemRPG CarneDeCoelho()
            => new ItemRPG(5, "Carne de Coelho Cru", TipoItemEnum.Recurso, 2)
            {
                Descricao = "Ainda fresco."
            };

        public static ItemRPG OssoAfiado()
            => new ItemRPG(6, "Osso afiado", TipoItemEnum.Arma, 50, 0, TipoExpEnum.Perfurante)
            {
                Descricao = "Meio nojento, mas é bem afiado.",
                AtaqueFisico = 10,
            };
    }
}
