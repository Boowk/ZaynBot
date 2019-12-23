using ZaynBot.RPG.Entidades;

namespace ZaynBot.DataTeste.Mobs.Normal
{
    public static class MobsAnker
    {
        public static RPGMob LadraoMob()
        {
            RPGMob mob = new RPGMob(25)
            {
                Nome = "Ladrão",
                AtaqueFisico = 10,
                Armadura = 0,
                Velocidade = 4,
                Dificuldade = 1,
                Essencia = 5,
            };

            mob.Drop = new MobItemDropRPG(0, 2);
            return mob;
        }
    }
}
