using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using ZaynBot._Gameplay.Raças;
using ZaynBot.Funções;

namespace ZaynBot.Entidades.EntidadesRpg
{
    [BsonIgnoreExtraElements]
    public class Personagem
    {
        #region Atributos

        public Raça RaçaPersonagem { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float PontosDeVida { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float PontosDeVidaMaxima { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float PontosDeMana { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float PontosDeManaMaximo { get; set; }
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
        public int Sorte { get; set; }

        #endregion

        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float Fome { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float Sede { get; set; }

        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public int LocalAtualId { get; set; }

        public Equipamento Equipamento { get; set; }
        public Habilidade Habilidade { get; set; }
        public Titulo Titulo { get; set; }
        public Emprego Emprego { get; set; }

        public bool Vivo { get; set; }
        public DateTime DataMorte { get; set; }

        public Batalha CampoBatalha { get; set; }

        public Personagem()
        {
            RaçaPersonagem = Humano.HumanoAb();
            Sortear sortear = new Sortear();
            PontosDeVida = sortear.Valor(RaçaPersonagem.PontosDeVidaBaseMin, RaçaPersonagem.PontosDeVidaBaseMax);
            PontosDeVidaMaxima = PontosDeVida;
            PontosDeMana = sortear.Valor(RaçaPersonagem.PontosDeManaBaseMin, RaçaPersonagem.PontosDeManaBaseMax);
            PontosDeManaMaximo = PontosDeMana;
            AtaqueFisico = sortear.Valor(RaçaPersonagem.AtaqueFisicoBaseMin, RaçaPersonagem.AtaqueFisicoBaseMax);
            DefesaFisica = sortear.Valor(RaçaPersonagem.DefesaFisicaBaseMin, RaçaPersonagem.DefesaFisicaBaseMax);
            AtaqueMagico = sortear.Valor(RaçaPersonagem.AtaqueMagicoBaseMin, RaçaPersonagem.AtaqueMagicoBaseMax);
            DefesaMagica = sortear.Valor(RaçaPersonagem.DefesaMagicaBaseMin, RaçaPersonagem.DefesaMagicaBaseMax);
            Velocidade = sortear.Valor(RaçaPersonagem.VelocidadeBaseMin, RaçaPersonagem.VelocidadeBaseMax);
            Sorte = sortear.Valor(RaçaPersonagem.VelocidadeBaseMin, RaçaPersonagem.VelocidadeBaseMin);
            Equipamento = new Equipamento();
            Habilidade = new Habilidade();
            Titulo = new Titulo();
            Emprego = new Emprego("Desempregado");
            LocalAtualId = 0;
            Vivo = true;
            CampoBatalha = new Batalha();
        }

        //public int Alimentar(int quantidade)
        //{
        //    Fome += quantidade;
        //    if (Fome > 100) Fome = 100;
        //    return (int)Fome;
        //}

        //public int Beber(int quantidade)
        //{
        //    Sede += quantidade;
        //    if (Sede > 100) Sede = 100;
        //    return (int)Sede;
        //}
    }

    public static class ExtensãoPersonagem
    {
        public static string Texto(this float numero)
        {
            return string.Format("{0:N2}", numero);
        }
    }
}
