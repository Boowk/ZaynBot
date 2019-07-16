using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    public class RPGGuilda
    {
        public ObjectId Id { get; set; }
        public ulong IdDono { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public List<ulong> Membros { get; set; }
        public List<Convite> Convites { get; set; }
    }

    public class Convite
    {
        public ulong IdUsuario { get; set; }
        public ObjectId IdGuilda { get; set; }
        public DateTime DataConvidado { get; set; } = DateTime.UtcNow;
    }
}
