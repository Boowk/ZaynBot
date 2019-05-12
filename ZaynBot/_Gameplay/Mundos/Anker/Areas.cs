using System.Collections.Concurrent;
using System.Collections.Generic;
using ZaynBot._Gameplay.Raças;
using ZaynBot.Entidades.EntidadesRpg;
using ZaynBot.Entidades.EntidadesRpg.Mapa;

namespace ZaynBot._Gameplay.Mundos.Anker
{
    public class Areas
    {
        // Para fazer:
        // Gerar em memoria, salvar em HD e depois deletar tudo da memoria.
        private static List<Região> Regiões;

        public Areas()
        {
            // Deleta todas as zonas
            Banco.DeletarRegions();
            // Adiciona as zonas na lista
            Regiões = new List<Região>
            {
                Anker.AnkarEstrada(),
                Anker.AnkarEstrada2()
            };
            // Salva as zonas na HD
            foreach (var item in Regiões)
            {
                Banco.AdicionarRegions(item);
            }
            // Limpa a lista para liberar memoria
            Regiões = null;
        }
    }
}
