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
        [BsonIgnore] public int PontosDeVidaBase { get; set; }
        [BsonIgnore] public int PontosDeManaBase { get; set; }
        [BsonIgnore] public int AtaqueFisicoBase { get; set; }
        [BsonIgnore] public int DefesaFisicaBase { get; set; }
        [BsonIgnore] public int AtaqueMagicoBase { get; set; }
        [BsonIgnore] public int DefesaMagicaBase { get; set; }
        [BsonIgnore] public int VelocidadeBase { get; set; }

        [BsonIgnore] public int 

        // Habilidades Base
    }
}
