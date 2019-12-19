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
            RPGMob mob = new RPGMob(60)
            {
                Nome = "Coelho",
                AtaqueFisico = 6,
                Armadura = 0,
                Velocidade = 6,
                Dificuldade = 1,
                Essencia = 15,
            };

            mob.Drop = new MobItemDropRPG(2, 1);
            return mob;
        }

        public static RPGMob SlimeMobAb()
        {
            RPGMob mob = new RPGMob(60)
            {
                Nome = "Slime",
                AtaqueFisico = 5,
                Armadura = 0,
                Velocidade = 4,
                Dificuldade = 1,
                Essencia = 10,
            };

            mob.Drop = new MobItemDropRPG(0, 6);
            return mob;
        }

        public static RPGMob EsqueletoMobAb()
        {
            RPGMob mob = new RPGMob(60)
            {
                Nome = "Esqueleto",
                AtaqueFisico = 10,
                Armadura = 0,
                Velocidade = 4,
                Dificuldade = 1,
                Essencia = 40,
            };

            mob.Drop = new MobItemDropRPG(2, 1);
            return mob;
        }
    }
}
