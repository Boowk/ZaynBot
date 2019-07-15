using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot
{
    public class ModuloBanco
    {
        public static IMongoDatabase Database { get; private set; }

        #region Colecoes

        public static IMongoCollection<RPGRegiao> RegiaoColecao { get; private set; }
        public static IMongoCollection<RPGUsuario> UsuarioColecao { get; private set; }
        public static IMongoCollection<RPGGuilda> GuildaColecao { get; private set; }
        public static IMongoCollection<RPGRaça> RacaColecao { get; private set; }
        public static IMongoCollection<RPGMissao> MissaoColecao { get; private set; }

        #endregion

        public ModuloBanco()
        {
            IMongoClient _client = new MongoClient("mongodb://localhost");
            Database = _client.GetDatabase("zaynbot");
            RegiaoColecao = Database.GetCollection<RPGRegiao>("regioes");
            UsuarioColecao = Database.GetCollection<RPGUsuario>("usuarios");
            GuildaColecao = Database.GetCollection<RPGGuilda>("guildas");
            RacaColecao = Database.GetCollection<RPGRaça>("racas");
            MissaoColecao = Database.GetCollection<RPGMissao>("missoes");
        }

        #region CRUD Usuario

        public static RPGUsuario GetUsuario(ulong id)
        {
            Expression<Func<RPGUsuario, bool>> filtro = x => x.Id == id;
            return UsuarioColecao.Find(filtro).FirstOrDefault();
        }

        public static void UpdateUsuario(RPGUsuario usuario)
        {
            Expression<Func<RPGUsuario, bool>> filtro = x => x.Id == usuario.Id;
            UsuarioColecao.ReplaceOne(filtro, usuario);
        }

        #endregion
        #region CRUD Guilda

        /// <summary>
        /// Consulta por uma guilda no banco de dados.
        /// </summary>
        /// <param name="guildaId"></param>
        /// <returns>Guilda</returns>
        public static RPGGuilda GuildaConsultar(ObjectId guildaId)
        {
            Expression<Func<RPGGuilda, bool>> filtro = x => x.Id.Equals(guildaId);
            RPGGuilda guilda = GuildaColecao.Find(filtro).FirstOrDefault();
            if (guilda != null)
                return guilda;
            return null;
        }

        public static ObjectId GuildaConsultarCriador(ulong dono)
        {
            Expression<Func<RPGGuilda, bool>> filtro = x => x.IdDono.Equals(dono);
            RPGGuilda guilda = GuildaColecao.Find(filtro).FirstOrDefault();
            return guilda.Id;
        }

        /// <summary>
        /// Consulta por uma guilda no banco de dados.
        /// </summary>
        /// <param name="idGuilda"></param>
        /// <returns>Guilda</returns>
        public static bool GuildaCriar(RPGGuilda guilda)
        {
            Expression<Func<RPGGuilda, bool>> filtro = x => x.Nome.Equals(guilda.Nome);
            RPGGuilda guildaAchou = GuildaColecao.Find(filtro).FirstOrDefault();
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
            GuildaColecao.InsertOne(guildaAchou);
            return true;
        }

        /// <summary>
        /// Altera uma guilda no banco de dados.
        /// </summary>
        /// <param name="idGuilda"></param>
        public static void GuildaAlterar(RPGGuilda guilda)
        {
            Expression<Func<RPGGuilda, bool>> filtro = x => x.Id.Equals(guilda.Id);
            GuildaColecao.ReplaceOne(filtro, guilda);
        }

        #endregion
        #region CRUD Regiao

        public static RPGRegiao GetRPGRegiao(int id)
        {
            Expression<Func<RPGRegiao, bool>> filtro = x => x.Id == id;
            return RegiaoColecao.Find(filtro).FirstOrDefault();
        }

        #endregion
        #region CRUD Missao

        public static RPGMissao MissaoConsultar(int id)
        {
            Expression<Func<RPGMissao, bool>> filtro = x => x.Id.Equals(id);
            RPGMissao missao = MissaoColecao.Find(filtro).FirstOrDefault();
            if (missao != null)
                return missao;
            return null;
        }

        #endregion
    }
}
