using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.RPG.Entidades
{
    public class RPGMissao
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int64, AllowTruncation = true)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<ItensObjetivo> ItensObjetivo { get; set; } = new List<ItensObjetivo>();
        public List<InimigosObjetivo> InimigosObjetivos { get; set; } = new List<InimigosObjetivo>();
    }

    public class ItensObjetivo
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
    }

    public class InimigosObjetivo
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
    }
}
