using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades.Mapa
{
    [BsonIgnoreExtraElements]
    public class RegiaoRPG
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

        public List<SaidaRPG> SaidasRegioes { get; set; } = new List<SaidaRPG>();
        public List<MobRPG> Mobs { get; set; } = new List<MobRPG>();

        //public NpcRPG

        public static RegiaoRPG GetRPGRegiao(int id)
            => ModuloBanco.RegiaoGet(id);

    }
}
