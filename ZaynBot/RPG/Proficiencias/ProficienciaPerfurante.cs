using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Proficiencias
{
    [BsonIgnoreExtraElements]
    public class ProficienciaPerfurante : RPGProficiencia
    {
        public ProficienciaPerfurante(string nome = "Perfurante") : base(nome)
        {
        }
    }
}
