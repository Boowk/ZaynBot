using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.Entidades.EntidadesRpg
{
    [BsonIgnoreExtraElements]
    public class Mob
    {
        public int IdCombate { get; set; }

        public string Raça { get; set; }
        public float PontosDeVida { get; set; }
        public float PontosDeVidaMaxima { get; set; }
        public float AtaqueFisico { get; set; }
        public float DefesaFisica { get; set; }
        public float AtaqueMagico { get; set; }
        public float DefesaMagica { get; set; }
        public int Velocidade { get; set; }
        public List<Hit> Hits { get; set; } // Aleatoriamente vai sair um dano que ele tiver e com base no estilo para mostrar ao jogador a fraqueza
        public int ChanceDeAparecer { get; set; }
        // public List<ItemChanceCair> ChanceCairItem { get; set; } = new List<ItemChanceCair>();

    }

    public class Hit
    {
        public int Hits { get; set; }
        public string Nome { get; set; }
    }

    public static class ExtensaoMob
    {
        public static Mob Raca(this Mob mob, string raca)
        {
            mob.Raça = raca;
            return mob;
        }
    }
}
