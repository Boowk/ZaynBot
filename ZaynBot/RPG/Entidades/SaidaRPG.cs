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

    public class SaidaRPG
    {
        public SaidaRPG(DirecaoEnum direcao, int regiaoId)
        {
            this.Direcao = direcao;
            this.RegiaoId = regiaoId;
        }

        public DirecaoEnum Direcao { get; set; }
        public int RegiaoId { get; set; }
    }
}
