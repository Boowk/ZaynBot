using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.RPG.Entidades
{
    public class RPGItemDrop
    {
        public RPGItem Item { get; set; }
        public int QuantidadeMaxima { get; set; } = 1;
        public double ChanceDeCair { get; set; } = 1;
    }
}
