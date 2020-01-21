using MongoDB.Bson.Serialization.Attributes;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGMob
    {
        public string Nome { get; set; }
        public double VidaAtual { get; set; }
        public double VidaMax { get; set; }
        public double AtaqueFisico { get; set; }

        public RPGMob() { }

        public RPGMob(double vida)
        {
            VidaAtual = vida;
            VidaMax = vida;
        }
    }
}
