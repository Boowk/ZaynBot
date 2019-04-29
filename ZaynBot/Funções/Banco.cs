using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using ZaynBot.Entidades;

namespace ZaynBot.Funções
{
    public static class Banco
    {
        public static Usuario ConsultarUsuario(ulong id)
        {
            IMongoClient client = new MongoClient("mongodb://localhost");
            IMongoDatabase database = client.GetDatabase("zaynbot");
            IMongoCollection<Usuario> colUsers = database.GetCollection<Usuario>("usuarios");

            Expression<Func<Usuario, bool>> filter = x => x.Id.Equals(id);

            Usuario user = colUsers.Find(filter).FirstOrDefault();
            if (user != null)
            {
                return user;
            }
            user = new Usuario
            {
                Id = id,
                DataMensagemEnviada = DateTime.UtcNow
            };
            colUsers.InsertOne(user);
            return user;
        }
    }
}
