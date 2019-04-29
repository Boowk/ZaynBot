using DSharpPlus.EventArgs;
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
                user.DataMensagemEnviada = DateTime.UtcNow.AddMinutes(3);
                bool evoluiu = user.AdicionarExp(20);

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
