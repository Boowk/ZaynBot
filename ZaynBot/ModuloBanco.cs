using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System.Linq;
using System;
using ZaynBot.Entidades;

namespace ZaynBot
{
    public class ModuloBanco
    {
        public static IMongoDatabase Database { get; private set; }
        public static IMongoCollection<EntidadeUsuario> Usuarios { get; private set; }
        //public static IMongoCollection<ServidorCore> ColecaoServidor { get; private set; }

        public ModuloBanco()
        {
#if DEBUG
            IMongoClient _client = new MongoClient("mongodb://localhost");
#else
            IMongoClient _client = new MongoClient("mongodb://admin:.Sts8562@localhost");
#endif
            Database = _client.GetDatabase("ZaynBot");

            BsonSerializer.RegisterSerializer(typeof(float),
                new SingleSerializer(BsonType.Double, new RepresentationConverter(
                true, //allow truncation
                true // allow overflow, return decimal.MinValue or decimal.MaxValue instead
            )));

            Usuarios = Database.GetCollection<EntidadeUsuario>("usuarios");
            // ColecaoServidor = Database.GetCollection<ServidorCore>("servidores");

            //var notificationLogBuilder = Builders<RPGJogador>.IndexKeys;
            //var indexModel = new CreateIndexModel<RPGJogador>(notificationLogBuilder.Ascending(x => x.NivelAtual));
            //ColecaoJogador.Indexes.CreateOne(indexModel);
        }

        public static EntidadeUsuario GetUsuario(ulong id)
        {
            var usuario = Usuarios.Find(x => x.Id == id).FirstOrDefault();
            if (usuario == null)
            {
                usuario = new EntidadeUsuario(id);
                Usuarios.InsertOne(usuario);
            }
            return usuario;
        }

        public static void EditUsuario(EntidadeUsuario usuario)
        {
            ulong revAnterior = usuario.Rev;
            usuario.Rev++;
            var result = Usuarios.ReplaceOne(x => x.Id == usuario.Id && x.Rev == revAnterior, usuario);
            if (result.MatchedCount == 0)
                throw new Exception();
        }
    }
}
