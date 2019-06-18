using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.Core
{
   public static class Extensoes
    {
        public static bool IsNullOrEmpty(this Array array)
        {
            return (array == null || array.Length == 0);
        }
    }
}
