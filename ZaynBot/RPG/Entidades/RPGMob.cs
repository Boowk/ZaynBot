using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    public class RPGMob
    {
        public RPGMob(double vida)
        {
            VidaAtual = vida;
            VidaMax = vida;
        }
        public double EstaminaAtual { get; set; } = 100;
        public double EstaminaMaxima { get; set; } = 100;

        public string Nome { get; set; }
        public double VidaAtual { get; set; }
        public double VidaMax { get; set; }
        public double AtaqueFisico { get; set; }
        public double Armadura { get; set; }
        public int Velocidade { get; set; }
        public double Essencia { get; set; }
        public int Dificuldade { get; set; }

        public MobItemDropRPG Drop { get; set; }

        public class MobItemDropRPG
        {
            public MobItemDropRPG(int itemId, int quantidadeMaxima)
            {
                this.ItemId = itemId;
                this.QuantidadeMaxima = quantidadeMaxima;
            }

            public int ItemId { get; set; }
            public int QuantidadeMaxima { get; set; } = 1;
        }
    }
}
