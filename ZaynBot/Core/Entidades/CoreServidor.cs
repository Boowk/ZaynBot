using DSharpPlus.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.Core.Entidades
{
    public class CoreServidor
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int64, AllowTruncation = true)]
        public ulong Id { get; set; }
        public List<ulong> IdCargos { get; set; } = new List<ulong>();

        public CoreServidor(ulong id)
        {
            Id = id;
        }
    }
}
