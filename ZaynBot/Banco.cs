using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZaynBot.Entidades;
using ZaynBot.Entidades.EntidadesRpg;

namespace ZaynBot
{
    public static class Banco
    {
        private const string LocalBancoSalvo = "mongodb://localhost";
        private const string BancoDeDados = "zaynbot";
        private const string ColecaoUsuarios = "usuarios";
        private const string ColecaoGuildas = "guildas";
        private const string ColecaoServidores = "servidores";
        public const string ObjectIDNulo = "000000000000000000000000";

        /// <summary>
        /// Consulta por um usuario no banco de dados.
        /// </summary>
        /// <param name="discordUser"></param>
        /// <returns>Usuario</returns>
        public static Usuario ConsultarUsuario(ulong discordUserId)
        {
            Expression<Func<Usuario, bool>> filtro = x => x.Id.Equals(discordUserId);
            Usuario user = ConsultarItem(filtro, ColecaoUsuarios);
            if (user != null)
            {
                //if (user.Personagem == null)
                //{
                //    user.Personagem = new Personagem();

                //    AlterarUsuario(user);
                //}
                //if (user.ConvitesGuildas == null)
                //{
                //    user.ConvitesGuildas = new System.Collections.Generic.List<Convite>();
                //}
                return user;
            }
            user = new Usuario(discordUserId);
            AdicionarItem(user, ColecaoUsuarios);
            return user;
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

        /// <summary>
        /// Consulta por uma guilda no banco de dados.
        /// </summary>
        /// <param name="idGuilda"></param>
        /// <returns>Guilda</returns>
        public static Guilda ConsultarGuilda(ObjectId idGuilda)
        {
            Expression<Func<Guilda, bool>> filtro = x => x.Id.Equals(idGuilda);
            Guilda guilda = ConsultarItem(filtro, ColecaoGuildas);
            if (guilda != null)
            {
                return guilda;
            }
            return null;
        }

        public static ObjectId ConsultarGuildaCriador(ulong dono)
        {
            Expression<Func<Guilda, bool>> filtro = x => x.IdDono.Equals(dono);
            Guilda guilda = ConsultarItem(filtro, ColecaoGuildas);

            return guilda.Id;
        }

        /// <summary>
        /// Consulta por uma guilda no banco de dados.
        /// </summary>
        /// <param name="idGuilda"></param>
        /// <returns>Guilda</returns>
        public static bool CriarGuilda(Guilda guilda)
        {
            Expression<Func<Guilda, bool>> filtro = x => x.Nome.Equals(guilda.Nome);
            Guilda guildaAchou = ConsultarItem(filtro, ColecaoGuildas);
            if (guildaAchou != null)
            {
                return false;
            }

            guildaAchou = new Guilda()
            {
                Convites = new System.Collections.Generic.List<Convite>(),
                Nome = guilda.Nome,
                IdDono = guilda.IdDono,
                Membros = new System.Collections.Generic.List<ulong>(),
                Descricao = "Sem descrição",
                Id = new ObjectId(),
            };
            AdicionarItem(guildaAchou, ColecaoGuildas);
            return true;
        }

        /// <summary>
        /// Altera uma guilda no banco de dados.
        /// </summary>
        /// <param name="idGuilda"></param>
        public static void AlterarGuilda(Guilda guilda)
        {
            Expression<Func<Guilda, bool>> filtro = x => x.Id.Equals(guilda.Id);

            AlterarItem(filtro, guilda, ColecaoGuildas);
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

        public static async Task AtualizarBancoAllAsync()
        {
            IMongoClient client = new MongoClient("mongodb://localhost");
            IMongoDatabase database = client.GetDatabase("zaynbot");
            IMongoCollection<Usuario> col = database.GetCollection<Usuario>("usuarios");

            await col.Find(FilterDefinition<Usuario>.Empty)
                .ForEachAsync(x =>
                {
                    Expression<Func<Usuario, bool>> filtro = f => f.Id.Equals(x.Id);
                    if (x.Personagem == null)
                        x.Personagem = new Personagem();
                    if (x.ConvitesGuildas == null)
                        x.ConvitesGuildas = new List<Convite>();
                    if (x.Personagem.LocalAtual == null)
                        x.Personagem.LocalAtual = new Entidades.EntidadesRpg.EntidadesRpgMapa.Região();
                    col.ReplaceOne(filtro, x);
                }).ConfigureAwait(false);
        }
    }
}
