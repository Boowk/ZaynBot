using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Habilidades
{
    [BsonIgnoreExtraElements]
    public class DesarmadoHabilidade : HabilidadeRPG
    {
        public DesarmadoHabilidade(string nome = "Desarmado", int nivel = 1, int nivelMax = 100, double expIncremento = 84, double incremento = 1.102)
            : base(nome, nivel, nivelMax, expIncremento, incremento) { }

        public bool AdicionarExp()
            => base.AdicionarExp(0.6);

        public double CalcularDano(double ataque)
            => 1 + (10.5 * (ataque / 100) * (2 + ((double)NivelAtual / 100)));
    }
}
