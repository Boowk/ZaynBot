using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGNpc
    {
        public string Nome { get; set; }
        public string FalaInicio { get; set; }
        public Dictionary<int, string> Variaveis { get; set; }
        public List<NpcLogica> Logica { get; set; }
        public Dictionary<string, NpcVenda> ItensAVenda { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class NpcVenda
    {
        public int Preco { get; set; }
        public RPGItem Item { get; set; }
    }

    [BsonIgnoreExtraElements]
    public class NpcLogica
    {
        public int Pergunta { get; set; }
        public int Respostas { get; set; }
        public bool Loja { get; set; } = false;
    }
}
