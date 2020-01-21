using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGMochila : SortedList<string, int>
    {
        public string AdicionarItem(RPGItem item, int quantidade = 1) => AdicionarItem(item.Nome, quantidade);

        public string AdicionarItem(string nome, int quantidade)
        {
            nome = nome.ToLower();
            if (this.TryGetValue(nome, out int quantidadeItem))
            {

                //quantidadeItem += quantidade;
                Remove(nome);
                Add(nome, quantidadeItem + quantidade);
                return nome;
            }
            Add(nome, quantidade);
            return nome;
        }

        public bool RemoverItem(string nome, int quantidade = 1)
        {
            nome = nome.ToLower();
            if (TryGetValue(nome, out int quantidadeItem))
            {
                if (quantidade > quantidadeItem)
                    return false;
                quantidadeItem -= quantidade;
                if (quantidadeItem == 0)
                    Remove(nome);
                else // Correcao de bug
                {
                    Remove(nome);
                    Add(nome, quantidadeItem);
                }
                return true;
            }
            return false;
        }
    }
}
