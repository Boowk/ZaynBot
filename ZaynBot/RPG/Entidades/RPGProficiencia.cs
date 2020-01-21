using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    [BsonDiscriminator("Proficiencia")]
    [BsonKnownTypes(typeof(ProficienciaAtaque), typeof(ProficienciaForca), typeof(ProficienciaDefesa), typeof(ProficienciaMinerar), typeof(ProficienciaCortar))]
    public class RPGProficiencia
    {
        public string Nome { get; set; }
        public int Pontos { get; set; } = 1;
        public RPGProficiencia(string nome)
        {
            Nome = nome;
        }
    }

    public class ProficienciaAtaque : RPGProficiencia
    {
        public ProficienciaAtaque(string nome = "Ataque") : base(nome) { }
    }
    public class ProficienciaForca : RPGProficiencia
    {
        public ProficienciaForca(string nome = "Força") : base(nome) { }

        public double CalcDanoExtra(double AtaqueBasico)
        {
            double porcentagem = Pontos * 0.001;
            return AtaqueBasico * porcentagem;
        }
    }
    public class ProficienciaDefesa : RPGProficiencia
    {
        public ProficienciaDefesa(string nome = "Defesa") : base(nome) { }
    }
    public class ProficienciaMinerar : RPGProficiencia
    {
        public ProficienciaMinerar(string nome = "Minerar") : base(nome) { }
        public int CalcMinerioExtra()
        {
            double minerioExtra = (Pontos * 0.25) + 1;
            return Convert.ToInt32(minerioExtra);
        }
    }
    public class ProficienciaCortar : RPGProficiencia
    {
        public ProficienciaCortar(string nome = "Cortar") : base(nome) { }
        public int CalcMadeiraExtra()
        {
            double madeira = (Pontos * 0.25) + 1;
            return Convert.ToInt32(madeira);
        }
    }
}