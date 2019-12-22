using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGRegiao
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
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public int Dificuldade { get; set; }

        public List<RPGSaida> SaidasRegioes { get; set; } = new List<RPGSaida>();
        public List<int> LojaItensId { get; set; } = new List<int>();


        public static RPGRegiao GetRPGRegiao(int id)
            => ModuloBanco.GetRegiaoData(id);

    }
}
