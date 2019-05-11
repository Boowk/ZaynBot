using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.Entidades.EntidadesRpg;

namespace ZaynBot.Entidades
{
    [BsonIgnoreExtraElements]
    public class Usuario
    {
        public ulong Id { get; set; }
        public int Nivel { get; set; } = 0;
        public double ExperienciaProximoNivel { get; set; } = 100;
        public double ExperienciaAtual { get; set; } = 0;
        public DateTime DataContaCriada { get; set; } = DateTime.UtcNow;
        public DateTime DataUltimaMensagemEnviada { get; set; } = DateTime.UtcNow;
        public Personagem Personagem { get; set; } = new Personagem();
        public List<Convite> ConvitesGuildas { get; set; } = new List<Convite>();

        public ObjectId IdGuilda { get; set; }

        public Usuario(ulong id)
        {
            Id = id;
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
