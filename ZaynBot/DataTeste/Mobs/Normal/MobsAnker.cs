using System;
using System.Collections.Generic;
using System.Text;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.DataTeste.Mobs.Normal
{
    public static class MobsAnker
    {
        public static MobRPG CoelhoMobAb()
        {
            MobRPG mob = new MobRPG()
            {
                Nome = "Coelho",
                AtaqueFisico = 6,
                Armadura = 0,
                PontosDeVida = 12,
                Velocidade = 6,
                ChanceDeAparecer = 50,
                Essencia = 15,
            };

            // Carne de coelho
            //mob.ChanceItemTodos.Add(new MobItemDropRPG
            //{
            //    ItemId = 0,
            //    ChanceDeCair = 0.5f,
            //    QuantidadeMaxima = 1,
            //});
            //mob.ChanceItemTodos.Add(new MobItemDropRPG
            //{
            //    ItemId = 1,
            //    ChanceDeCair = 0.5f,
            //    QuantidadeMaxima = 2,
            //});
            //mob.ChanceItemUnico.Add(new MobItemDropRPG
            //{
            //    ItemId = 0,
            //    ChanceDeCair = 1f,
            //    QuantidadeMaxima = 2,
            //});
            return mob;
        }

        public static MobRPG SlimeMobAb()
        {
            MobRPG mob = new MobRPG()
            {
                Nome = "Slime",
                AtaqueFisico = 5,
                Armadura = 0,
                PontosDeVida = 6,
                Velocidade = 4,
                ChanceDeAparecer = 50,
                Essencia = 10,
            };
            return mob;
        }

        public static MobRPG EsqueletoMobAb()
        {
            MobRPG mob = new MobRPG()
            {
                Nome = "Esqueleto",
                AtaqueFisico = 10,
                Armadura = 0,
                PontosDeVida = 10,
                Velocidade = 4,
                ChanceDeAparecer = 50,
                Essencia = 40,
            };
            return mob;
        }
    }
}
