using MongoDB.Bson.Serialization.Attributes;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGBatalha
    {
        public RPGMob Mob { get; set; }

        public RPGBatalha()
        {
            this.Mob = new RPGMob(0);
        }

        public RPGBatalha(RPGMob mob)
        {
            this.Mob = mob;
        }
    }
}