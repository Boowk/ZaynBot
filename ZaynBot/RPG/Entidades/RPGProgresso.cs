﻿using MongoDB.Bson.Serialization.Attributes;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGProgresso
    {
        public int NivelAtual { get; set; }
        public double ExpAtual { get; set; }
        public double ExpMax { get; set; }

        private RPGProgresso()
        {
            ExpAtual = 0;
            NivelAtual = 1;
        }

        public RPGProgresso(double expMax) : this()
        {
            ExpMax = expMax;
        }

        public int AdicionarExp(double exp)
        {
            double expResultante = ExpAtual + exp;
            int quantEvo = 0;
            if (expResultante >= ExpMax)
            {
                do
                {
                    double quantosPrecisaProxNivel = expResultante - ExpMax;
                    Evoluir();
                    quantEvo++;
                    expResultante = quantosPrecisaProxNivel;
                } while (expResultante >= ExpMax);
                ExpAtual += expResultante;
                return quantEvo;
            }
            ExpAtual += exp;
            return quantEvo;
        }

        public void Evoluir()
        {
            NivelAtual++;
            ExpMax *= 1.02;
            ExpAtual = 0;
        }

        public double Evoluir(double valorOriginal)
        {
            return valorOriginal * 1.02;
        }
    }
}