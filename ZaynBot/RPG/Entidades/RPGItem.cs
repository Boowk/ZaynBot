using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGItem
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public EnumTipoItem TipoItem { get; set; }
        public int Quantidade { get; set; }
        public float PreçoBase { get; set; }

        public RPGItem(string nome, string descrição, EnumTipoItem tipoTtem, float precoBase, int quantidade = 1)
        {
            Nome = nome;
            Descricao = descrição;
            TipoItem = tipoTtem;
            PreçoBase = precoBase;
            Quantidade = quantidade;
        }
    }
}
