using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Entidades.Mapa
{
    [BsonIgnoreExtraElements]
    public class RPGSaida
    {
        public EnumDirecoes Direcao { get; set; }
        public int RegiaoId { get; set; }
        public bool Travado { get; set; }
        public bool DestravaMissao { get; set; }
        public int IdMissaoDestravarPorta { get; set; }
    }
}
