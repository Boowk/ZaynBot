using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGRegiao
    {
        [BsonId]
        public string Nome { get; set; }
        public string Reino { get; set; }
        public string Descricao { get; set; }
        public HashSet<string> Saidas { get; set; }

        public RPGRegiao() { }
    }
}
