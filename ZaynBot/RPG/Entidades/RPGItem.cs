using MongoDB.Bson.Serialization.Attributes;

namespace ZaynBot.RPG.Entidades
{
    public enum EnumTipo
    {
        ArmaPrimaria = 1,
        ArmaSegundaria = 2,
        Helmo = 3,
        Peitoral = 4,
        Luvas = 5,
        Botas = 6,
        Moeda = 7,
        Comida = 8,
        Pocao = 9,
        Recurso = 10,
    }

    [BsonIgnoreExtraElements]
    public class RPGItem
    {
        [BsonId]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public EnumTipo Tipo { get; set; }
        public EnumProficiencia Proficiencia { get; set; }
        public int ProficienciaNivelRequisito { get; set; }

        public int PrecoCompra { get; set; }
        public int PrecoVenda { get; set; }

        public double AtaqueFisico { get; set; }
        public double DefesaFisica { get; set; }

        public double VidaRestaura { get; set; }
        public double MagiaRestaura { get; set; }
        public double FomeRestaura { get; set; }

        public RPGItem() { }
    }
}
