using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGItem
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public EnumTipoItem TipoItem { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public int Quantidade { get; set; } = 1;
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float PrecoBase { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float Peso { get; set; }

        public bool CompletaMissaoAoPegar { get; set; }
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public int CompletaMissaoAoPegarId { get; set; }

        public bool PegarSomenteComMissaoEmAndamento { get; set; }
        public string PegarSomenteComMissaoEmAndamentoMensagem { get; set; }
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public int PegarSomenteComMissaoEmAndamentoId { get; set; }

        public bool DesapareceAoPegar { get; set; }
        public string DesapareceAoPegarMensagem { get; set; }

        public RPGItem Clone()
              => (RPGItem)MemberwiseClone();
    }
}
