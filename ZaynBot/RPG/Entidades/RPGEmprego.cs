using MongoDB.Bson.Serialization.Attributes;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class Emprego
    {
        public string Nome { get; }
        public int Nivel { get; }

        public Emprego(string nome, int nivel = 1)
        {
            Nome = nome;
            Nivel = nivel;
        }
    }
}
