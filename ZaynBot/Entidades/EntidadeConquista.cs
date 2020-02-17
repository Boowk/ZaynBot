using MongoDB.Bson.Serialization.Attributes;
using System;

namespace ZaynBot.Entidades
{
    [BsonIgnoreExtraElements]
    public class EntidadeConquista
    {
        public string Nome { get; private set; }
        public ulong Progresso { get; private set; }
        public DateTime ProxTrigger { get; private set; }

        public EntidadeConquista() { }

        public EntidadeConquista(string nome)
        {
            this.Nome = nome;
            this.Progresso = 0;
            this.ProxTrigger = DateTime.UtcNow;
        }

        public void AdicionarProgresso(TimeSpan tempo)
        {
            this.ProxTrigger = DateTime.UtcNow + tempo;
            this.Progresso++;
        }
    }
}
