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

        public Saida(Direcoes direcao, int id)
        {
            Direcao = direcao;
            RegiaoId = id;
        }

        //var south = new Exit
        //{
        //    name = "South",
        //    area = "Anker Farm",
        //    region = "Anker",
        //    areaId = 1,
        //    keywords = new List<string>(),
        //    hidden = false,
        //    locked = false
        //};
    }
}
