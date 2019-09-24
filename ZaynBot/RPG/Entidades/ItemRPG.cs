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
        public int Durabilidade { get; set; }
        public float PrecoBase { get; set; } = 0;

        public float AtaqueFisico { get; set; }
        public float DefesaFisica { get; set; }

        public ItemRPG(TipoItemEnum tipo, int durabilidade)
        {
            TipoItem = tipo;
            Durabilidade = durabilidade;
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
