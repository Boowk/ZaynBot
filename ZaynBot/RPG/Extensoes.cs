using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.RPG
{
    public static class Extensoes
    {
        public static string Titulo(this string titulo)
        {
            return "⌈" + titulo + "⌋";
        }
    }
}
