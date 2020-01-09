using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGMochila
    {
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public SortedList<string, RPGMochilaItemData> Itens { get; set; } = new SortedList<string, RPGMochilaItemData>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<EnumTipo, RPGItem> Equipamentos { get; set; } = new Dictionary<EnumTipo, RPGItem>();

        public void AdicionarItem(RPGItem item, int quantidade = 1)
        {
            AdicionarItem(item.Nome, new RPGMochilaItemData()
            {
                Id = item.Id,
                Quantidade = quantidade
            });
        }
        public void AdicionarItem(string nome, RPGMochilaItemData item)
        {
            nome = nome.ToLower();
            // Verificar se já está na mochila
            // Se o encontrar, incrementa-se a quantidade
            if (Itens.TryGetValue(nome, out RPGMochilaItemData itemEncontrado))
            {
                itemEncontrado.Quantidade += item.Quantidade;
                return;
            }
            // Se não está na mochila, o adiciona
            Itens.Add(nome, item);
        }

        public void RemoverItem(string itemNome, int quantidade = 1)
        {
            RPGMochilaItemData item = Itens[itemNome];
            item.Quantidade -= quantidade;
            if (item.Quantidade == 0)
                Itens.Remove(itemNome);
        }
    }

    [BsonIgnoreExtraElements]
    public class RPGMochilaItemData
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
    }
}
