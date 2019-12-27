using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.Collections.Generic;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGMochila
    {
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public SortedList<string, RPGMochilaItemData> Itens { get; set; } = new SortedList<string, RPGMochilaItemData>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<EnumItem, RPGItem> Equipamentos { get; set; } = new Dictionary<EnumItem, RPGItem>();

        public bool AdicionarItem(RPGItem item, int quantidade = 1)
        {
            item.Nome = item.Nome.ToLower();
            // Verificar se já está na mochila
            // Se o encontrar, incrementa-se a quantidade
            if (Itens.TryGetValue(item.Nome, out RPGMochilaItemData itemEncontrado))
            {
                itemEncontrado.Quantidade += quantidade;
                return true;
            }

            // Se não está na mochila, o adiciona
            Itens.Add(item.Nome, new RPGMochilaItemData()
            {
                Id = item.Id,
                Quantidade = quantidade,
            });
            return true;
        }

        public void RemoverItem(string itemNome, int quantidade = 1)
        {
            RPGMochilaItemData item = Itens[itemNome];
            item.Quantidade -= quantidade;
            if (item.Quantidade == 0)
                Itens.Remove(itemNome);
        }

        public void DesequiparItem(RPGItem item, RPGPersonagem personagem)
        {
            personagem.Mochila.Equipamentos.Remove(item.Tipo);
            AdicionarItem(item);
        }
    }

    [BsonIgnoreExtraElements]
    public class RPGMochilaItemData
    {
        public int Id { get; set; }
        public int Quantidade { get; set; }
    }
}
