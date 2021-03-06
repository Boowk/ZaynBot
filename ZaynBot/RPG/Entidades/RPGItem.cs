﻿using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGItem
    {
        [BsonId]
        public string Nome { get; set; }
        public string Descricao { get; set; }

        public EnumTipo Tipo { get; set; }
        public EnumProficiencia Proficiencia { get; set; }
        public int ProficienciaNivelRequisito { get; set; }

        public int PrecoCompra { get; set; }
        public bool PodeComprar { get; set; }

        public double AtaqueFisico { get; set; }
        public double AtaqueMagico { get; set; }

        public double DefesaFisica { get; set; }
        public double DefesaMagica { get; set; }

        public double VidaRestaura { get; set; }
        public double MagiaRestaura { get; set; }
        public double FomeRestaura { get; set; }
    }
}
