using System;
using System.Collections.Generic;
using System.Text;
using ZaynBot.Data.Itens;
using ZaynBot.RPG.Entidades;
using static ZaynBot.RPG.Entidades.RPGMob;

namespace ZaynBot.DataTeste.Mobs.Normal
{
    public static class MobsAnker
    {
        public static RPGMob CoelhoMobAb()
        {
            RPGMob mob = new RPGMob()
            {
                Nome = "Coelho",
                AtaqueFisico = 6,
                Armadura = 0,
                PontosDeVida = 12,
                Velocidade = 6,
                ChanceDeAparecer = 50,
                Dificuldade = 1,
                Essencia = 15,
            };

            mob.ChanceItemTodos.Add(new MobItemDropRPG(2, 1, 1));
            mob.ChanceItemTodos.Add(new MobItemDropRPG(5, 1, 1));
            return mob;
        }

        public static RPGMob SlimeMobAb()
        {
            RPGMob mob = new RPGMob()
            {
                Nome = "Slime",
                AtaqueFisico = 5,
                Armadura = 0,
                PontosDeVida = 6,
                Velocidade = 4,
                ChanceDeAparecer = 50,
                Dificuldade = 1,
                Essencia = 10,
            };

            mob.ChanceItemTodos.Add(new MobItemDropRPG(0, 6, 0.5));
            mob.ChanceItemTodos.Add(new MobItemDropRPG(3, 1, 1));
            return mob;
        }

        public static RPGMob EsqueletoMobAb()
        {
            RPGMob mob = new RPGMob()
            {
                Nome = "Esqueleto",
                AtaqueFisico = 10,
                Armadura = 0,
                PontosDeVida = 10,
                Velocidade = 4,
                ChanceDeAparecer = 50,
                Dificuldade = 1,
                Essencia = 40,
            };

            mob.ChanceItemTodos.Add(new MobItemDropRPG(2, 1, 1));
            mob.ChanceItemTodos.Add(new MobItemDropRPG(0, 1, 0.3));
            mob.ChanceItemUnico.Add(new MobItemDropRPG(1, 1, 0.2));
            return mob;
        }
    }
}
