using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using ZaynBot.Entidades.EntidadesRpg.EntidadesRpgMapa;

namespace ZaynBot._Gameplay.Mundos.Anker
{
    public class Areas
    {
        public static ConcurrentDictionary<int, Região> Regiões;

        public Areas()
        {
            Regiões = new ConcurrentDictionary<int, Região>();
            //AdicionarArea(Superficie.ArmazemGeral18Ab());
        }

        private static void AddLista(List<Região> lista)
        {
            foreach (var regs in lista)
            {
                Add(regs);
            }
        }

        private static void Add(Região regiao)
        {
            Regiões.TryAdd(regiao.RegiaoId, regiao);
        }
    }
}
