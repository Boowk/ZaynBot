using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.Core.Entidades
{
    [BsonIgnoreExtraElements]
    public class ServidorCore
    {
        [BsonId]
        public ulong Id { get; set; }
        public string Prefix { get; set; }
    }
}
