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
        public string TravadoMensagem { get; set; } // Se a porta esta travada, mensagem.
        public bool TravadoSemItemInventario { get; set; } // A porta está travada por que não tem um item no inventario?
        public RPGItem TravadoItem { get; set; } // Qual é o item? Que precisa estar no inventario.


        public bool DestravaComMissaoConcluida { get; set; } // Destrava com alguma missão concluida?
        public int DestravaComMissaoConcluidaId { get; set; } // Qual é essa missão.


        public bool DestravaComMissaoEmAndamento { get; set; } // Destrava com alguma missão em andamento?
        public int DestravaComMissaoEmAndamentoId { get; set; } // Qual é essa missão.


    }
}
