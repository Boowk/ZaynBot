using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.RPG
{
    public static class Sortear
    {
        public static int Valor(int min, int max)
        {
            Random rd = new Random();
            return rd.Next(min, max);
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
    }
}
