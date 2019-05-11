using System;

namespace ZaynBot.Funções
{
    public class Sortear
    {
        private static Random random;

        public static int Valor(int min, int max)
        {
            random = new Random();
            return random.Next(min, max + 1);
        }

        public static long Valor(long min, long max)
        {
            random = new Random();
            return (long)random.NextDouble() * (max - min) + min;
        }

        public static double Valor(double min, double max)
        {
            random = new Random();
            return random.NextDouble() * (max - min) + min;
        }
    }
}
