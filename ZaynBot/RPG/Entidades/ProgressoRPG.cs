using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class ProgressoRPG
    {
        public int NivelAtual { get; set; }
        public int NivelMax { get; set; }
        public double ExpAtual { get; set; }
        public double ExpMax { get; set; }
        public double ExpIncremento { get; set; }
        public double MultIncremento { get; set; }

        private ProgressoRPG()
        {
            ExpAtual = 0;
        }

        /// <summary>
        /// Deixe o nivelMax igual 0 para infinito
        /// </summary>
        /// <param name="nivel"></param>
        /// <param name="nivelMax"></param>
        /// <param name="expIncremento"></param>
        /// <param name="multIncremento"></param>
        public ProgressoRPG(int nivel, int nivelMax, double expIncremento, double multIncremento) : this()
        {
            this.NivelAtual = nivel;
            this.NivelMax = nivelMax;
            this.ExpMax = expIncremento;
            this.ExpIncremento = expIncremento;
            this.MultIncremento = multIncremento;
        }

        public ProgressoRPG(double expIncremento, double incremento) : this()
        {
            this.NivelAtual = 1;
            this.NivelMax = 0;
            this.ExpMax = expIncremento;
            this.ExpIncremento = expIncremento;
            this.MultIncremento = incremento;
        }

        /// <summary>
        /// Adiciona experiencia
        /// </summary>
        /// <param name="exp"></param>
        /// <returns>Retorna true caso tenha evoluido</returns>
        public bool AdicionarExp(double exp)
        {
           if (NivelAtual != NivelMax)
            {
                double expResultante = ExpAtual + exp;
                if (expResultante >= ExpMax)
                {
                    do
                    {
                        double quantosPrecisaProxNivel = expResultante - ExpMax;
                        Evoluir();
                        expResultante = quantosPrecisaProxNivel;
                    } while (expResultante >= ExpMax);
                    ExpAtual += expResultante;
                    return true;
                }
                ExpAtual += exp;
                return false;
            }
            else
            {
                ExpAtual = 0;
                return false;
            }
        }

        private void Evoluir()
        {
            if (NivelAtual != NivelMax)
            {
                NivelAtual++;
                ExpIncremento *= MultIncremento;
                ExpMax += ExpIncremento;
            }
            ExpAtual = 0;
        }
    }
}