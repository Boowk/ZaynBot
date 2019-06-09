using MongoDB.Bson.Serialization.Attributes;

namespace ZaynBot.RPG.Entidades.Mapa
{
    [BsonIgnoreExtraElements]
    public class RPGSaida
    {
        public EnumDirecoes Direcao { get; set; }
        public int RegiaoId { get; set; }
    }
}
