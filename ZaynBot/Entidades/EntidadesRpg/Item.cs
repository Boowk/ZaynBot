using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.Entidades.EntidadesRpg
{
    public class Item
    {
        public enum Tipo
        {
            Arma,
            Armadura,
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Tipo TipoItem { get; set; }
        public int Quantidade { get; set; }
        public float PreçoBase { get; set; }

        public Item(string nome, string descrição, Tipo tipo_item, float preco_base, int quantidade = 1)
        {
            Nome = nome;
            Descricao = descrição;
            TipoItem = tipo_item;
            PreçoBase = preco_base;
            Quantidade = quantidade;
        }
    }
}
