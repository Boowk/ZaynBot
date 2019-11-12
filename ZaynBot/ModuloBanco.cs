using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using ZaynBot.Core.Entidades;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Mapa;
using static ZaynBot.RPG.Entidades.MobRPG;
using MongoDB.Driver.Linq;

namespace ZaynBot
{
    public class ModuloBanco
    {
        public static IMongoDatabase Database { get; private set; }

        public static IMongoCollection<RegiaoRPG> RegiaoColecao { get; private set; }
        public static IMongoCollection<UsuarioRPG> UsuarioColecao { get; private set; }
        public static IMongoCollection<ServidorCore> ServidorColecao { get; private set; }
        public static IMongoCollection<MobRPG> MobColecao { get; private set; }
        public static IMongoCollection<ItemRPG> ItemColecao { get; private set; }
        public static IMongoCollection<ReceitaRPG> ReceitaColecao { get; private set; }

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
            BsonClassMap.RegisterClassMap<InventarioRPG>(m =>
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
            ServidorColecao = Database.GetCollection<ServidorCore>("servidores");
            MobColecao = Database.GetCollection<MobRPG>("mobs");
            ItemColecao = Database.GetCollection<ItemRPG>("itens");
            ReceitaColecao = Database.GetCollection<ReceitaRPG>("receitas");
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

        #region CRUD Mobs

        public static List<MobRPG> MobsGet(int dificuldade)
        {
            return MobColecao.Find(x => x.Dificuldade == dificuldade).ToList();
            //MobColecao.AsQueryable().Where(x => x.Dificuldade == dificuldade).Sample(6);
        }

        #endregion

        #region CRUD Servidor

        public static ServidorCore ServidorGet(ulong id)
            => ServidorColecao.Find(x => x.Id == id).FirstOrDefault();

        public static void ServidorDel(ulong id)
            => ServidorColecao.DeleteOne(x => x.Id == id);

        #endregion

        #region CRUD Item

        public static ItemRPG ItemGet(int id)
             => ItemColecao.Find(x => x.Id == id).FirstOrDefault();

        #endregion

        #region CRUD Receita

        public static ReceitaRPG ReceitaGet(int id)
             => ReceitaColecao.Find(x => x.Id == id).FirstOrDefault();

        #endregion
    }
}
