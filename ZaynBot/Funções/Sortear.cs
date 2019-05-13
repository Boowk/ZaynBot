using System;
using System.Collections.Generic;
using System.Threading;
using ZaynBot.Entidades.EntidadesRpg;
using static ZaynBot.Funções.Sortear;

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

        public Mob ListaMob(List<Mob> mobs)
        {
            mobs.Shuffle();     //Randomiza os mobs para ter chance de cair qualquer um
            int somaPeso = 0;    //Quantidade Max
            foreach (var item in mobs)
            {
                somaPeso += item.ChanceDeAparecer;
            }

            int sorteio = Valor(0, somaPeso);     //Soteia um valor aleatorio entre 0 e a quantidade max
            int posicaoEscolhida = -1;
            do
            {
                posicaoEscolhida++;
                sorteio -= mobs[posicaoEscolhida].ChanceDeAparecer;
            } while (sorteio > 0);

            //Retorna posiçãoescolhida
            return mobs[posicaoEscolhida];
        }
    }

    public static class ThreadSafeRandom
    {
        [ThreadStatic] private static Random Local;

        public static Random ThisThreadsRandom
        {
            get { return Local ?? (Local = new Random(unchecked(Environment.TickCount * 31 + Thread.CurrentThread.ManagedThreadId))); }
        }
    }

    public static class Extensões
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
