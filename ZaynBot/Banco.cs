using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot
{
    public static class Banco
    {
        private const string LocalBancoSalvo = "mongodb://localhost";
        private const string BancoDeDados = "zaynbot";
        private const string ColecaoUsuarios = "usuarios";
        private const string ColecaoGuildas = "guildas";
        private const string ColecaoRegiões = "regions";
        public const string ObjectIDNulo = "000000000000000000000000";

        /// <summary>
        /// Consulta por um usuario no banco de dados.
        /// </summary>
        /// <param name="discordUser"></param>
        /// <returns>Usuario</returns>
        public static RPGUsuario ConsultarUsuario(ulong discordUserId)
        {
            Expression<Func<RPGUsuario, bool>> filtro = x => x.Id.Equals(discordUserId);
            RPGUsuario user = ConsultarItem(filtro, ColecaoUsuarios);
            if (user != null)
                return user;
            user = new RPGUsuario(discordUserId);
            AdicionarItem(user, ColecaoUsuarios);
            return user;
        }

        /// <summary>
        /// Altera um usuario no banco de dados.
        /// </summary>
        /// <param name="usuario"></param>
        public static void AlterarUsuario(RPGUsuario usuario)
        {
            Expression<Func<RPGUsuario, bool>> filtro = x => x.Id.Equals(usuario.Id);

            AlterarItem(filtro, usuario, ColecaoUsuarios);
        }

        /// <summary>
        /// Consulta por uma guilda no banco de dados.
        /// </summary>
        /// <param name="guildaId"></param>
        /// <returns>Guilda</returns>
        public static RPGGuilda ConsultarGuilda(ObjectId guildaId)
        {
            Expression<Func<RPGGuilda, bool>> filtro = x => x.Id.Equals(guildaId);
            RPGGuilda guilda = ConsultarItem(filtro, ColecaoGuildas);
            if (guilda != null)
            {
                return guilda;
            }
            return null;
        }

        public static ObjectId ConsultarGuildaCriador(ulong dono)
        {
            Expression<Func<RPGGuilda, bool>> filtro = x => x.IdDono.Equals(dono);
            RPGGuilda guilda = ConsultarItem(filtro, ColecaoGuildas);

            return guilda.Id;
        }

        /// <summary>
        /// Consulta por uma guilda no banco de dados.
        /// </summary>
        /// <param name="idGuilda"></param>
        /// <returns>Guilda</returns>
        public static bool CriarGuilda(RPGGuilda guilda)
        {
            Expression<Func<RPGGuilda, bool>> filtro = x => x.Nome.Equals(guilda.Nome);
            RPGGuilda guildaAchou = ConsultarItem(filtro, ColecaoGuildas);
            if (guildaAchou != null)
            {
                return false;
            }

            guildaAchou = new RPGGuilda()
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
        public static void AlterarGuilda(RPGGuilda guilda)
        {
            Expression<Func<RPGGuilda, bool>> filtro = x => x.Id.Equals(guilda.Id);

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

        private static void DeletarItem<T>(Expression<Func<T, bool>> filtro, string colecao) where T : class
        {
            IMongoClient client = new MongoClient(LocalBancoSalvo);
            IMongoDatabase database = client.GetDatabase(BancoDeDados);
            IMongoCollection<T> col = database.GetCollection<T>(colecao);

            col.DeleteOne(filtro);
        }

        public static void DeletarRegions()
        {
            IMongoClient client = new MongoClient(LocalBancoSalvo);
            IMongoDatabase database = client.GetDatabase(BancoDeDados);
            IMongoCollection<RPGRegião> col = database.GetCollection<RPGRegião>(ColecaoRegiões);
            col.DeleteMany(FilterDefinition<RPGRegião>.Empty);
        }
        public static void AdicionarRegions(RPGRegião regiao)
        {
            AdicionarItem(regiao, ColecaoRegiões);
        }
        public static RPGRegião ConsultarRegions(int id)
        {
            Expression<Func<RPGRegião, bool>> filtro = x => x.Id.Equals(id);
            RPGRegião region = ConsultarItem(filtro, ColecaoRegiões);
            if (region != null)
                return region;
            return null;

        }

        public static async Task AtualizarBancoAllAsync()
        {
            IMongoClient client = new MongoClient("mongodb://localhost");
            IMongoDatabase database = client.GetDatabase("zaynbot");
            IMongoCollection<RPGUsuario> col = database.GetCollection<RPGUsuario>("usuarios");

            await col.Find(FilterDefinition<RPGUsuario>.Empty)
                .ForEachAsync(x =>
                {
                    Expression<Func<RPGUsuario, bool>> filtro = f => f.Id.Equals(x.Id);
                    if (x.Personagem == null)
                        x.Personagem = new RPGPersonagem();
                    if (x.ConvitesGuildas == null)
                        x.ConvitesGuildas = new List<Convite>();
                    if (x.Personagem.Vivo == false)
                        x.Personagem.Vivo = true;
                    if (x.Personagem.RaçaPersonagem.Nome == null)
                        x.Personagem.RaçaPersonagem = _Gameplay.Raças.Humano.HumanoAb();
                    col.ReplaceOne(filtro, x);
                }).ConfigureAwait(false);
        }
    }
}
