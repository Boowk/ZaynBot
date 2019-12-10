﻿using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    public class RPGMob
    {

        public double EstaminaAtual { get; set; } = 100;
        public double EstaminaMaxima { get; set; } = 100;


        public string Nome { get; set; }
        public double PontosDeVida { get; set; }
        public double AtaqueFisico { get; set; }
        public double Armadura { get; set; }
        public int Velocidade { get; set; }
        public int ChanceDeAparecer { get; set; }
        public double Essencia { get; set; }
        public int Dificuldade { get; set; }

        public List<MobItemDropRPG> ChanceItemUnico { get; set; } = new List<MobItemDropRPG>();
        public List<MobItemDropRPG> ChanceItemTodos { get; set; } = new List<MobItemDropRPG>();


        public class MobItemDropRPG
        {
            public MobItemDropRPG(int itemId, int quantidadeMaxima, double chanceDeCair)
            {
                this.ItemId = itemId;
                this.QuantidadeMaxima = quantidadeMaxima;
                this.ChanceDeCair = chanceDeCair;
            }

            public int ItemId { get; set; }
            public int QuantidadeMaxima { get; set; } = 1;
            public double ChanceDeCair { get; set; } = 1; // 100%
        }
    }
}
