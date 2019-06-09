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
    public class Banco
    {
        public static string ObjectIDNulo { get; } = "000000000000000000000000";
        public static IMongoDatabase Database { get; private set; }

        #region Coleções

        public static IMongoCollection<RPGRegião> ColecaoRegioes { get; private set; }
        public static IMongoCollection<RPGUsuario> ColecaoUsuarios { get; private set; }
        public static IMongoCollection<RPGGuilda> ColecaoGuildas { get; private set; }

        #endregion

        public Banco()
        {
            IMongoClient _client = new MongoClient("mongodb://localhost");
            Database = _client.GetDatabase("zaynbot");
            ColecaoRegioes = Database.GetCollection<RPGRegião>("regions");
            ColecaoUsuarios = Database.GetCollection<RPGUsuario>("usuarios");
            ColecaoGuildas = Database.GetCollection<RPGGuilda>("guildas");
        }

        public static async Task<RPGUsuario> ConsultarUsuarioPersonagemAsync(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            Expression<Func<RPGUsuario, bool>> filtro = x => x.Id.Equals(ctx.User.Id);
            RPGUsuario user = ColecaoUsuarios.Find(filtro).FirstOrDefault();
            if (user == null)
            {
                user = new RPGUsuario(ctx.User.Id);
                ColecaoUsuarios.InsertOne(user);
            }
            if (user.Personagem == null)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você precisa criar um personagem com o comando z!reencarnar");
                return null;
            }
            return user;
        }

        public static async Task<RPGUsuario> ConsultarUsuarioAsync(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            Expression<Func<RPGUsuario, bool>> filtro = x => x.Id.Equals(ctx.User.Id);
            RPGUsuario user = ColecaoUsuarios.Find(filtro).FirstOrDefault();
            if (user == null)
            {
                user = new RPGUsuario(ctx.User.Id);
                ColecaoUsuarios.InsertOne(user);
            }
            return user;
        }

        public static RPGUsuario ConsultarUsuario(ulong id)
        {
            Expression<Func<RPGUsuario, bool>> filtro = x => x.Id.Equals(id);
            RPGUsuario user = ColecaoUsuarios.Find(filtro).FirstOrDefault();
            if (user == null)
            {
                user = new RPGUsuario(id);
                ColecaoUsuarios.InsertOne(user);
            }
            return user;
        }

        /// <summary>
        /// Altera um usuario no banco de dados.
        /// </summary>
        /// <param name="usuario"></param>
        public static void AlterarUsuario(RPGUsuario usuario)
        {
            Expression<Func<RPGUsuario, bool>> filtro = x => x.Id.Equals(usuario.Id);
            ColecaoUsuarios.ReplaceOne(filtro, usuario);
        }

        /// <summary>
        /// Consulta por uma guilda no banco de dados.
        /// </summary>
        /// <param name="guildaId"></param>
        /// <returns>Guilda</returns>
        public static RPGGuilda ConsultarGuilda(ObjectId guildaId)
        {
            Expression<Func<RPGGuilda, bool>> filtro = x => x.Id.Equals(guildaId);
            RPGGuilda guilda = ColecaoGuildas.Find(filtro).FirstOrDefault();
            if (guilda != null)
                return guilda;
            return null;
        }

        public static ObjectId ConsultarGuildaCriador(ulong dono)
        {
            Expression<Func<RPGGuilda, bool>> filtro = x => x.IdDono.Equals(dono);
            RPGGuilda guilda = ColecaoGuildas.Find(filtro).FirstOrDefault();
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
            RPGGuilda guildaAchou = ColecaoGuildas.Find(filtro).FirstOrDefault();
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
            ColecaoGuildas.InsertOne(guildaAchou);
            return true;
        }

        /// <summary>
        /// Altera uma guilda no banco de dados.
        /// </summary>
        /// <param name="idGuilda"></param>
        public static void AlterarGuilda(RPGGuilda guilda)
        {
            Expression<Func<RPGGuilda, bool>> filtro = x => x.Id.Equals(guilda.Id);
            ColecaoGuildas.ReplaceOne(filtro, guilda);
        }

        public static RPGRegião ConsultarRegions(int id)
        {
            Expression<Func<RPGRegião, bool>> filtro = x => x.Id.Equals(id);
            RPGRegião region = ColecaoRegioes.Find(filtro).FirstOrDefault();
            if (region != null)
                return region;
            return null;
        }

        public static async Task AtualizarBancoAllAsync()
        {
            await ColecaoUsuarios.Find(FilterDefinition<RPGUsuario>.Empty)
                .ForEachAsync(x =>
                {
                    Expression<Func<RPGUsuario, bool>> filtro = f => f.Id.Equals(x.Id);
                    if (x.Personagem == null)
                        x.Personagem = new RPGPersonagem();
                    if (x.Personagem.Vivo == false)
                        x.Personagem.Vivo = true;
                    //if (x.Personagem.RaçaPersonagem.Nome == null)
                    //    x.Personagem.RaçaPersonagem = Humano.HumanoAb();
                    ColecaoUsuarios.ReplaceOne(filtro, x);
                }).ConfigureAwait(false);
        }
    }
}
