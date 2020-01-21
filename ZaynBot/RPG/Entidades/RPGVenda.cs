using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGVenda
    {
        // id 984984844
        // numero 1
        // mostra id + #slot
        // separa com var array = valores.Split('#');
        [BsonId]
        public ulong JogadorId { get; set; }
        public int Slot { get; set; }
        public int Preco { get; set; }
        public int Quantidade { get; set; }
        public string ItemNome { get; set; }
        public int QuantidadeParaColetar { get; set; }

        public RPGVenda(ulong id, int preco, int quantidade, string nome)
        {
            if (ModuloBanco.TryGetVenda(id, out var vendas))
                this.Slot = vendas.Count;
            else
                this.Slot = 0;
            this.JogadorId = id;
            this.Preco = preco;
            this.Quantidade = quantidade;
            this.ItemNome = nome;
            this.QuantidadeParaColetar = 0;

            ModuloBanco.ColecaoVenda.InsertOne(this);
        }

        public void Salvar()
        {
            if (QuantidadeParaColetar == 0)
            {
                ModuloBanco.ColecaoVenda.DeleteOne(x => x.JogadorId == this.JogadorId);
                return;
            }
            ModuloBanco.EditVenda(this);
        }
    }
}
