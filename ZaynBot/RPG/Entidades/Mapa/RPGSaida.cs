using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Entidades.Mapa
{
    [BsonIgnoreExtraElements]
    public class RPGSaida
    {
        public EnumDirecoes Direcao { get; set; } 
        public int RegiaoId { get; set; }
        public bool Travado { get; set; } // A porta está travada?
        public bool DestravaComMissao { get; set; } // Destrava com alguma missão concluida?
        public int IdMissao { get; set; } // Qual é essa missão.
        public string TravadoMensagem { get; set; } // Se a porta esta travada, é preciso de uma mensagem.
        
    }
}
