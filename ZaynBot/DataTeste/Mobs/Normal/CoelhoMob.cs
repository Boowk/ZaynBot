using ZaynBot.RPG.Entidades;
using static ZaynBot.RPG.Entidades.MobRPG;

namespace ZaynBot.Data.Mobs.Normal
{
    public class CoelhoMob
    {
        static public MobRPG CoelhoMobAb()
        {
            MobRPG mob = new MobRPG()
            {
                Nome = "Coelho",
                AtaqueFisico = 5,
                Armadura = 4,
                PontosDeVida = 10,
                Velocidade = 4,
                ChanceDeAparecer = 50,
                Essencia = 200,
            };

            // Carne de coelho
            mob.ChanceItemTodos.Add(new MobItemDropRPG
            {
                ItemId = 0,
                ChanceDeCair = 0.5f,
                QuantidadeMaxima = 1,
            });
            mob.ChanceItemTodos.Add(new MobItemDropRPG
            {
                ItemId = 1,
                ChanceDeCair = 0.5f,
                QuantidadeMaxima = 2,
            });
            mob.ChanceItemUnico.Add(new MobItemDropRPG
            {
                ItemId = 0,
                ChanceDeCair = 1f,
                QuantidadeMaxima = 2,
            });

            return mob;
        }
    }
}
