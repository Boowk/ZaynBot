﻿using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGBatalha
    {
        public float PontosDeAcao { get; set; }

        public int Turno { get; set; } = 0;
        public List<RPGMob> Inimigos { get; set; } = new List<RPGMob>();

        public float PontosDeAcaoBase { get; set; }
    }
}
