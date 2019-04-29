using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Exceptions;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Entidades;

namespace ZaynBot.Eventos
{
    public static class MensagemNovaEnviada
    {
        public static async Task XpUsuario(MessageCreateEventArgs e, Usuario user)
        {
            if (DateTime.UtcNow >= user.DataMensagemEnviada)
            {
                user.DataMensagemEnviada = DateTime.UtcNow.AddMinutes(2).AddSeconds(30);
                Random random = new Random();
                bool evoluiu = user.AdicionarExp(random.Next(5, 26));

                IMongoClient client = new MongoClient("mongodb://localhost");
                IMongoDatabase database = client.GetDatabase("zaynbot");
                IMongoCollection<Usuario> colUsers = database.GetCollection<Usuario>("usuarios");

                Expression<Func<Usuario, bool>> filter = x => x.Id.Equals(user.Id);

                Usuario userSalvar = colUsers.Find(filter).FirstOrDefault();
                colUsers.ReplaceOne(filter, user);
                if (evoluiu == true) await e.Channel.SendMessageAsync($"Parabéns {e.Author.Mention}, você evoluiu para o nível {user.Nivel}! Vida e Mana Aumentadas!");
            }
            await Task.CompletedTask;
        }
    }
}
