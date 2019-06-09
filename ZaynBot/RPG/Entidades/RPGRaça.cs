using MongoDB.Bson.Serialization.Attributes;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGRaça
    {

        /*
         * Força	Inteligência	Percepção	Destreza	Constituição	Sorte
         */
        public string Nome { get; private set; }
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
