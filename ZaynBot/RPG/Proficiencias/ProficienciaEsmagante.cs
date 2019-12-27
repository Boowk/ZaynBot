using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Proficiencias
{
    [BsonIgnoreExtraElements]
    class ProficienciaEsmagante : RPGProficiencia
    {
        public ProficienciaEsmagante(string nome = "Esmagante") : base(nome)
        {
        }
    }
}
