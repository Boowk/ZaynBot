using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using ZaynBot.Utilidades;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGPersonagem
    {
        #region Atributos

        public RPGRaça Raca { get; set; }
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

        #endregion

        //[BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        //public float Fome { get; set; }
        //[BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        //public float Sede { get; set; }

        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public int LocalAtualId { get; set; }

        public RPGEquipamento Equipamento { get; set; }
        public RPGHabilidade Habilidade { get; set; }
        public RPGTitulo Titulo { get; set; }
        public RPGEmprego Emprego { get; set; }

        public bool Vivo { get; set; }
        public DateTime DataMorte { get; set; }

        public RPGBatalha CampoBatalha { get; set; }

        public RPGPersonagem(RPGRaça raca)
        {
            Raca = raca;
            Sortear sortear = new Sortear();
            int sorte = sortear.Valor(Raca.Sorte, Raca.Sorte);
            PontosDeVida = sortear.Valor(raca.Constituicao * 1 + sorte, raca.Constituicao * 10 + sorte);
            sorte = sortear.Valor(Raca.Sorte, Raca.Sorte);
            PontosDeMana = sortear.Valor(Raca.Inteligencia * 1 + sorte, raca.Inteligencia * 10 + sorte);
            PontosDeManaMaximo = PontosDeMana;
            PontosDeVidaMaxima = PontosDeVida;
            sorte = sortear.Valor(Raca.Sorte, Raca.Sorte);
            AtaqueFisico = sortear.Valor(Raca.Forca * 1 + sorte, Raca.Forca * 10 + sorte);
            sorte = sortear.Valor(Raca.Sorte, Raca.Sorte);
            DefesaFisica = sortear.Valor(Raca.Constituicao * 1 + sorte, Raca.Constituicao * 10 + sorte);
            sorte = sortear.Valor(Raca.Sorte, Raca.Sorte);
            AtaqueMagico = sortear.Valor(Raca.Inteligencia * 1 + sorte, Raca.Inteligencia * 10 + sorte);
            sorte = sortear.Valor(Raca.Sorte, Raca.Sorte);
            DefesaMagica = sortear.Valor(Raca.Inteligencia * 1 + sorte, Raca.Inteligencia * 10 + sorte);
            sorte = sortear.Valor(Raca.Sorte, Raca.Sorte);
            Velocidade = sortear.Valor(Raca.Destreza * 1 + sorte, Raca.Destreza * 10 + sorte);
            Equipamento = new RPGEquipamento();
            Habilidade = new RPGHabilidade();
            Titulo = new RPGTitulo();
            Emprego = new RPGEmprego("Desempregado");
            LocalAtualId = 0;
            Vivo = true;
            CampoBatalha = new RPGBatalha();
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
