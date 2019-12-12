﻿using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    // Quando um mob morre, os itens vai para o lider do grupo. 
    // A não ser que esteja ativado o modo compartilhar.
    public class RPGBatalha
    {
        public int Turno { get; set; }
        public string NomeGrupo { get; set; }
        public ulong LiderGrupo { get; set; }
        public ulong LiderGrupoInimigo { get; set; }

        public bool ModoDividir { get; set; }

        public List<ulong> Membros { get; set; }


        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<string, RPGMob> MobsVivos { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<string, RPGMob> MobsMortos { get; set; }

        public RPGBatalha()
        {
            Turno = 0;
            NomeGrupo = "";
            LiderGrupo = 0;
            LiderGrupoInimigo = 0;


            ModoDividir = false;


            Membros = new List<ulong>();


            MobsVivos = new Dictionary<string, RPGMob>();
            MobsMortos = new Dictionary<string, RPGMob>();
        }
    }
}