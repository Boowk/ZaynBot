using DSharpPlus.Entities;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using ZaynBot.Entidades;

namespace ZaynBot.Funções
{
    public static class Banco
    {
        private const string LocalBancoSalvo = "mongodb://localhost";
        private const string BancoDeDados = "zaynbot";
        private const string ColecaoUsuarios = "usuarios";

        /// <summary>
        /// Consulta por um usuario no banco de dados.
        /// </summary>
        /// <param name="discordUser"></param>
        /// <returns>Usuario</returns>
        public static Usuario ConsultarUsuario(DiscordUser discordUser)
        {
            Expression<Func<Usuario, bool>> filtro = x => x.Id.Equals(discordUser.Id);
            Usuario user = ConsultarItem(filtro, ColecaoUsuarios);
            if (user != null)
            {
                if (user.Personagem == null)
                {
                    user.Personagem = new Entidades.Rpg.Personagem();
                    AlterarUsuario(user);
                }
                return user;
            }
            user = new Usuario(discordUser.Id);
            AdicionarItem(user, ColecaoUsuarios);
            return user;
        }

        /// <summary>
        /// Consulta por um item no banco de dados
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="colecao"></param>
        /// <param name="filtro"></param>
        /// <returns></returns>
        private static T ConsultarItem<T>(Expression<Func<T, bool>> filtro, string colecao) where T : class
        {
            IMongoClient client = new MongoClient(LocalBancoSalvo);
            IMongoDatabase database = client.GetDatabase(BancoDeDados);
            IMongoCollection<T> col = database.GetCollection<T>(colecao);
            return col.Find(filtro).FirstOrDefault();
        }

        /// <summary>
        /// Adiciona um item no banco de dados.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="colecao"></param>
        private static void AdicionarItem<T>(T item, string colecao) where T : class
        {
            IMongoClient client = new MongoClient(LocalBancoSalvo);
            IMongoDatabase database = client.GetDatabase(BancoDeDados);
            IMongoCollection<T> col = database.GetCollection<T>(colecao);

            col.InsertOne(item);
        }

        /// <summary>
        /// Altera um item no banco de dados, com base em algum valor especifico.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="filtro"></param>
        /// <param name="item"></param>
        /// <param name="colecao"></param>
        private static void AlterarItem<T>(Expression<Func<T, bool>> filtro, T item, string colecao) where T : class
        {
            IMongoClient client = new MongoClient(LocalBancoSalvo);
            IMongoDatabase database = client.GetDatabase(BancoDeDados);
            IMongoCollection<T> col = database.GetCollection<T>(colecao);

            col.ReplaceOne(filtro, item);
        }

        /// <summary>
        /// Altera um usuario no banco de dados.
        /// </summary>
        /// <param name="usuario"></param>
        public static void AlterarUsuario(Usuario usuario)
        {
            Expression<Func<Usuario, bool>> filtro = x => x.Id.Equals(usuario.Id);

            AlterarItem(filtro, usuario, ColecaoUsuarios);
        }
    }
}
