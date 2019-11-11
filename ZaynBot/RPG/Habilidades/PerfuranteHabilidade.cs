using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Habilidades
{
    [BsonIgnoreExtraElements]
    public class PerfuranteHabilidade : HabilidadeRPG
    {
        public PerfuranteHabilidade(string nome = "Perfurante", int nivel = 1, int nivelMax = 100, double expIncremento = 84, double incremento = 1.102)
            : base(nome, nivel, nivelMax, expIncremento, incremento) { }

        public bool AdicionarExp()
            => base.AdicionarExp(0.5);

        public double CalcularDano(double armaDano, int armaDurabilidadeAtual, int armaDurabilidadeMax, int forcaNivel)
            => (armaDano * 0.5) * (0.75 + forcaNivel * 0.005) * (0.2 + NivelAtual * 0.015) * ((double)armaDurabilidadeAtual / ((double)armaDurabilidadeMax));
    }
}
