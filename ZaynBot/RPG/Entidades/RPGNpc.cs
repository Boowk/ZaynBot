using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGNpc
    {
        public string Nome { get; set; } //Para encontra-lo
        public string FalaInicial { get; set; }
        public bool Visivel { get; set; } = true; // Aparece no comando localizacao?

        public bool FalarComSomenteSemMissaoConcluida { get; set; }
        public int FalarComSomenteSemMissaoConcluidaId { get; set; }

        public List<RPGNpcPergunta> Perguntas { get; set; } = new List<RPGNpcPergunta>();
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<string, NpcVenda> ItensAVenda { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class NpcVenda
    {
        public int Preco { get; set; }
        public RPGItem Item { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class RPGNpcPergunta
    {
        public string Pergunta { get; set; }
        public string Resposta { get; set; }
        public bool Loja { get; set; } = false;
        public bool Missao { get; set; } = false;
        public int MissaoId { get; set; } = -1;
        public string MissaoAtivaPergunta { get; set; } = "O que era para eu fazer mesmo?";
    }
}
