using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Entidades
{
    public class ItemRPG
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public TipoItemEnum TipoItem { get; set; }
        public TipoExpEnum TipoExp { get; set; }
        public int Durabilidade { get; set; }
        public double PrecoBase { get; set; } = 0;

        public double AtaqueFisico { get; set; }
        public double DefesaFisica { get; set; }

        public ItemRPG(int id, string nome, TipoItemEnum tipo, TipoExpEnum tipoExp, int durabilidade, double preco)
        {
            Id = id;
            Nome = nome;
            TipoItem = tipo;
            TipoExp = tipoExp;
            Durabilidade = durabilidade;
            PrecoBase = preco;
        }

        public ItemRPG Clone()
              => (ItemRPG)MemberwiseClone();
    }

    public class ItemDataRPG
    {
        public int Id { get; set; }
        public int Durabilidade { get; set; }
        public int Quantidade { get; set; }
    }
}
