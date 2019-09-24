using System.Collections.Generic;
using ZaynBot.Data.Mobs.Normal;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.RPG.Data.Mundos.Anker
{
    public class AreaAnker
    {
        public RegiaoRPG LugarDesconhecido0()
        {
            RegiaoRPG regiao = new RegiaoRPG
            {
                Id = 0,
                Nome = "Lugar desconhecido - 0",
                Descrição = "Algum lucar",
                UrlImagem = "https://cdn.discordapp.com/attachments/586204969934389268/587699128063230004/caverna.png"
            };

            #region saidas

            SaidaRPG leste = new SaidaRPG
            {
                Direcao = DirecaoEnum.Leste,
                RegiaoId = 1,
            };

            #endregion  
            regiao.SaidasRegioes.Add(leste);

            return regiao;
        }

        public RegiaoRPG LugarDesconhecido1()
        {

            RegiaoRPG regiao = new RegiaoRPG
            {
                Id = 1,
                Nome = "Lugar desconhecido - 1",
                Descrição = "Algum lugar estranho",
                UrlImagem = "https://cdn.discordapp.com/attachments/586204969934389268/587699128063230004/caverna.png"
            };

            regiao.Mobs.Add(CoelhoMob.CoelhoMobAb());

            #region saidas

            SaidaRPG leste = new SaidaRPG
            {
                Direcao = DirecaoEnum.Leste,
                RegiaoId = 2,
            };

            SaidaRPG norte = new SaidaRPG
            {
                Direcao = DirecaoEnum.Norte,
                RegiaoId = 3,
            };

            #endregion
            //regiao.SaidasRegioes.Add(leste);
            //regiao.SaidasRegioes.Add(norte);
            return regiao;
        }
    }
}
