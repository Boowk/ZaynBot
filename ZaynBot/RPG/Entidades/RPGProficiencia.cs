using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Proficiencias;

namespace ZaynBot.RPG.Entidades
{
    public enum EnumProficiencia
    {
        Nenhum,
        Perfurante,
        Esmagante,
        ArmaduraLeve,
        ArmaduraPesada,
    }

    [BsonIgnoreExtraElements]
    [BsonDiscriminator("Proficiencia")]
    [BsonKnownTypes(typeof(ProficienciaPerfurante), typeof(ProficienciaDesarmado), typeof(ProficienciaEsmagante))]
    public class RPGProficiencia
    {
        public string Nome { get; set; }
        public int Pontos { get; set; } = 0;
        public RPGProficiencia(string nome)
        {
            Nome = nome;
        }
    }
}