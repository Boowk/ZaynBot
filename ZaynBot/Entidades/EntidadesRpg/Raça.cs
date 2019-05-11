using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.Entidades.EntidadesRpg
{
    [BsonIgnoreExtraElements]
    public class Raça
    {
        public string Nome { get; set; }
        [BsonIgnore] public float PontosDeVidaBaseMin { get; set; }
        [BsonIgnore] public float PontosDeManaBaseMin { get; set; }
        [BsonIgnore] public float AtaqueFisicoBaseMin { get; set; }
        [BsonIgnore] public float DefesaFisicaBaseMin { get; set; }
        [BsonIgnore] public float AtaqueMagicoBaseMin { get; set; }
        [BsonIgnore] public float DefesaMagicaBaseMin { get; set; }
        [BsonIgnore] public int VelocidadeBaseMin { get; set; }

        [BsonIgnore] public float PontosDeVidaBaseMax { get; set; }
        [BsonIgnore] public float PontosDeManaBaseMax { get; set; }
        [BsonIgnore] public float AtaqueFisicoBaseMax { get; set; }
        [BsonIgnore] public float DefesaFisicaBaseMax { get; set; }
        [BsonIgnore] public float AtaqueMagicoBaseMax { get; set; }
        [BsonIgnore] public float DefesaMagicaBaseMax { get; set; }
        [BsonIgnore] public int VelocidadeBaseMax { get; set; }
    }
}
