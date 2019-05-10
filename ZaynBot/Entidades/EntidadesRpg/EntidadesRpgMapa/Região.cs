using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.Entidades.EntidadesRpg.EntidadesRpgMapa
{
    public class Região
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

        public int RegiaoId { get; set; }
        [BsonIgnore] public string RegiaoNome { get; set; }
        [BsonIgnore] public string Descrição { get; set; } = "Sem descrição";
        [BsonIgnore] public Tipo Terreno { get; set; }
        [BsonIgnore] public List<Saida> Saidas { get; set; }
        //[BsonIgnore] public List<Npc> Npcs { get; set; }
        //  [JsonIgnore]
        //  public List<GrupoDeInimigo> Inimigos { get; set; } = new List<GrupoDeInimigo>();   
        //public List<Regiao> Conexoes { get; set; }

        //[JsonIgnore]
        //public List<Inimigo> Inimigos { get; set; }

        public Região()
        {
            Saidas = new List<Saida>();
        }

    }
}
