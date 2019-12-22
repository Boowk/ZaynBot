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
        public Dictionary<TipoItemEnum, RPGItem> Equipamentos { get; set; } = new Dictionary<TipoItemEnum, RPGItem>();

        public bool AdicionarItem(RPGItem item, int quantidade = 1)
        {
            // Verifica se o item tem durabilidade
            item.Nome = item.Nome.ToLower();
            if (item.DurabilidadeMax > 0)
            {
                // Se tiver, só precisa adiciona-lo
                RPGMochilaItemData itemData = new RPGMochilaItemData()
                {
                    Id = item.Id,
                    DurabilidadeAtual = item.DurabilidadeMax,
                    Quantidade = 1,
                };

                int incr = 1;
                for (int i = 0; i < quantidade; i++)
                {
                    do
                    {
                        if (Itens.TryAdd($"{item.Nome} {incr}", itemData))
                            break;
                        else
                            incr++;

                    } while (true);
                }
                return true;
            }

            // Se não tiver
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
                DurabilidadeAtual = 0,
            });
            return true;
        }

        public void DesequiparItem(RPGItem item, RPGPersonagem personagem)
        {
            personagem.Mochila.Equipamentos.Remove(item.TipoItem);
            AdicionarItem(item);
        }
    }

    [BsonIgnoreExtraElements]
    public class RPGMochilaItemData
    {
        public int Id { get; set; }
        public int DurabilidadeAtual { get; set; }
        public int Quantidade { get; set; }
    }
}
