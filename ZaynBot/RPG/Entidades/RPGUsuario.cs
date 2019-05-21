using System;
using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.Funções;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGUsuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int64, AllowTruncation = true)]
        public ulong Id { get; set; }
        public int Nivel { get; set; } = 0;
        public double ExperienciaProximoNivel { get; set; } = 100;
        public double ExperienciaAtual { get; set; } = 0;
        public DateTime DataContaCriada { get; set; } = DateTime.UtcNow;
        public DateTime DataUltimaMensagemEnviada { get; set; } = DateTime.UtcNow;
        public RPGPersonagem Personagem { get; set; } = new RPGPersonagem();
        public List<Convite> ConvitesGuildas { get; set; } = new List<Convite>();

        public ObjectId IdGuilda { get; set; }

        public RPGUsuario(ulong id)
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

        public Tuple<bool, int> AdicionarExp()
        {
            Sortear sortear = new Sortear();
            int exp = sortear.Valor(5, 25);
            return new Tuple<bool, int>(AdicionarExp(exp), exp);
        }

        private void Evoluir()
        {
            Nivel += 1;
            ExperienciaAtual = 0;
            ExperienciaProximoNivel += 25;
        }

        public void RegeneraçãoVida()
        {
            float quantidade = Nivel / 100.0F;
            Personagem.PontosDeVida += quantidade;
            if (Personagem.PontosDeVida >= Personagem.PontosDeVidaMaxima) Personagem.PontosDeVida = Personagem.PontosDeVidaMaxima;
        }

        public void RegeneraçãoMana()
        {
            float quantidade = Nivel / 150.0F;
            Personagem.PontosDeMana += quantidade;
            if (Personagem.PontosDeMana >= Personagem.PontosDeManaMaximo) Personagem.PontosDeMana = Personagem.PontosDeManaMaximo;
        }
    }
}
