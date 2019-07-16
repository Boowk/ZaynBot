﻿using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    public class RPGInventario
    {
        public float PesoMaximo { get; set; }
        public float PesoAtual { get; set; } = 0;
        public Dictionary<string, RPGItem> Inventario { get; set; } = new Dictionary<string, RPGItem>();

        public RPGInventario(int forca, int destreza)
            => PesoMaximo = 8 + forca + (destreza / 2);

        public bool Adicionar(RPGItem item, int quantidade)
        {
            /* Verifica se existi o item no inventario
             * se não existir, o adiciona.
             * se existir, o soma
             * 
             * Equipamentos deve funcionar quase que da mesma forma.
            */

            Inventario.TryGetValue(item.Nome, out RPGItem existeNoInventario);
            if (existeNoInventario != null)
            {
                existeNoInventario.Quantidade += quantidade;
                PesoAtual += item.Peso * quantidade;
            }
            else
            {
                RPGItem f = item.Clone();
                f.Quantidade = quantidade;
                PesoAtual += f.Peso * quantidade;
                Inventario.Add(f);
            }

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
