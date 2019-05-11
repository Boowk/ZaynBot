using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace ZaynBot.Entidades.EntidadesRpg
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

    [BsonIgnoreExtraElements]
    public class Convite
    {
        public ulong IdUsuario { get; set; }
        public ObjectId IdGuilda { get; set; }
        public DateTime DataConvidado { get; set; } = DateTime.UtcNow;
    }
}
