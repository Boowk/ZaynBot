//using MongoDB.Bson;
//using MongoDB.Bson.Serialization;
//using MongoDB.Bson.Serialization.Options;
//using MongoDB.Bson.Serialization.Serializers;
//using MongoDB.Driver;
//using System.Linq;
//using ZaynBot.Core.Entidades;
//using ZaynBot.RPG.Entidades;
//using System.Threading.Tasks;
//using System.IO;
//using System;
//using Newtonsoft.Json;
//using DSharpPlus.CommandsNext;
//using DSharpPlus.Entities;
//using System.Collections.Generic;

//namespace ZaynBot
//{
//    public class ModuloBanco
//    {
//        public static IMongoDatabase Database { get; private set; }
//        public static IMongoCollection<RPGJogador> ColecaoJogador { get; private set; }
//        public static IMongoCollection<ServidorCore> ColecaoServidor { get; private set; }

//        public ModuloBanco()
//        {
//            //IMongoClient _client = new MongoClient("mongodb://localhost");
//            IMongoClient _client = new MongoClient("mongodb://admin:.Sts8562@localhost");
//            Database = _client.GetDatabase("zaynbot");

//            BsonSerializer.RegisterSerializer(typeof(float),
//                new SingleSerializer(BsonType.Double, new RepresentationConverter(
//                true, //allow truncation
//                true // allow overflow, return decimal.MinValue or decimal.MaxValue instead
//            )));

//            ColecaoJogador = Database.GetCollection<RPGJogador>("jogadores");
//            ColecaoServidor = Database.GetCollection<ServidorCore>("servidores");

//            var notificationLogBuilder = Builders<RPGJogador>.IndexKeys;
//            var indexModel = new CreateIndexModel<RPGJogador>(notificationLogBuilder.Ascending(x => x.NivelAtual));
//            ColecaoJogador.Indexes.CreateOne(indexModel);
//        }

   

//        #region Jogador
//        /// <summary>
//        /// Tenta retornar o jogador, caso o encontre no banco de dados.
//        /// </summary>
//        /// <param name="id"></param>
//        /// <param name="jogador"></param>
//        /// <returns></returns>
//        public static bool TryGetJogador(ulong id, out RPGJogador jogador)
//        {
//            jogador = ColecaoJogador.Find(x => x.Id == id).FirstOrDefault();
//            if (jogador != null)
//                return true;
//            return false;
//        }
//        /// <summary>
//        /// Recupera o jogador no banco de dados. Caso não tenha, o cria.
//        /// </summary>
//        /// <param name="id"></param>
//        /// <returns></returns>
//        public static RPGJogador GetJogador(ulong id)
//        {
//            var jogador = ColecaoJogador.Find(x => x.Id == id).FirstOrDefault();
//            if (jogador == null)
//            {
//                jogador = new RPGJogador(id);
//                ColecaoJogador.InsertOne(jogador);
//            }
//            return jogador;
//        }
//        /// <summary>
//        /// Recupera o jogador no banco de dados. Caso não tenha, o cria.
//        /// </summary>
//        /// <param name="ctx"></param>
//        /// <returns></returns>
//        public static RPGJogador GetJogador(CommandContext ctx) => GetJogador(ctx.User.Id);
//        /// <summary>
//        /// Recupera o jogador no banco de dados. Caso não tenha, o cria.
//        /// </summary>
//        /// <param name="user"></param>
//        /// <returns></returns>
//        public static RPGJogador GetJogador(DiscordUser user) => GetJogador(user.Id);
//        /// <summary>
//        /// Edita um jogador que esteja salvo no banco de dados.
//        /// </summary>
//        /// <param name="jogador"></param>
//        public static void EditJogador(RPGJogador jogador) => ColecaoJogador.ReplaceOne(x => x.Id == jogador.Id, jogador);
//        #endregion


//        #region Servidor
//        public static ServidorCore GetServidor(ulong id)
//        {
//            ServidorCore server = ColecaoServidor.Find(x => x.Id == id).FirstOrDefault();
//            if (server == null)
//            {
//                server = new ServidorCore()
//                {
//                    Id = id,
//                };
//                ColecaoServidor.InsertOne(server);
//            }
//            return server;
//        }
//        public static void EditServidor(ServidorCore server) => ColecaoServidor.ReplaceOne(x => x.Id == server.Id, server);
//        #endregion

       

//    }
//}
