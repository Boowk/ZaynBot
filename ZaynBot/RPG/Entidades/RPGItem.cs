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
        public int PrecoCompra { get; set; }
        public double PrecoVenda { get; set; }

        public double AtaqueFisico { get; set; }
        public double DefesaFisica { get; set; }

        public double VidaRestaura { get; set; }
        public double MagiaRestaura { get; set; }
        public double FomeRestaura { get; set; }

        public RPGItem(int id, string nome, TipoItemEnum tipo, int preco = 1)
        {
            Id = id;
            Nome = nome;
            TipoItem = tipo;
            PrecoCompra = preco;
            DurabilidadeMax = 0;
            TipoExp = TipoExpEnum.Nenhum;
        }

        public RPGItem(int id, string nome, TipoItemEnum tipo, int durabilidade, int preco, TipoExpEnum tipoExp) : this(id, nome, tipo, preco)
        {
            TipoExp = tipoExp;
            DurabilidadeMax = durabilidade;
        }
    }
}
