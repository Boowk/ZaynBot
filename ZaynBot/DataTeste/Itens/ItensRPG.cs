﻿using System;
using System.Collections.Generic;
using System.Text;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.Data.Itens
{
    public class ItensRPG
    {
        public static RPGItem MoedaDeCobreItem()
            => new RPGItem(0, "Moeda de cobre", TipoItemEnum.Moeda)
            {
                Descricao = "Uma moeda de cobre, vale 1 sense.",
            };


        public static RPGItem EspadaEnferrujadaItem()
            => new RPGItem(1, "Espada enferrujada de bronze", TipoItemEnum.Arma, 100, 2, TipoExpEnum.Perfurante)
            {
                Descricao = "Uma espada de bronze muito desgastada, devido ao uso constante.",
                AtaqueFisico = 20,
            };

        public static RPGItem Ossositem()
            => new RPGItem(2, "Ossos", TipoItemEnum.Recurso, 1)
            {
                Descricao = "Ossos.. isso dá um arrepio."
            };


        public static RPGItem GosmaItem()
            => new RPGItem(3, "Gosma", TipoItemEnum.Recurso, 3)
            {
                Descricao = "Muito gosmento, onde gruda não sai com facilidade."
            };

        public static RPGItem CarneDeCoelhoAssada()
            => new RPGItem(4, "Carne de Coelho Assada", TipoItemEnum.Usavel, 4)
            {
                Descricao = "Uma carne bem cheirosa!",
                FomeRestaura = 2,
                VidaRestaura = 5,
            };

        public static RPGItem CarneDeCoelho()
            => new RPGItem(5, "Carne de Coelho Cru", TipoItemEnum.Recurso, 2)
            {
                Descricao = "Ainda fresco."
            };

        public static RPGItem OssoAfiado()
            => new RPGItem(6, "Osso afiado", TipoItemEnum.Arma, 50, 0, TipoExpEnum.Perfurante)
            {
                Descricao = "Meio nojento, mas é bem afiado.",
                AtaqueFisico = 10,
            };
    }
}
