using DSharpPlus;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.Entidades
{
    public class Usuario
    {
        public ulong Id { get; set; }
        public string Nome { get; set; }
        public int Nivel { get; set; } = 0;
        public double ExperienciaProximoNivel { get; set; } = 100;
        public double ExperienciaAtual { get; set; } = 0;
        public DateTime DataMensagemEnviada { get; set; }
        public DateTime DataContaCriada { get; set; } = DateTime.Now;

        public void Copiar(Usuario usuario)
        {
            Id = usuario.Id;
            Nome = usuario.Nome;
            Nivel = usuario.Nivel;
            ExperienciaProximoNivel = usuario.ExperienciaProximoNivel;
            ExperienciaAtual = usuario.ExperienciaAtual;
            DataMensagemEnviada = usuario.DataMensagemEnviada;
            DataContaCriada = usuario.DataContaCriada;
        }

        public bool AdicionarExp(int exp)
        {
            //         70      =   50 + 20
            double expResultante = ExperienciaAtual + exp;
            // 70 >=  50     
            if (expResultante >= ExperienciaProximoNivel)
            {
                do
                {
                    //             20  =         70 - 20
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
