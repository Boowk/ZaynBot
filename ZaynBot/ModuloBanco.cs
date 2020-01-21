using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System.Linq;
using ZaynBot.Core.Entidades;
using ZaynBot.RPG.Entidades;
using System.Threading.Tasks;
using System.IO;
using System;
using Newtonsoft.Json;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.Exceptions;
using ZaynBot.RPG.Exceptions;
using System.Collections.Generic;

namespace ZaynBot
{
    public class ModuloBanco
    {
        public static IMongoDatabase Database { get; private set; }
        public static IMongoCollection<RPGRegiao> ColecaoRegiao { get; private set; }
        public static IMongoCollection<RPGJogador> ColecaoJogador { get; private set; }
        public static IMongoCollection<ServidorCore> ColecaoServidor { get; private set; }
        public static IMongoCollection<RPGItem> ColecaoItem { get; private set; }
        public static IMongoCollection<RPGVenda> ColecaoVenda { get; private set; }

        public ModuloBanco()
        {
            IMongoClient _client = new MongoClient("mongodb://admin:.Sts8562@localhost");
            Database = _client.GetDatabase("zaynbot");

            BsonSerializer.RegisterSerializer(typeof(float),
                new SingleSerializer(BsonType.Double, new RepresentationConverter(
                true, //allow truncation
                true // allow overflow, return decimal.MinValue or decimal.MaxValue instead
            )));

            ColecaoRegiao = Database.GetCollection<RPGRegiao>("regioes");
            ColecaoJogador = Database.GetCollection<RPGJogador>("jogadores");
            ColecaoServidor = Database.GetCollection<ServidorCore>("servidores");
            ColecaoItem = Database.GetCollection<RPGItem>("itens");
            ColecaoVenda = Database.GetCollection<RPGVenda>("vendas");

            var notificationLogBuilder = Builders<RPGJogador>.IndexKeys;
            var indexModel = new CreateIndexModel<RPGJogador>(notificationLogBuilder.Ascending(x => x.NivelAtual));
            ColecaoJogador.Indexes.CreateOne(indexModel);
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
                    await ColecaoItem.InsertOneAsync(f);
                    quant++;
                }
            }
            Console.WriteLine($"{quant} Itens carregados!");
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
                    await ColecaoRegiao.InsertOneAsync(f);
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
                        await ColecaoRegiao.InsertOneAsync(o);
                        quant++;
                    }
                }
            }
            Console.WriteLine($"{quant} Regiões carregadas!");
        }

        #region Jogador
        /// <summary>
        /// Tenta retornar o jogador, caso o encontre no banco de dados.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="jogador"></param>
        /// <returns></returns>
        public static bool TryGetJogador(ulong id, out RPGJogador jogador)
        {
            jogador = ColecaoJogador.Find(x => x.Id == id).FirstOrDefault();
            if (jogador != null)
                return true;
            return false;
        }
        /// <summary>
        /// Recupera o jogador no banco de dados. Caso não tenha, o cria.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static RPGJogador GetJogador(ulong id)
        {
            var jogador = ColecaoJogador.Find(x => x.Id == id).FirstOrDefault();
            if (jogador == null)
            {
                jogador = new RPGJogador(id);
                ColecaoJogador.InsertOne(jogador);
            }
            return jogador;
        }
        /// <summary>
        /// Recupera o jogador no banco de dados. Caso não tenha, o cria.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static RPGJogador GetJogador(CommandContext ctx) => GetJogador(ctx.User.Id);
        /// <summary>
        /// Recupera o jogador no banco de dados. Caso não tenha, o cria.
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static RPGJogador GetJogador(DiscordUser user) => GetJogador(user.Id);
        /// <summary>
        /// Edita um jogador que esteja salvo no banco de dados.
        /// </summary>
        /// <param name="jogador"></param>
        public static void EditJogador(RPGJogador jogador) => ColecaoJogador.ReplaceOne(x => x.Id == jogador.Id, jogador);
        #endregion

        #region Região
        public static bool GetRegiao(string nome, out RPGRegiao regiao)
        {
            regiao = ColecaoRegiao.Find(x => x.Nome == nome).FirstOrDefault();
            if (regiao == null)
                return false;
            return true;
        }
        public static bool TryGetRegiao(RPGJogador jogador, out RPGRegiao regiao)
        {
            regiao = ColecaoRegiao.Find(x => x.Nome == jogador.RegiaoAtual).FirstOrDefault();
            if (regiao == null)
                return false;
            return true;
        }
        #endregion

        #region Servidor
        public static ServidorCore GetServidor(ulong id)
        {
            ServidorCore server = ColecaoServidor.Find(x => x.Id == id).FirstOrDefault();
            if (server == null)
            {
                server = new ServidorCore()
                {
                    Id = id,
                };
                ColecaoServidor.InsertOne(server);
            }
            return server;
        }
        public static void EditServidor(ServidorCore server) => ColecaoServidor.ReplaceOne(x => x.Id == server.Id, server);
        #endregion

        #region Vendas

        public static bool TryGetVenda(ulong id, int slot, out RPGVenda venda)
        {
            venda = ColecaoVenda.Find(x => x.JogadorId == id && x.Slot == slot).FirstOrDefault();
            if (venda != null)
                return true;
            return false;
        }

        public static bool TryGetVenda(ulong id, out List<RPGVenda> venda)
        {
            venda = ColecaoVenda.Find(x => x.JogadorId == id).ToList();
            if (venda != null)
                return true;
            return false;
        }

        public static void EditVenda(RPGVenda venda) => ColecaoVenda.ReplaceOne(x => x.JogadorId == venda.JogadorId, venda);

        #endregion

        public static bool TryGetItem(string nome, out RPGItem item)
        {
            item = ColecaoItem.Find(x => x.Nome == nome).FirstOrDefault();
            if (item == null)
                return false;
            return true;
        }
    }
}
