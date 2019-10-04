using System;
using System.Collections.Generic;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.Utilidades
{
    public static class Sortear
    {
        public static int Valor(int min, int max)
        {
            Random rd = new Random();
            return rd.Next(min, max + 1);
        }

        public static long Valor(long min, long max)
        {
            Random rd = new Random();
            return (long)rd.NextDouble() * (max - min) + min;
        }

        public static double Valor(double min, double max)
        {
            Random rd = new Random();
            return rd.NextDouble() * (max - min) + min;
        }

        public static float Valor(float min, float max)
        {
            Random rd = new Random();
            return (float)rd.NextDouble() * (max - min) + min;
        }

        public static bool Sucesso(double probabilidade)
        {
            Random rd = new Random();
            return rd.NextDouble() < probabilidade;
        }


        public static MobRPG Mobs(List<MobRPG> mobs)
        {
            int somaPeso = 0;
            foreach (var item in mobs)
                somaPeso += item.ChanceDeAparecer;
            int sorteio = Valor(0, somaPeso);
            int posicaoEscolhida = -1;
            do
            {
                posicaoEscolhida++;
                sorteio -= mobs[posicaoEscolhida].ChanceDeAparecer;
            } while (sorteio > 0);
            return mobs[posicaoEscolhida];
        }
    }
}
