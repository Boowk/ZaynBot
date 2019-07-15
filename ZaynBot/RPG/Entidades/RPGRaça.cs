using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGRaça
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public int Forca { get; set; }
        public int Inteligencia { get; set; }
        public int Percepcao { get; set; }
        public int Destreza { get; set; }
        public int Constituicao { get; set; }
        public int Sorte { get; set; }
        public int Nivel { get; set; } = 1;
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public double ExperienciaProximoNivel { get; set; } = 1000;
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public double ExperienciaAtual { get; set; } = 0;
        public int PontosDisponiveis { get; set; } = 0;

        public RPGRaça(string nome)
            => Nome = nome.ToLower();

        public void AdicionarExp(float exp)
        {
            double expResultante = ExperienciaAtual + exp;
            if (expResultante >= ExperienciaProximoNivel)
            {
                do
                {
                    double quantosPrecisaProxNivel = expResultante - ExperienciaProximoNivel;
                    Evoluir();
                    expResultante = quantosPrecisaProxNivel;
                } while (expResultante >= ExperienciaProximoNivel);
                ExperienciaAtual += expResultante;
                return;
            }
            ExperienciaAtual += exp;
        }

        private void Evoluir()
        {
            Nivel += 1;
            ExperienciaAtual = 0;
            ExperienciaProximoNivel *= 1.10409;
            PontosDisponiveis++;
        }

        public RPGRaça Clone()
            => (RPGRaça)MemberwiseClone();
    }
}
