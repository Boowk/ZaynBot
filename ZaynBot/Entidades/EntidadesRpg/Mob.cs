using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;
using ZaynBot.Funções;

namespace ZaynBot.Entidades.EntidadesRpg
{
    [BsonIgnoreExtraElements]
    public class Mob
    {
        public int IdCombate { get; set; }

        public Raça RaçaMob { get; set; }
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
        public static Mob Raca(this Mob mob, Raça raca)
        {
            Sortear sortear = new Sortear();
            mob.RaçaMob = raca;
            mob.PontosDeVida += sortear.Valor(raca.PontosDeVidaBaseMin, raca.PontosDeVidaBaseMax);
            mob.PontosDeVidaMaxima = mob.PontosDeVida;
            mob.AtaqueFisico += sortear.Valor(raca.AtaqueFisicoBaseMin, raca.AtaqueFisicoBaseMax);
            mob.DefesaFisica += sortear.Valor(raca.DefesaFisicaBaseMin, raca.DefesaFisicaBaseMax);
            mob.AtaqueMagico += sortear.Valor(raca.AtaqueMagicoBaseMin, raca.AtaqueMagicoBaseMax);
            mob.DefesaMagica += sortear.Valor(raca.DefesaMagicaBaseMin, raca.DefesaMagicaBaseMax);
            mob.Velocidade += sortear.Valor(raca.VelocidadeBaseMin, raca.VelocidadeBaseMax);
            return mob;
        }
    }
}
