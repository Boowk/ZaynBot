using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.Entidades.EntidadesRpg.EntidadesRpgMapa
{
    public class Saida
    {
        public enum Direcoes
        {
            Norte,
            Sul,
            Oeste,
            Leste,
        }

        public Direcoes Direcao { get; set; }
        public int RegiaoId { get; set; }
    }
}
