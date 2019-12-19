using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Habilidades
{
    [BsonIgnoreExtraElements]
    public class ProficienciaPerfurante : RPGProficiencia
    {
        public ProficienciaPerfurante(string nome = "Perfurante", double expIncremento = 84)
            : base(nome, expIncremento) { }

        public bool AdicionarExp()
            => base.AdicionarExp(0.5);

        public double CalcularDano(double armaDano, int armaDurabilidadeAtual, int armaDurabilidadeMax)
            => (armaDano * 0.5) * (0.8 + NivelAtual * 0.015) * ((double)armaDurabilidadeAtual / ((double)armaDurabilidadeMax + 1) / 2);
    }
}
