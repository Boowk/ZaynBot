using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ZaynBot.RPG.Entidades
{
    public enum EnumProficiencia
    {
        Nenhum,
        Ataque,
        Forca,
        Defesa,
    }

    [BsonIgnoreExtraElements]
    [BsonDiscriminator("Proficiencia")]
    [BsonKnownTypes(typeof(ProficienciaAtaque), typeof(ProficienciaForca), typeof(ProficienciaDefesa), typeof(ProficienciaDesarmado),
        typeof(ProficienciaEsmagante), typeof(ProficienciaPerfurante))]
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

        public double CalcChance(double sorte, double armaduraInimigo)
        {
            //Hit Chance = (F(Ataque Nivel) + Sorte / Pontos de Armadura)
            return (FormulaAtaque() + sorte) / armaduraInimigo;
        }

        private double FormulaAtaque()
        {
            //F(1) = 0.0008.a³ +4.a + 40
            return (0.0008 * Math.Pow(Pontos, 3)) + (4 * (double)Pontos) + 40;
        }
    }

    public class ProficienciaForca : RPGProficiencia
    {
        public ProficienciaForca(string nome = "Força") : base(nome) { }

        public double CalcDanoExtra(double AtaqueBasico)
        {
            //Força = 0.1 % do ataque basico por nivel
            double porcentagem = Pontos * 0.001;
            return AtaqueBasico * porcentagem;
        }
    }

    public class ProficienciaDefesa : RPGProficiencia
    {
        public ProficienciaDefesa(string nome = "Defesa") : base(nome) { }

        public double CalcDefesa()
        {
            // Cada nível de defesa reduz o dano em 0.1%. (usar defesa do LoL)
            double porcentagem = Pontos * 0.1;
            return 100 / (100 + porcentagem);
        }
    }

    public class ProficienciaPerfurante : RPGProficiencia
    {
        public ProficienciaPerfurante(string nome = "Remover") : base(nome) { }
    }
    public class ProficienciaEsmagante : RPGProficiencia
    {
        public ProficienciaEsmagante(string nome = "Remover") : base(nome) { }
    }
    public class ProficienciaDesarmado : RPGProficiencia
    {
        public ProficienciaDesarmado(string nome = "Remover") : base(nome) { }
    }
}