using System.Collections.Generic;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.RPG._Gameplay.Mundos.Anker
{
    public class Areas
    {
        // Para fazer:
        // Gerar em memoria, salvar em HD e depois deletar tudo da memoria.
        private static List<RPGRegião> Regiões;

        public Areas()
        {
            // Deleta todas as zonas
            Banco.DeletarRegions();
            // Adiciona as zonas na lista
            Regiões = new List<RPGRegião>
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
