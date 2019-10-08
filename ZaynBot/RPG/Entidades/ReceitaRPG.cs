using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class ReceitaRPG
    {
        [BsonId]
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Quantidade { get; set; }
        public int Resultado { get; set; }
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<int, int> Ingredientes { get; set; }


        public ReceitaRPG(int id, string nome, int resultado, int quantidade = 1)
        {
            Id = id;
            Nome = nome;
            Quantidade = quantidade;
            Resultado = resultado;
            Ingredientes = new Dictionary<int, int>();
        }
    }
}
