using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Entidades
{
    public enum DirecaoEnum
    {
        Norte,
        Sul,
        Oeste,
        Leste,
    }

    public class RPGSaida
    {
        public RPGSaida(DirecaoEnum direcao, int regiaoId)
        {
            this.Direcao = direcao;
            this.RegiaoId = regiaoId;
        }

        public DirecaoEnum Direcao { get; set; }
        public int RegiaoId { get; set; }
    }
}
