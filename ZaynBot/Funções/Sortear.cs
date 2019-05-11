using System;

namespace ZaynBot.Funções
{
    public class Sortear
    {
        private Random random;

        public int Valor(int min, int max)
        {
            random = new Random();
            return random.Next(min, max + 1);
        }

        public long Valor(long min, long max)
        {
            random = new Random();
            return (long)random.NextDouble() * (max - min) + min;
        }

        public double Valor(double min, double max)
        {
            random = new Random();
            return random.NextDouble() * (max - min) + min;
        }

        public float Valor(float min, float max)
        {
            random = new Random();
            return (float)random.NextDouble() * (max - min) + min;
        }
    }
}
