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
        [BsonIgnore] public float PontosDeVidaBaseMin { get; private set; }
        [BsonIgnore] public float PontosDeManaBaseMin { get; private set; }
        [BsonIgnore] public float AtaqueFisicoBaseMin { get; private set; }
        [BsonIgnore] public float DefesaFisicaBaseMin { get; private set; }
        [BsonIgnore] public float AtaqueMagicoBaseMin { get; private set; }
        [BsonIgnore] public float DefesaMagicaBaseMin { get; private set; }
        [BsonIgnore] public int VelocidadeBaseMin { get; private set; }
        [BsonIgnore] public int SorteBaseMin { get; private set; }

        [BsonIgnore] public float PontosDeVidaBaseMax { get; private set; }
        [BsonIgnore] public float PontosDeManaBaseMax { get; private set; }
        [BsonIgnore] public float AtaqueFisicoBaseMax { get; private set; }
        [BsonIgnore] public float DefesaFisicaBaseMax { get; private set; }
        [BsonIgnore] public float AtaqueMagicoBaseMax { get; private set; }
        [BsonIgnore] public float DefesaMagicaBaseMax { get; private set; }
        [BsonIgnore] public int VelocidadeBaseMax { get; private set; }
        [BsonIgnore] public int SorteBaseMax { get; private set; }
    }
}
