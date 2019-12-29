using MongoDB.Bson.Serialization.Attributes;

namespace ZaynBot.RPG.Entidades
{
    public enum EnumItem
    {
        Arma,
        Escudo,
        Helmo,
        Peitoral,
        Luvas,
        Botas,
        Moeda,
        Comida,
        Pocao,
        Recurso,
    }

    [BsonIgnoreExtraElements]
    public class RPGItem
    {
        [BsonId]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public EnumItem Tipo { get; set; }
        public EnumProficiencia Proficiencia { get; set; }
        public int PrecoCompra { get; set; }
        public int PrecoVenda { get; set; }

        public double AtaqueFisico { get; set; }
        public double DefesaFisica { get; set; }

        public double VidaRestaura { get; set; }
        public double MagiaRestaura { get; set; }
        public double FomeRestaura { get; set; }

        public RPGItem(int id, string nome, EnumItem tipo)
        {
            Id = id;
            Nome = nome;
            Tipo = tipo;
            PrecoCompra = 1;
            Proficiencia = EnumProficiencia.Nenhum;
        }

        public RPGItem(int id, string nome, EnumItem tipo, EnumProficiencia proficiencia) : this(id, nome, tipo)
        {
            Proficiencia = proficiencia;
        }
    }
}
