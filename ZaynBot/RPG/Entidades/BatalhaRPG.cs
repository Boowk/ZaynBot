using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    // Quando um mob morre, os itens vai parao lider da party. 
    // A não ser que esteja ativado o modo compartilhar.
    public class BatalhaRPG
    {
        public int Turno { get; set; }
        public ulong LiderParty { get; set; }
        public ulong LiderPartyInimiga { get; set; }

        public List<ulong> Jogadores { get; set; }

        public bool ModoDividir { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<string, MobRPG> Mobs { get; set; }

        public BatalhaRPG()
        {
            Turno = 0;
            LiderParty = 0;
            LiderPartyInimiga = 0;


            ModoDividir = false;


            Jogadores = new List<ulong>();


            Mobs = new Dictionary<string, MobRPG>();
        }
    }
}