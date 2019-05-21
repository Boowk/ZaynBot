using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class Batalha
    {
        public bool Party { get; set; } = false;
        public ulong PartyId { get; set; }
        // Para saber quem está na Party.
        public Dictionary<ulong, int> PersonagensVelocidade { get; set; } = new Dictionary<ulong, int>();
        // Após o jogador ter feito o seu "Ataque", fica salvo quem foi que atacou.
        public Dictionary<ulong, int> PersonagensVelocidadeAtacou { get; set; } = new Dictionary<ulong, int>();

        // Se for a vez do Mob - Automaticamente joga até chegar a vez de algum jogador
        public List<Mob> Inimigos { get; set; } = new List<Mob>();

        // E assim finaliza um round

        //Dictionary<string, int> dict = new Dictionary<string, int>();
        //    dict.Add("A", 3);
        //    dict.Add("B", 2);
        //    dict.Add("C", 1);

        //    List<KeyValuePair<string, int>> sorted = (from kv in dict orderby kv.Value select kv).ToList();

        //    foreach (KeyValuePair<string, int> kv in sorted)
        //    {
        //        Console.WriteLine("{0}={1}", kv.Key, kv.Value);
        //        // Prints:
        //        // C=1
        //        // B=2
        //        // A=3
        //    }
    }
}
