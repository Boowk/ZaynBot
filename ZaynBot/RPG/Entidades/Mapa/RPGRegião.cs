using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades.Mapa
{
    [BsonIgnoreExtraElements]
    public class RPGRegião
    {
        //CIDADE               |   1  | 
        //CAMPO                |   2  | 
        //FLORESTA             |   3  | 
        //COLINAS              |   4  | 
        //MONTANHAS            |   5  |
        //MAR OU RIO           |   6  |    
        //AR                   |   7  |
        //DESERTO              |   8  |
        //DESCONHECIDO         |   9  |    
        //PROIBIDO
        //public enum Tipo
        //{
        //    Cidade,
        //    Campo,
        //    Floresta,
        //    Colina,
        //    Montanha,
        //    Agua,
        //    Ar,
        //    Deserto,
        //    Desconhecido,
        //    Proibido
        //}

        [BsonId]
        [BsonRepresentation(BsonType.Int64, AllowTruncation = true)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public List<RPGSaida> SaidasRegioes { get; set; } = new List<RPGSaida>();
        public List<RPGSaida> Entrada { get; set; } = new List<RPGSaida>();
        public List<RPGNpc> Npcs { get; set; } = new List<RPGNpc>();
        public List<RPGMob> Inimigos { get; set; } = new List<RPGMob>();
    }
}
