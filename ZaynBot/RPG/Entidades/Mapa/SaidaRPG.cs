using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Entidades.Mapa
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
        public DirecaoEnum Direcao { get; set; }
        public int RegiaoId { get; set; }
    }
}
