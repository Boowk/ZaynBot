using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.RPG.Entidades
{
    public class RPGInventario
    {
        public float PesoMaximo { get; set; }
        public float PesoAtual { get; set; }
        public Dictionary<string, RPGItem> Inventario { get; set; } = new Dictionary<string, RPGItem>();

        public RPGInventario(RPGRaça raca)
        {
            //PesoMax = raca.atributos * ...
        }

        public bool Adicionar(RPGItem item)
        {
            /* Verifica se existi o item no inventario
             * se não existir, o adiciona.
             * se existir, o soma
            */
            return true;
        }

        public bool Destruir(string nome)
        {
            return true;
        }

        public bool Usar(string nome, int quantidade)
        {
            return true;
        }
    }
}
