using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Mapa;
using static ZaynBot.RPG.Entidades.MobRPG;

namespace ZaynBot
{
    public class ModuloBanco
    {
        public static IMongoDatabase Database { get; private set; }

        public static IMongoCollection<RegiaoRPG> RegiaoColecao { get; private set; }
        public static IMongoCollection<UsuarioRPG> UsuarioColecao { get; private set; }
        public static IMongoCollection<RPGGuilda> GuildaColecao { get; private set; }

        public static IMongoCollection<RacaRPG> RacaColecao { get; private set; }
        public static IMongoCollection<ItemRPG> ItemColecao { get; private set; }

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
            BsonClassMap.RegisterClassMap<PersonagemRPG>(m =>
            {
                m.AutoMap();
                m.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<MobRPG>(m =>
            {
                m.AutoMap();
                m.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<MobItemDropRPG>(m =>
            {
                m.AutoMap();
                m.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<ItemRPG>(m =>
            {
                m.AutoMap();
                m.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<ItemDataRPG>(m =>
            {
                m.AutoMap();
                m.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<MochilaRPG>(m =>
            {
                m.AutoMap();
                m.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<RPGGuilda>(m =>
            {
                m.AutoMap();
                m.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<BatalhaRPG>(m =>
            {
                m.AutoMap();
                m.SetIgnoreExtraElements(true);
            });
            #endregion

            RegiaoColecao = Database.GetCollection<RegiaoRPG>("regioes");
            UsuarioColecao = Database.GetCollection<UsuarioRPG>("usuarios");
            GuildaColecao = Database.GetCollection<RPGGuilda>("guildas");

            RacaColecao = Database.GetCollection<RacaRPG>("racas");
            ItemColecao = Database.GetCollection<ItemRPG>("itens");
        }

        #region CRUD Usuario

        public static UsuarioRPG UsuarioGet(ulong id)
            => UsuarioColecao.Find(x => x.Id == id).FirstOrDefault();

        public static void UsuarioEdit(UsuarioRPG usuario)
            => UsuarioColecao.ReplaceOne(x => x.Id == usuario.Id, usuario);

        #endregion

        #region CRUD Regiao

        public static RegiaoRPG RegiaoGet(int id)
            => RegiaoColecao.Find(x => x.Id == id).FirstOrDefault();

        #endregion

        #region CRUD Raca

        public static RacaRPG RacaGetRandom()
        {
            int count = (int)RacaColecao.CountDocuments(FilterDefinition<RacaRPG>.Empty);
            Random r = new Random();
            int random = (int)r.Next(0, count);
            return RacaColecao.Find(FilterDefinition<RacaRPG>.Empty).Skip(random).Limit(1).First();
        }

        #endregion

        #region CRUD Item

        public static ItemRPG ItemGet(int id)
             => ItemColecao.Find(x => x.Id == id).FirstOrDefault();

        #endregion
    }
}
