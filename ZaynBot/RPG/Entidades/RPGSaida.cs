using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Entidades
{
    public enum EnumDirecao
    {
        Norte,
        Sul,
        Oeste,
        Leste,
    }

    public class RPGSaida
    {
        public EnumDirecao Direcao { get; set; }
        public int RegiaoId { get; set; }

        public RPGSaida(EnumDirecao direcao, int regiaoId)
        {
            Direcao = direcao;
            RegiaoId = regiaoId;
        }
    }
}
