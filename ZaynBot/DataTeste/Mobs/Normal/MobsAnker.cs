using ZaynBot.RPG.Entidades;

namespace ZaynBot.DataTeste.Mobs.Normal
{
    public static class MobsAnker
    {
        public static RPGMob CoelhoMobAb()
        {
            RPGMob mob = new RPGMob(15)
            {
                Nome = "Coelho",
                AtaqueFisico = 6,
                Armadura = 0,
                Velocidade = 6,
                Dificuldade = 1,
                Essencia = 15,
            };

            mob.Drop = new MobItemDropRPG(5, 1);
            return mob;
        }

        public static RPGMob SlimeMobAb()
        {
            RPGMob mob = new RPGMob(15)
            {
                Nome = "Slime",
                AtaqueFisico = 5,
                Armadura = 0,
                Velocidade = 4,
                Dificuldade = 1,
                Essencia = 10,
            };

            mob.Drop = new MobItemDropRPG(3, 2);
            return mob;
        }

        public static RPGMob EsqueletoMobAb()
        {
            RPGMob mob = new RPGMob(20)
            {
                Nome = "Esqueleto",
                AtaqueFisico = 10,
                Armadura = 0,
                Velocidade = 4,
                Dificuldade = 1,
                Essencia = 15,
            };

            mob.Drop = new MobItemDropRPG(2, 2);
            return mob;
        }

        public static RPGMob LadraoMob()
        {
            RPGMob mob = new RPGMob(25)
            {
                Nome = "Ladrão",
                AtaqueFisico = 10,
                Armadura = 0,
                Velocidade = 4,
                Dificuldade = 1,
                Essencia = 15,
            };

            mob.Drop = new MobItemDropRPG(0, 2);
            return mob;
        }
    }
}
