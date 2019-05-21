using MongoDB.Bson.Serialization.Attributes;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGEmprego
    {
        public string Nome { get; }
        public int Nivel { get; }

        public RPGEmprego(string nome, int nivel = 1)
        {
            Nome = nome;
            Nivel = nivel;
        }
    }
}
