﻿using MongoDB.Bson.Serialization.Attributes;
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
        public double EstaminaAtual { get; set; } = 100;
        public double EstaminaMaxima { get; set; } = 100;

        public string Nome { get; set; }
        public double VidaAtual { get; set; }
        public double VidaMax { get; set; }
        public double AtaqueFisico { get; set; }
        public double Armadura { get; set; }
        public double Velocidade { get; set; }
        public double Essencia { get; set; }
        public int Dificuldade { get; set; }

        public MobItemDropRPG Drop { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class MobItemDropRPG
    {
        public MobItemDropRPG() { }
        public MobItemDropRPG(int itemId, int quantidadeMaxima)
        {
            this.ItemId = itemId;
            this.QuantMax = quantidadeMaxima;
        }

        public MobItemDropRPG(int itemId)
        {
            this.ItemId = itemId;
            this.QuantMax = 1;
        }

        public int ItemId { get; set; }
        public int QuantMax { get; set; }
    }
}
