using System;
using System.Collections.Generic;
using System.Text;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.DataTeste.Receitas
{
    public static class ReceitasReceita
    {
        public static ReceitaRPG CarneDeCoelho()
            => new ReceitaRPG(0, "Carne de Coelho Assada", 4)
            {
                Ingredientes = new Dictionary<int, int>
                {
                    {5,1 } //Id item, item quantidade
                }
            };
        public static ReceitaRPG OssoAfiado()
            => new ReceitaRPG(1, "Osso afiado", 6)
            {
                Ingredientes = new Dictionary<int, int>
                {
                    {2,2 }
                }
            };
    }
}
