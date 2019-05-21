using MongoDB.Bson.Serialization.Attributes;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGItem
    {
        public enum Tipo
        {
            Arma,
            Armadura,
        }

        public string Nome { get; set; }
        public string Descricao { get; set; }
        public Tipo TipoItem { get; set; }
        public int Quantidade { get; set; }
        public float PreçoBase { get; set; }

        public RPGItem(string nome, string descrição, Tipo tipo_item, float preco_base, int quantidade = 1)
        {
            Nome = nome;
            Descricao = descrição;
            TipoItem = tipo_item;
            PreçoBase = preco_base;
            Quantidade = quantidade;
        }
    }
}
