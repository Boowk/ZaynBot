using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGHabilidade
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float CustoMana { get; set; } //Diminui 1% a cada nivel = * 0,99 <-padrão
        public bool Passiva { get; set; }
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public int Nivel { get; set; } = 0;
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public double ExperienciaProximoNivel { get; set; } = 100; //* 1.10409 a cada nivel
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public double ExperienciaAtual { get; set; } = 0;


        public bool Cura { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float CuraQuantidadePorcentagem { get; set; } // 0,04 
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float CuraSobeNivelPorcentagem { get; set; } // em porcentagem ex: subir 1% = 1,01

        public bool Dano { get; set; }
        public bool DanoFisica { get; set; }
        public bool DanoMagico { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float DanoPorcentagem { get; set; } // tipo dano  + porcentagemDano
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float DanoSobeNivelPorcentagem { get; set; } // em porcentagem ex: subir 1% = 1,01

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
            ExperienciaProximoNivel = ExperienciaProximoNivel * 1.10409;
            if (!Passiva)
                CustoMana = CustoMana * 0.99F;
            if (Cura)
            {
                CuraQuantidadePorcentagem = CuraQuantidadePorcentagem * CuraSobeNivelPorcentagem;
            }
            if (Dano)
            {

            }
        }
    }
}
