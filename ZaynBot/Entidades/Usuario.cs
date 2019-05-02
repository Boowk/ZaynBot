using System;
using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.Entidades.Rpg;

namespace ZaynBot.Entidades
{
    [BsonIgnoreExtraElements]
    public class Usuario
    {
        public ulong Id { get; set; }
        public int Nivel { get; set; }
        public double ExperienciaProximoNivel { get; set; }
        public double ExperienciaAtual { get; set; }
        public DateTime DataContaCriada { get; set; }
        public DateTime DataMensagemEnviada { get; set; }
        public Personagem Personagem { get; set; }

        public Usuario(ulong id)
        {
            Id = id;
            Personagem = new Personagem();
            DataMensagemEnviada = DateTime.UtcNow;
            DataContaCriada = DateTime.UtcNow;
            ExperienciaAtual = 0;
            ExperienciaProximoNivel = 100;
            Nivel = 0;
        }

        public Usuario() { }

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
