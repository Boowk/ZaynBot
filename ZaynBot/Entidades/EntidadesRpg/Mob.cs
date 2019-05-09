using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.Entidades.EntidadesRpg
{
    public class Mob
    {
        public class Hit
        {
            public int Hits { get; set; }
            public string Nome { get; set; }
        }

        public int IdCombate { get; set; }

        public string Nome { get; set; }
        public int Vida { get; set; }
        public List<Hit> Hits { get; set; } // Aleatoriamente vai sair um dano que ele tiver e com base no estilo para mostrar ao jogador a fraqueza
        //public int NivelAtaque { get; set; } //Precição
        //public int Armadura { get; set; } // Somar com o nivel defesa
        public int ChanceDeAparecer { get; set; }
        // public List<ItemChanceCair> ChanceCairItem { get; set; } = new List<ItemChanceCair>();

    }
}
