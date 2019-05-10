using System.Collections.Concurrent;
using ZaynBot.Entidades.EntidadesRpg.EntidadesRpgMapa;

namespace ZaynBot._Gameplay.Mundos.Anker
{
    public class Areas
    {
        public static ConcurrentDictionary<int, Região> Regiões;

        public Areas()
        {
            Regiões = new ConcurrentDictionary<int, Região>();
            Add(Anker.AnkarEstrada());
            Add(Anker.AnkarEstrada2());
        }

        private static void Add(Região regiao)
        {
            Regiões.TryAdd(regiao.RegiaoId, regiao);
        }
    }
}
