using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGInventario
    {
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public float PesoMaximo { get; set; }
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public float PesoAtual { get; set; }
        public Dictionary<string, RPGItem> Inventario { get; set; } = new Dictionary<string, RPGItem>();

        public RPGInventario(RPGRaça raca)
        {
            PesoMaximo = 8 + raca.Forca + (raca.Destreza / 2);
            PesoAtual = 0;
        }

        public bool Adicionar(RPGItem item)
        {
            /* Verifica se existi o item no inventario
             * se não existir, o adiciona.
             * se existir, o soma
             * 
             * Equipamentos deve funcionar quase que da mesma forma.
            */

            return true;
        }

        public bool Destruir(string nome)
        {
            return true;
        }

        public bool Usar(string nome, int quantidade)
        {
            return true;
        }
    }
}
