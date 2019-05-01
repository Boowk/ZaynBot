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
                return user;
            }
            user = new Usuario
            {
                Id = usuario.Id,          
                DataMensagemEnviada = DateTime.UtcNow
            };
            colUsers.InsertOne(user);
            return user;
        }

        public static void AlterarUsuario(Usuario usuario)
        {
            IMongoClient client = new MongoClient("mongodb://localhost");
            IMongoDatabase database = client.GetDatabase("zaynbot");
            IMongoCollection<Usuario> colUsers = database.GetCollection<Usuario>("usuarios");

            Expression<Func<Usuario, bool>> filter = x => x.Id.Equals(usuario.Id);

            colUsers.ReplaceOne(filter, usuario);
        }
    }
}
