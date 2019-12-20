using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System.Linq;
using ZaynBot.Core.Entidades;
using ZaynBot.RPG.Entidades;
using MongoDB.Driver.Linq;

namespace ZaynBot
{
    public class ModuloBanco
    {
        public static IMongoDatabase Database { get; private set; }
        public static IMongoCollection<RPGRegiao> RegiaoColecao { get; private set; }
        public static IMongoCollection<RPGUsuario> UsuarioColecao { get; private set; }
        public static IMongoCollection<ServidorCore> ServidorColecao { get; private set; }
        public static IMongoCollection<RPGMob> MobColecao { get; private set; }
        public static IMongoCollection<RPGItem> ItemColecao { get; private set; }
        public static IMongoCollection<RPGReceita> ReceitaColecao { get; private set; }

        public ModuloBanco()
        {
            IMongoClient _client = new MongoClient("mongodb://localhost");
            Database = _client.GetDatabase("zaynbot");

            #region Mapa
            BsonSerializer.RegisterSerializer(typeof(float),
                new SingleSerializer(BsonType.Double, new RepresentationConverter(
                true, //allow truncation
                true // allow overflow, return decimal.MinValue or decimal.MaxValue instead
            )));
            #endregion

            RegiaoColecao = Database.GetCollection<RPGRegiao>("regioes");
            UsuarioColecao = Database.GetCollection<RPGUsuario>("usuarios");
            ServidorColecao = Database.GetCollection<ServidorCore>("servidores");
            MobColecao = Database.GetCollection<RPGMob>("mobs");
            ItemColecao = Database.GetCollection<RPGItem>("itens");
            ReceitaColecao = Database.GetCollection<RPGReceita>("receitas");

            var notificationLogBuilder = Builders<RPGUsuario>.IndexKeys;
            var indexModel = new CreateIndexModel<RPGUsuario>(notificationLogBuilder.Ascending(x => x.Personagem.NivelAtual));
            UsuarioColecao.Indexes.CreateOne(indexModel);
        }

        public static RPGUsuario GetUsuario(ulong id)
            => UsuarioColecao.Find(x => x.Id == id).FirstOrDefault();

        public static void UsuarioEdit(RPGUsuario usuario)
            => UsuarioColecao.ReplaceOne(x => x.Id == usuario.Id, usuario);

        public static RPGRegiao GetRegiaoData(int id)
            => RegiaoColecao.Find(x => x.Id == id).FirstOrDefault();

        public static RPGMob GetMob(RPGRegiao regiao)
    => MobColecao.AsQueryable().Where(x => x.Dificuldade == regiao.Dificuldade).Sample(1).FirstOrDefault();

        public static ServidorCore ServidorGet(ulong id)
            => ServidorColecao.Find(x => x.Id == id).FirstOrDefault();

        public static void ServidorDel(ulong id)
            => ServidorColecao.DeleteOne(x => x.Id == id);

        public static RPGItem ItemGet(int id)
             => ItemColecao.Find(x => x.Id == id).FirstOrDefault();

        public static RPGReceita ReceitaGet(int id)
             => ReceitaColecao.Find(x => x.Id == id).FirstOrDefault();
    }
}
