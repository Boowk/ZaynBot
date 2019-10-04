using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Habilidades
{
    [BsonIgnoreExtraElements]
    public class DesarmadoHabilidade : HabilidadeRPG
    {
        public DesarmadoHabilidade(string nome = "Desarmado", int nivel = 1, int nivelMax = 100, double expIncremento = 84, double incremento = 1.104)
            : base(nome, nivel, nivelMax, expIncremento, incremento) { }

        public bool AdicionarExp()
            => base.AdicionarExp(0.6);

        public double CalcularDano(int forcaNivel)
            => 1 + (10.5 * (((double)forcaNivel) / 100) * (((double)NivelAtual) / 100));
    }
}
