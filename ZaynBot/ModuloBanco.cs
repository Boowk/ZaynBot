using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System.Linq;
using ZaynBot.Core.Entidades;
using ZaynBot.RPG.Entidades;
using MongoDB.Driver.Linq;
using System.Threading.Tasks;
using System.IO;
using System;
using Newtonsoft.Json;

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

        public static async Task CarregarItensAsync()
        {
            Database.DropCollection("itens");
            var Files = Directory.EnumerateFiles(Program.EntrarPasta(@"Data/Itens"), "*.json", SearchOption.AllDirectories);
            int quant = 0;
            foreach (var file in Files)
            {
                using (var sr = new StreamReader(file))
                {
                    var f = Newtonsoft.Json.JsonConvert.DeserializeObject<RPGItem>(sr.ReadToEnd());
                    await ItemColecao.InsertOneAsync(f);
                    quant++;
                }
            }
            Console.WriteLine($"{quant} Itens carregados!");
        }

        public static async Task CarregarMobsAsync()
        {
            Database.DropCollection("mobs");
            var Files = Directory.EnumerateFiles(Program.EntrarPasta(@"Data/Mobs"), "*.json", SearchOption.AllDirectories);
            int quant = 0;
            foreach (var file in Files)
            {
                using (var sr = new StreamReader(file))
                {
                    var f = JsonConvert.DeserializeObject<RPGMob>(sr.ReadToEnd());
                    await MobColecao.InsertOneAsync(f);
                    quant++;
                }
            }
            Console.WriteLine($"{quant} Mobs carregados!");
        }

        public static async Task CarregarRegioesAsync()
        {
            Database.DropCollection("regioes");
            var Files = Directory.EnumerateFiles(Program.EntrarPasta(@"Data/Regioes"), "*.json", SearchOption.AllDirectories);
            int quant = 0;
            foreach (var file in Files)
            {
                using (var sr = new StreamReader(file))
                {
                    var f = Newtonsoft.Json.JsonConvert.DeserializeObject<RPGRegiao>(sr.ReadToEnd());
                    await RegiaoColecao.InsertOneAsync(f);
                    quant++;
                }
            }
            Console.WriteLine($"{quant} Regiões carregadas!");
        }

        public static async Task CarregarRegioesTrizbortAsync()
        {
            Database.DropCollection("regioes");

            int quant = 0;
            JsonSerializer serializer = new JsonSerializer();
            using (FileStream s = File.Open(Program.EntrarPasta(@"Data/Regioes") + "/Regioes.json", FileMode.Open))
            using (StreamReader sr = new StreamReader(s))
            using (JsonReader reader = new JsonTextReader(sr))
            {
                while (reader.Read())
                {
                    if (reader.TokenType == JsonToken.StartObject)
                    {
                        var o = serializer.Deserialize<RPGRegiao>(reader);
                        await RegiaoColecao.InsertOneAsync(o);
                        quant++;
                    }
                }
            }
            Console.WriteLine($"{quant} Regiões carregadas!");
        }

        public static RPGUsuario GetUsuario(ulong id)
            => UsuarioColecao.Find(x => x.Id == id).FirstOrDefault();

        public static void UsuarioEdit(RPGUsuario usuario)
            => UsuarioColecao.ReplaceOne(x => x.Id == usuario.Id, usuario);

        public static RPGRegiao GetRegiaoData(int id)
            => RegiaoColecao.Find(x => x.Id == id).FirstOrDefault();

        public static RPGMob GetMob(RPGRegiao regiao)
          => MobColecao.AsQueryable().Where(x => x.Dificuldade == regiao.Dificuldade).Sample(1).FirstOrDefault();

        public static ServidorCore GetServidor(ulong id)
        {
            ServidorCore server = ServidorColecao.Find(x => x.Id == id).FirstOrDefault();
            if (server == null)
            {
                server = new ServidorCore()
                {
                    Id = id,
                };
                ServidorColecao.InsertOne(server);
            }
            return server;
        }

        public static void EditServidor(ServidorCore server)
             => ServidorColecao.ReplaceOne(x => x.Id == server.Id, server);

        public static RPGItem GetItem(int id)
             => ItemColecao.Find(x => x.Id == id).FirstOrDefault();

        public static RPGReceita ReceitaGet(int id)
             => ReceitaColecao.Find(x => x.Id == id).FirstOrDefault();
    }
}
