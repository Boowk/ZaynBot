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
        public int Quantidade { get; set; } = 1;
        public float PrecoBase { get; set; }

        public bool CompletaMissaoAoPegar { get; set; }
        public int CompletaMissaoAoPegarId { get; set; }

        public bool PegarSomenteComMissaoEmAndamento { get; set; }
        public string PegarSomenteComMissaoEmAndamentoMensagem { get; set; }
        public int PegarSomenteComMissaoEmAndamentoId { get; set; }

        public bool DesapareceAoPegar { get; set; }
        public string DesapareceAoPegarMensagem { get; set; }
    }
}
