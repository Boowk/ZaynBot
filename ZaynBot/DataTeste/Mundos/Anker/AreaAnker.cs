using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Data.Mundos.Anker
{
    public static class AreaAnker
    {
        public static RPGRegiao CidadePrincipal()
        {
            RPGRegiao regiao = new RPGRegiao
            {
                Id = 0,
                Nome = "Centro da cidade",
                Descrição = "Onde todos aparecem...",
                Dificuldade = 1
            };
            //RPGSaida leste = new RPGSaida(DirecaoEnum.Leste, 1);
            //regiao.SaidasRegioes.Add(leste);

            regiao.LojaItensId.Add(7);
            return regiao;
        }
    }
}
