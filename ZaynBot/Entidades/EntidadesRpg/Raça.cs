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
        [BsonIgnore] public float PontosDeVidaBase { get; set; }
        [BsonIgnore] public float PontosDeManaBase { get; set; }
        [BsonIgnore] public float AtaqueFisicoBase { get; set; }
        [BsonIgnore] public float DefesaFisicaBase { get; set; }
        [BsonIgnore] public float AtaqueMagicoBase { get; set; }
        [BsonIgnore] public float DefesaMagicaBase { get; set; }
        [BsonIgnore] public int VelocidadeBase { get; set; }

        //[BsonIgnore] public int

        // Habilidades Base
    }
}
