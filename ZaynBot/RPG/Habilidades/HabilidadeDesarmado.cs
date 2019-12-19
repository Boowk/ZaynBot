using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Habilidades
{
    [BsonIgnoreExtraElements]
    public class ProficienciaDesarmado : RPGProficiencia
    {
        public ProficienciaDesarmado(string nome = "Desarmado", double expIncremento = 84)
            : base(nome, expIncremento) { }

        public bool AdicionarExp()
            => base.AdicionarExp(0.6);

        public double CalcularDano(double ataque)
            => 1 + (10.5 * (ataque / 100) * (2 + ((double)NivelAtual / 100)));
    }
}
