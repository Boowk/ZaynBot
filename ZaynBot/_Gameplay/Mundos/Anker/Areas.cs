using System.Collections.Concurrent;
using ZaynBot.Entidades.EntidadesRpg.Mapa;

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

        public static Região GetRegiao(int id)
        {
            Região r = new Região()
            {
                Descrição = Regiões[id].Descrição,
                Inimigos = Regiões[id].Inimigos,
                RegiaoId = Regiões[id].RegiaoId,
                RegiaoNome = Regiões[id].RegiaoNome,
                Saidas = Regiões[id].Saidas,
                Terreno = Regiões[id].Terreno,
            };
            return r;
        }
    }
}
