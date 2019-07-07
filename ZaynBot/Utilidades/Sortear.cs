using System;
using System.Collections.Generic;
using System.Threading;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.Utilidades
{
    public class Sortear
    {
        private Random _random;

        public int Valor(int min, int max)
        {
            _random = new Random();
            return _random.Next(min, max + 1);
        }

        public long Valor(long min, long max)
        {
            _random = new Random();
            return (long)_random.NextDouble() * (max - min) + min;
        }

        public double Valor(double min, double max)
        {
            _random = new Random();
            return _random.NextDouble() * (max - min) + min;
        }

        public float Valor(float min, float max)
        {
            _random = new Random();
            return (float)_random.NextDouble() * (max - min) + min;
        }

        public bool Sucesso(double probabilidade)
        {
            _random = new Random();
            return _random.NextDouble() < probabilidade;
        }


        public RPGMob ListaMob(List<RPGMob> mobs)
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


        //public static List<RPGItem> SortearItemUnico(List<RPGItemDrop> itens)
        //{
        //    foreach (var item in itens)
        //    {
        //        List<ItemChanceCair> itemQuePodeCair = new List<ItemChanceCair>();
        //        itemQuePodeCair.Add(itemComChanceDeCair);
        //        itemQuePodeCair.Add(new ItemChanceCair
        //        {
        //            ChanceDeCair = 100 - itemComChanceDeCair.ChanceDeCair,
        //            QuantidadeMinima = -100,
        //        });

        //        Random r = new Random();
        //        int sorteio = r.Next(0, 100);
        //        int posicaoEscolhida = -1;
        //        do
        //        {
        //            posicaoEscolhida++;
        //            sorteio -= itemQuePodeCair[posicaoEscolhida].ChanceDeCair;
        //        } while (sorteio > 0);

        //        if (itemQuePodeCair[posicaoEscolhida].QuantidadeMinima != -100)
        //        {
        //            Item objeto = itemQuePodeCair[posicaoEscolhida].Item.Clone();
        //            if (itemQuePodeCair[posicaoEscolhida].QuantidadeMaxima == 1)
        //            {
        //                objeto.Quantidade = 1;
        //            }
        //            else
        //            {
        //                objeto.Quantidade = Sortear(itemQuePodeCair[posicaoEscolhida].QuantidadeMinima, itemQuePodeCair[posicaoEscolhida].QuantidadeMaxima);
        //            }
        //            itensQueCaiu.Add(objeto);
        //        }
        //    }
        //    return itensQueCaiu;
        //}
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
