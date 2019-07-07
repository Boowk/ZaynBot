using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using ZaynBot.Utilidades;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGMob
    {
        public string Nome { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float PontosDeVida { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float AtaqueFisico { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float DefesaFisica { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float AtaqueMagico { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float DefesaMagica { get; set; }
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public int Velocidade { get; set; }
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public int ChanceDeAparecer { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float Experiencia { get; set; }
        public List<RPGItemDrop> ChanceItemUnico { get; set; } = new List<RPGItemDrop>();
        public List<RPGItemDrop> ChanceItemTodos { get; set; } = new List<RPGItemDrop>();

        //Calculos feitos em outras áreas 
        public float DanoFeito { get; set; } = 0;

        public float PontosDeAcao { get; set; } //Determina se ataca ou não.
    }
}
