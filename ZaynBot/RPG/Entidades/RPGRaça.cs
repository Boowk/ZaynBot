using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGRaça
    {

        /*
         * Força	Inteligência	Percepção	Destreza	Constituição	Sorte
         */
        [BsonId]
        [BsonRepresentation(BsonType.Int64, AllowTruncation = true)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Forca { get; set; }
        public int Inteligencia { get; set; }
        public int Percepcao { get; set; }
        public int Destreza { get; set; }
        public int Constituicao { get; set; }
        public int Sorte { get; set; }

        public RPGRaça(string nome)
        {
            Nome = nome;
        }
    }
}
