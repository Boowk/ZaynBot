
namespace ZaynBot.RPG.Entidades
{
    public class RPGHabilidade
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public float CustoMana { get; set; } //Diminui 1% a cada nivel = * 0,99 <-padrão
        public bool Passiva { get; set; }
        public int Nivel { get; set; } = 0;
        public double ExperienciaProximoNivel { get; set; } = 100; //* 1.10409 a cada nivel
        public double ExperienciaAtual { get; set; } = 0;


        public bool Cura { get; set; }
        public float CuraQuantidadePorcentagem { get; set; } // 0,04 
        public float CuraSobeNivelPorcentagem { get; set; } // em porcentagem ex: subir 1% = 1,01

        public bool Dano { get; set; }
        public bool DanoFisica { get; set; }
        public bool DanoMagico { get; set; }
        public float DanoPorcentagem { get; set; } // tipo dano  + porcentagemDano
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
