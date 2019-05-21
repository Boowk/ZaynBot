using MongoDB.Bson.Serialization.Attributes;

namespace ZaynBot.RPG.Entidades.Mapa
{
    [BsonIgnoreExtraElements]
    public class RPGSaida
    {
        public enum Direcoes
        {
            Norte,
            Sul,
            Oeste,
            Leste,
        }

        public Direcoes Direcao { get; set; }
        public int RegiaoId { get; set; }
    }
}
