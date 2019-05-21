﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.Funções;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGMob
    {
        public RPGRaça RaçaMob { get; set; }                              
        public string Nome { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float PontosDeVida { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float PontosDeVidaMaxima { get; set; }
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
        // public List<Hit> Hits { get; set; } // Aleatoriamente vai sair um dano que ele tiver e com base no estilo para mostrar ao jogador a fraqueza
        [BsonIgnore] public int ChanceDeAparecer { get; set; }
        // public List<ItemChanceCair> ChanceCairItem { get; set; } = new List<ItemChanceCair>();

        public RPGMob(string nome)
        {
            Nome = nome;
        }
    }

    public static class Extensão
    {
        public static RPGMob SetRaça(this RPGMob mob, RPGRaça raca)
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

    //public class Hit
    //{
    //    public int Hits { get; set; }
    //    public string Nome { get; set; }
    //}
}
