using MongoDB.Bson.Serialization.Attributes;

namespace ZaynBot.Core.Entidades
{
    [BsonIgnoreExtraElements]
    public class ServidorCore
    {
        [BsonId]
        public ulong Id { get; set; }
        public string Prefix { get; set; }
        public string BemVindoMensagem { get; set; }
        public ulong BemVindoCanalId { get; set; }

        public void Salvar()
            => ModuloBanco.EditServidor(this);

    }
}
