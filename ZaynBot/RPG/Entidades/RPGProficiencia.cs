using MongoDB.Bson.Serialization.Attributes;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    [BsonDiscriminator("Proficiencia")]
    [BsonKnownTypes(typeof(ProficienciaAtaque), typeof(ProficienciaForca), typeof(ProficienciaDefesa))]
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
}