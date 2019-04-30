using DSharpPlus.Entities;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using ZaynBot.Entidades;

namespace ZaynBot.Funções
{
    public static class Banco
    {
        public static Usuario ConsultarUsuario(DiscordUser usuario)
        {
            IMongoClient client = new MongoClient("mongodb://localhost");
            IMongoDatabase database = client.GetDatabase("zaynbot");
            IMongoCollection<Usuario> colUsers = database.GetCollection<Usuario>("usuarios");

            Expression<Func<Usuario, bool>> filter = x => x.Id.Equals(usuario.Id);

            Usuario user = colUsers.Find(filter).FirstOrDefault();
            if (user != null)
            {
                user.Nome = usuario.Username;
                return user;
            }
            user = new Usuario
            {
                Id = usuario.Id,
                Nome = usuario.Username,
                DataMensagemEnviada = DateTime.UtcNow
            };
            colUsers.InsertOne(user);
            return user;
        }
    }
}
