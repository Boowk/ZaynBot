using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Proficiencias
{
    [BsonIgnoreExtraElements]
    class ProficienciaDesarmado : RPGProficiencia
    {
        public ProficienciaDesarmado(string nome = "Desarmado") : base(nome)
        {
        }
    }
}
