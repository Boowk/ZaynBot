using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGMob
    {
        public RPGMob() { }
        public RPGMob(double vida)
        {
            VidaAtual = vida;
            VidaMax = vida;
        }
        public double EstaminaAtual { get; set; } = 0;
        public double EstaminaMaxima { get; set; } = 100;

        public string Nome { get; set; }
        public string Descricao { get; set; }

        public double VidaAtual { get; set; }
        public double VidaMax { get; set; }

        public double AtaqueFisico { get; set; }
        public double DefesaFisica { get; set; }


        public double Velocidade { get; set; }
        public double Essencia { get; set; }
        public int Dificuldade { get; set; }

        //Drops
        public int ChanceDropTotal { get; set; }
        public List<MobItemDropRPG> Drops { get; set; }

        public MobItemSorteadoRPG Item { get; set; }

        public MobItemDropRPG SortearDrop()
        {
            var rand = Sortear.Valor(0, ChanceDropTotal);
            var top = 0;
            for (int i = 0; i < Drops.Count; i++)
            {
                top += Drops[i].ChanceDrop;
                if (rand <= top)
                    return Drops[i];
            }
            return null;
        }
    }

    [BsonIgnoreExtraElements]
    public class MobItemDropRPG
    {
        public int ItemId { get; set; }
        public int QuantMax { get; set; }
        public int ChanceDrop { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class MobItemSorteadoRPG
    {
        public int QuantidadeRestante { get; set; }
        public int ItemID { get; set; }
    }
}
