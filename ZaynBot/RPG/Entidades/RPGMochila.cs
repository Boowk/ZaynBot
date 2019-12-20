using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.Collections.Generic;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Entidades
{
    public class RPGMochila
    {
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public SortedList<string, ItemDataRPG> Itens { get; set; } = new SortedList<string, ItemDataRPG>();

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<TipoItemEnum, RPGItem> Equipamentos { get; set; } = new Dictionary<TipoItemEnum, RPGItem>();

        public double PesoAtual { get; set; }
        public double PesoMaximo { get; set; }

        public bool AdicionarItem(RPGItem item, int quantidade = 1)
        {
            // Verifica se o item tem durabilidade
            if (item.DurabilidadeMax > 0)
            {
                // Se tiver, só precisa adiciona-lo
                int incr = 1;
                for (int i = 0; i < quantidade; i++)
                {
                    bool naoAdicionou = true;
                    do
                    {
                        try
                        {
                            Itens.Add($"{item.Id} {incr}", new ItemDataRPG()
                            {
                                Id = item.Id,
                                DurabilidadeAtual = item.DurabilidadeMax,
                                Quantidade = 1,
                            });
                            naoAdicionou = false;
                        }
                        catch
                        {
                            incr++;
                            naoAdicionou = true;
                        }
                    } while (naoAdicionou);
                }
                return true;
            }

            // Se não tiver
            // Verificar se já está na mochila
            bool achou = Itens.TryGetValue(item.Id.ToString(), out ItemDataRPG itemEncontrado);

            // Se o encontrar, incrementa-se a quantidade
            if (achou)
            {
                itemEncontrado.Quantidade += quantidade;
                return true;
            }


            // Se não está na mochila, o adiciona
            Itens.Add(item.Id.ToString(), new ItemDataRPG()
            {
                Id = item.Id,
                Quantidade = quantidade,
                DurabilidadeAtual = 0,
            });
            return true;
        }

        public void DesequiparItem(RPGItem item, RPGPersonagem personagem)
        {
            personagem.Inventario.Equipamentos.Remove(item.TipoItem);

            // Adiciona-o na mochila
            AdicionarItem(item);
        }
    }
}
