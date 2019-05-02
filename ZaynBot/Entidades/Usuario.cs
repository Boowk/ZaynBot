using MongoDB.Bson.Serialization.Attributes;
using System;
using ZaynBot.Entidades.Rpg;

namespace ZaynBot.Entidades
{
    [BsonIgnoreExtraElements]
    public class Usuario
    {
        public ulong Id { get; set; }
        public int Nivel { get; set; } = 0;
        public double ExperienciaProximoNivel { get; set; } = 100;
        public double ExperienciaAtual { get; set; } = 0;
        public DateTime DataContaCriada { get; set; } = DateTime.Now;
        public DateTime DataMensagemEnviada { get; set; }
        public Personagem Personagem { get; set; }

        public Usuario()
        {
            Personagem = new Personagem();
            //DataRespeitosReset = DataContaCriada.AddDays(14);
        }

        public void Copiar(Usuario usuario)
        {
            Id = usuario.Id;
            Nivel = usuario.Nivel;
            ExperienciaProximoNivel = usuario.ExperienciaProximoNivel;
            ExperienciaAtual = usuario.ExperienciaAtual;
            DataMensagemEnviada = usuario.DataMensagemEnviada;
            DataContaCriada = usuario.DataContaCriada;
            Personagem = usuario.Personagem;
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

        private void Evoluir()
        {
            Nivel += 1;
            ExperienciaAtual = 0;
            ExperienciaProximoNivel += 25;
        }
    }
}
