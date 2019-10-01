using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.IO;
using System.Threading.Tasks;
using ZaynBot.Core.Entidades;
using ZaynBot.Data.Itens;
using ZaynBot.Data.Racas;
using ZaynBot.RPG.Data.Mundos.Anker;

namespace ZaynBot
{
    public class Program
    {
        private ConfigCore _config;
        static void Main(string[] args) => new Program().RodarOBotAsync().GetAwaiter().GetResult();

        public async Task RodarOBotAsync()
        {
            // Infelizmente no linux muda as barrinha
#if DEBUG
            string projetoRaiz = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\")) + "config.json";
#else
            string projetoRaiz = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"../../../../")) + "config.json";
#endif
            _config = ConfigCore.LoadFromFile(projetoRaiz);
            if (_config == null)
            {
                Console.WriteLine("O arquivo config.json não existe!");
                Console.WriteLine("Coloque as informações necessarias no arquivo gerado!");
                Console.WriteLine("Aperte qualquer botão para sair...");
                Console.ReadKey();
                Environment.Exit(0);
            }
            BotCore.DiscordBotsApiKey = _config.DiscordBotsKey;

            DiscordConfiguration cfg = new DiscordConfiguration
            {
#if DEBUG
                Token = _config.TokenTeste,
#else
                Token = _config.Token,
#endif
                TokenType = TokenType.Bot,
                ReconnectIndefinitely = true,
                GatewayCompressionLevel = GatewayCompressionLevel.Stream,
                AutoReconnect = true,
#if DEBUG
                LogLevel = LogLevel.Debug,
#else
                LogLevel = LogLevel.Info,
#endif
                UseInternalLogHandler = true,
            };
            ModuloCliente cliente = new ModuloCliente(cfg);

            string[] prefix = new string[1];
#if DEBUG
            prefix[0] = _config.PrefixTeste;
#else
            prefix[0] = _config.Prefix;
#endif
            ModuloComando todosOsComandos = new ModuloComando(new CommandsNextConfiguration
            {
                StringPrefixes = prefix,
                EnableDms = false,
                CaseSensitive = false,
                EnableDefaultHelp = false,
                EnableMentionPrefix = true,
                IgnoreExtraArguments = true,
                PrefixResolver = PrefixResolverCustomizado,
            }, ModuloCliente.Client);
            new ModuloBanco();
            Console.WriteLine("Banco concluido.");
            new TodasAsAreas();
            Console.WriteLine("Áreas concluido.");
            new TodasAsRacas();
            Console.WriteLine("Raças concluido.");
            new TodosOsItens();
            Console.WriteLine("Itens concluido.");
            await ModuloCliente.Client.ConnectAsync();
            await Task.Delay(-1);
        }

        public static Task<int> PrefixResolverCustomizado(DiscordMessage msg)
        {
            var gld = msg.Channel.Guild;
            if (gld == null)
                return Task.FromResult(-1);

            ServidorCore slv = ModuloBanco.ServidorGet(gld.Id);
            if (slv == null)
                return Task.FromResult(-1);

            var pfixLocation = msg.GetStringPrefixLength(slv.Prefix);
            if (pfixLocation != -1)
                return Task.FromResult(pfixLocation);
            return Task.FromResult(-1);
        }
    }
}
