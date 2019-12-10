using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGItem
    {
        [BsonId]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoItemEnum TipoItem { get; set; }
        public TipoExpEnum TipoExp { get; set; }
        public int DurabilidadeMax { get; set; }
        public double PrecoBase { get; set; }

        public double AtaqueFisico { get; set; }
        public double DefesaFisica { get; set; }

        public double VidaRestaura { get; set; }
        public double MagiaRestaura { get; set; }
        public double FomeRestaura { get; set; }

        public RPGItem(int id, string nome, TipoItemEnum tipo, double preco = 1)
        {
            Id = id;
            Nome = nome;
            TipoItem = tipo;
            PrecoBase = preco;
            DurabilidadeMax = 0;
            TipoExp = TipoExpEnum.Nenhum;
        }

        public RPGItem(int id, string nome, TipoItemEnum tipo, int durabilidade, double preco, TipoExpEnum tipoExp) : this(id, nome, tipo, preco)
        {
            TipoExp = tipoExp;
            DurabilidadeMax = durabilidade;
        }
    }

    [BsonIgnoreExtraElements]
    public class ItemDataRPG
    {
        public int Id { get; set; }
        public int DurabilidadeAtual { get; set; }
        public int Quantidade { get; set; }
    }
}
