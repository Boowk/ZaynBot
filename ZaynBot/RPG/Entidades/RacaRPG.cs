using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.Collections.Generic;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RacaRPG
    {
        [BsonId]
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Forca { get; set; }
        public int Inteligencia { get; set; }
        public int ForcaDeVontade { get; set; }
        public int Agilidade { get; set; }
        public int Resistencia { get; set; }
        public int Pontos { get; set; }

        public RacaRPG(int id, string nome, int forca, int inteligencia, int forcaDeVontade, int agilidade, int resistencia)
        {
            this.Id = id;
            this.Nome = nome;
            this.Forca = forca;
            this.Inteligencia = inteligencia;
            this.ForcaDeVontade = forcaDeVontade;
            this.Agilidade = agilidade;
            this.Resistencia = resistencia;
            this.Pontos = 0;
        }
    }
}
