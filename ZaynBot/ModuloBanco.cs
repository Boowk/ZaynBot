using DSharpPlus.CommandsNext;
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
    public class ModuloBanco
    {
        public static IMongoDatabase Database { get; private set; }

        #region Colecoes

        public static IMongoCollection<RPGRegião> RegiaoColecao { get; private set; }
        public static IMongoCollection<RPGUsuario> UsuarioColecao { get; private set; }
        public static IMongoCollection<RPGGuilda> GuildaColecao { get; private set; }
        public static IMongoCollection<RPGRaça> RacaColecao { get; private set; }
        public static IMongoCollection<RPGMissao> MissaoColecao { get; private set; }

        #endregion

        public ModuloBanco()
        {
            IMongoClient _client = new MongoClient("mongodb://localhost");
            Database = _client.GetDatabase("zaynbot");
            RegiaoColecao = Database.GetCollection<RPGRegião>("regions");
            UsuarioColecao = Database.GetCollection<RPGUsuario>("usuarios");
            GuildaColecao = Database.GetCollection<RPGGuilda>("guildas");
            RacaColecao = Database.GetCollection<RPGRaça>("racas");
            MissaoColecao = Database.GetCollection<RPGMissao>("missoes");
        }

        #region CRUD Usuario

        /// <summary>
        /// Procura no banco por um usuario e verifica se existe um personagem antes de devolver.
        /// </summary>
        /// <param name="CommandContext"></param>
        /// <returns></returns>
        public static async Task<RPGUsuario> UsuarioConsultarPersonagemAsync(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            Expression<Func<RPGUsuario, bool>> filtro = x => x.Id.Equals(ctx.User.Id);
            RPGUsuario user = UsuarioColecao.Find(filtro).FirstOrDefault();
            if (user == null)
            {
                user = new RPGUsuario(ctx.User.Id);
                UsuarioColecao.InsertOne(user);
            }
            if (user.Personagem == null)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você precisa criar um personagem com o comando z!reencarnar");
                return user;
            }
            CancelamentoToken.CancelarToken(ctx);
            return user;
        }

        /// <summary>
        /// Procura no banco por um usuario e o devolve sem verificar personagem.
        /// </summary>
        /// <param name="CommandContext"></param>
        /// <returns></returns>
        public static async Task<RPGUsuario> UsuarioConsultarAsync(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            return UsuarioConsultar(ctx.User.Id);
        }

        /// <summary>
        /// Procura no banco por um usuario e o devolve sem verificar personagem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static RPGUsuario UsuarioConsultar(ulong id)
        {
            Expression<Func<RPGUsuario, bool>> filtro = x => x.Id.Equals(id);
            RPGUsuario user = UsuarioColecao.Find(filtro).FirstOrDefault();
            if (user == null)
            {
                user = new RPGUsuario(id);
                UsuarioColecao.InsertOne(user);
            }
            return user;
        }

        /// <summary>
        /// Altera um usuario no banco.
        /// </summary>
        /// <param name="usuario"></param>
        public static void UsuarioAlterar(RPGUsuario usuario)
        {
            Expression<Func<RPGUsuario, bool>> filtro = x => x.Id.Equals(usuario.Id);
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

        public static RPGRegião RegiaoConsultar(int id)
        {
            Expression<Func<RPGRegião, bool>> filtro = x => x.Id.Equals(id);
            RPGRegião region = RegiaoColecao.Find(filtro).FirstOrDefault();
            if (region != null)
                return region;
            return null;
        }

        #endregion
        #region CRUD Raca

        public static RPGRaça RacaConsultar(int id)
        {
            Expression<Func<RPGRaça, bool>> filtro = x => x.Id.Equals(id);
            RPGRaça raca = RacaColecao.Find(filtro).FirstOrDefault();
            if (raca != null)
                return raca;
            return null;
        }

        public static RPGRaça RacaConsultar(string nome)
        {
            Expression<Func<RPGRaça, bool>> filtro = x => x.Nome.ToLower() == nome;
            RPGRaça raca = RacaColecao.Find(filtro).FirstOrDefault();
            if (raca != null)
                return raca;
            return null;
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

        public static async Task AtualizarBancoAllAsync()
        {
            //List<int> racasdisponiveis = new List<int>
            //{
            //    0,
            //    1,
            //    2,
            //    3,
            //};

            //await UsuarioColecao.Find(FilterDefinition<RPGUsuario>.Empty)
            //    .ForEachAsync(x =>
            //    {
            //        Expression<Func<RPGUsuario, bool>> filtro = f => f.Id.Equals(x.Id);
            //        if (x.Personagem != null)
            //        {
            //            x.RacasDisponiveisId = racasdisponiveis;
            //            UsuarioColecao.ReplaceOne(filtro, x);
            //        }
            //    }).ConfigureAwait(false);
        }
    }
}
