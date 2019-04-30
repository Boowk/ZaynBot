using System;

namespace ZaynBot.Entidades
{
    public class Usuario
    {
        public ulong Id { get; set; }
        public string Nome { get; set; }
        public int Nivel { get; set; } = 0;
        public double ExperienciaProximoNivel { get; set; } = 100;
        public double ExperienciaAtual { get; set; } = 0;
        public int Respeitos { get; set; } = 0;
        public int RespeitosDisponiveis { get; set; } = 0;
        public DateTime DataContaCriada { get; set; } = DateTime.Now;
        public DateTime DataRespeitosReset { get; set; }
        public DateTime DataMensagemEnviada { get; set; }

        public Usuario()
        {
            //DataRespeitosReset = DataContaCriada.AddDays(14);
        }


        public void Copiar(Usuario usuario)
        {                  
            Id = usuario.Id;
            Nome = usuario.Nome;
            Nivel = usuario.Nivel;
            ExperienciaProximoNivel = usuario.ExperienciaProximoNivel;
            ExperienciaAtual = usuario.ExperienciaAtual;
            DataMensagemEnviada = usuario.DataMensagemEnviada;
            DataContaCriada = usuario.DataContaCriada;
            DataRespeitosReset = usuario.DataRespeitosReset;
            Respeitos = usuario.Respeitos;
            RespeitosDisponiveis = usuario.RespeitosDisponiveis;   
        }

        public bool AdicionarExp(int exp)
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
                return true;
            }
            ExperienciaAtual += exp;
            return false;
        }

        public void Evoluir()
        {
            Nivel += 1;
            ExperienciaAtual = 0;
            ExperienciaProximoNivel += 25;
        }
    }
}
