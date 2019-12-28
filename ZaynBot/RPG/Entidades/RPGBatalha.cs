using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGBatalha
    {
        public int Turno { get; set; }

        public RPGMob Mob { get; set; }

        public RPGBatalha()
        {
            Turno = 0;
            Mob = new RPGMob(0);
        }
    }
}