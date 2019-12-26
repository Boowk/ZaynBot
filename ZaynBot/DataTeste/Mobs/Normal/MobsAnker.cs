using ZaynBot.RPG.Entidades;

namespace ZaynBot.DataTeste.Mobs.Normal
{
    public static class MobsAnker
    {
        public static RPGMob MobLadrao()
        {
            RPGMob mob = new RPGMob(25)
            {
                Nome = "Ladrão",
                AtaqueFisico = 4,
                Armadura = 0,
                Velocidade = 4,
                Dificuldade = 1,
                Essencia = 5,
            };

            mob.Drop = new MobItemDropRPG(0, 2);
            return mob;
        }

        public static RPGMob MobLadrao1()
        {
            RPGMob mob = new RPGMob(25)
            {
                Nome = "Ladrão",
                AtaqueFisico = 4,
                Armadura = 1,
                Velocidade = 4,
                Dificuldade = 1,
                Essencia = 6,
            };

            mob.Drop = new MobItemDropRPG(7, 1);
            return mob;
        }

        public static RPGMob MobRato()
        {
            RPGMob mob = new RPGMob(15)
            {
                Nome = "Rato",
                AtaqueFisico = 2,
                Armadura = 1,
                Velocidade = 4,
                Dificuldade = 1,
                Essencia = 2,
            };

            mob.Drop = new MobItemDropRPG(2, 1);
            return mob;
        }

        public static RPGMob MobCoelho()
        {
            RPGMob mob = new RPGMob(15)
            {
                Nome = "Coelho",
                AtaqueFisico = 3,
                Armadura = 1,
                Velocidade = 4,
                Dificuldade = 1,
                Essencia = 3,
            };

            mob.Drop = new MobItemDropRPG(3, 1);
            return mob;
        }

        public static RPGMob MobCoelho1()
        {
            RPGMob mob = new RPGMob(15)
            {
                Nome = "Coelho",
                AtaqueFisico = 3,
                Armadura = 1,
                Velocidade = 4,
                Dificuldade = 1,
                Essencia = 3,
            };

            mob.Drop = new MobItemDropRPG(2, 1);
            return mob;
        }

        public static RPGMob MobPato()
        {
            RPGMob mob = new RPGMob(10)
            {
                Nome = "Pato",
                AtaqueFisico = 6,
                Armadura = 0,
                Velocidade = 4,
                Dificuldade = 1,
                Essencia = 4,
            };

            mob.Drop = new MobItemDropRPG(2, 1);
            return mob;
        }

        public static RPGMob MobVaca()
        {
            RPGMob mob = new RPGMob(25)
            {
                Nome = "Vaca",
                AtaqueFisico = 3,
                Armadura = 3,
                Velocidade = 4,
                Dificuldade = 1,
                Essencia = 5,
            };

            mob.Drop = new MobItemDropRPG(5, 1);
            return mob;
        }

        public static RPGMob MobVaca1()
        {
            RPGMob mob = new RPGMob(25)
            {
                Nome = "Vaca",
                AtaqueFisico = 3,
                Armadura = 3,
                Velocidade = 4,
                Dificuldade = 1,
                Essencia = 5,
            };

            mob.Drop = new MobItemDropRPG(2, 1);
            return mob;
        }

        public static RPGMob MobVaca2()
        {
            RPGMob mob = new RPGMob(25)
            {
                Nome = "Vaca",
                AtaqueFisico = 3,
                Armadura = 3,
                Velocidade = 4,
                Dificuldade = 1,
                Essencia = 5,
            };

            mob.Drop = new MobItemDropRPG(4, 1);
            return mob;
        }

        public static RPGMob MobGalinha()
        {
            RPGMob mob = new RPGMob(15)
            {
                Nome = "Galinha",
                AtaqueFisico = 3,
                Armadura = 0,
                Velocidade = 4,
                Dificuldade = 1,
                Essencia = 3,
            };

            mob.Drop = new MobItemDropRPG(2, 1);
            return mob;
        }

        public static RPGMob MobGalinha1()
        {
            RPGMob mob = new RPGMob(15)
            {
                Nome = "Galinha",
                AtaqueFisico = 3,
                Armadura = 0,
                Velocidade = 4,
                Dificuldade = 1,
                Essencia = 3,
            };

            mob.Drop = new MobItemDropRPG(8, 1);
            return mob;
        }

        public static RPGMob MobGalinha2()
        {
            RPGMob mob = new RPGMob(15)
            {
                Nome = "Galinha",
                AtaqueFisico = 3,
                Armadura = 0,
                Velocidade = 4,
                Dificuldade = 1,
                Essencia = 3,
            };

            mob.Drop = new MobItemDropRPG(6, 1);
            return mob;
        }
    }
}
