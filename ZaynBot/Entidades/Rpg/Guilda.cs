using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.Entidades.Rpg
{
    [BsonIgnoreExtraElements]
    public class Guilda
    {
        public ObjectId Id { get; set; }
        public ulong IdDono { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<ulong> Membros { get; set; }
        public List<Convite> Convites { get; set; }
    }

    public class Convite
    {
        public ulong IdUsuario { get; set; }
        public ObjectId IdGuilda { get; set; }
        public DateTime DataConvidado { get; set; } = DateTime.UtcNow;
    }
}
