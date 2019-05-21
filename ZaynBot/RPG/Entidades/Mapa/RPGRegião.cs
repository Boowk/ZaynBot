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
        public enum Tipo
        {
            Cidade,
            Campo,
            Floresta,
            Colina,
            Montanha,
            Agua,
            Ar,
            Deserto,
            Desconhecido,
            Proibido
        }

        public int Id { get; set; }
        public string RegiaoNome { get; set; }
        public string Descrição { get; set; } = "Sem descrição";
        public Tipo Terreno { get; set; }
        public List<RPGSaida> Saidas { get; set; } = new List<RPGSaida>();
        //[BsonIgnore] public List<Npc> Npcs { get; set; }
        public List<RPGMob> Inimigos { get; set; } = new List<RPGMob>();
        //public List<Regiao> Conexoes { get; set; }

        //[JsonIgnore]
        //public List<Inimigo> Inimigos { get; set; }
    }
}
