using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.Collections.Generic;
using ZaynBot.Enuns;

namespace ZaynBot.Entidades
{
    [BsonIgnoreExtraElements]
    public class EntidadeUsuario
    {
        [BsonId]
        public ulong Id { get; private set; }
        public ulong Rev { get; set; }
        public decimal Real { get; private set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfDocuments)]
        public Dictionary<EnumConquistas, EntidadeConquista> Conquistas { get; private set; }

        public EntidadeUsuario(ulong id)
        {
            Id = id;
            Real = 0;
            Rev = 0;

            Conquistas = new Dictionary<EnumConquistas, EntidadeConquista>()
            {
                {EnumConquistas.MensagensCriadas, new EntidadeConquista("Mensagens criadas") },
                {EnumConquistas.MensagensEditadas, new EntidadeConquista("Mensagens editadas") },
                {EnumConquistas.MensagensDeletadas, new EntidadeConquista("Mensagens deletadas") },
                {EnumConquistas.CanalDeVoz, new EntidadeConquista("Canais de voz entrados") },
            };
        }

        public void AdicionarReal(decimal valor) => Real += valor;

        public bool RemoverReal(decimal valor)
        {
            if (Real - valor < 0)
                return false;
            Real -= valor;
            return true;
        }

        public void Salvar() => ModuloBanco.EditUsuario(this);
    }
}
