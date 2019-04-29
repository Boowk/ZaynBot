using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.IO;
using System.Threading.Tasks;
using ZaynBot.Comandos;
using ZaynBot.Entidades;

namespace ZaynBot
{
    public class Program
    {
        private ModuloComandos _todosOsComandos;
        private Cliente _cliente;
        private Config _config;
        static void Main(string[] args) => new Program().RodarOBotAsync().GetAwaiter().GetResult();


        public async Task RodarOBotAsync()
        {
            // Infelizmente no linux muda as barrinha
#if DEBUG
            string projetoRaiz = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\")) + "config.json";
#else
            string projetoRaiz = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"../../../../")) + "config.json";
#endif
            _config = Config.LoadFromFile(projetoRaiz);
            if (_config == null)
            {
                Console.WriteLine("O arquivo config.json não existe!");
                Console.WriteLine("Coloque as informações necessarias no arquivo gerado!");
                Console.WriteLine("Aperte qualquer botão para sair...");
                Console.ReadKey();
                Environment.Exit(0);
            }

            DiscordConfiguration cfg = new DiscordConfiguration
            {
#if DEBUG
                Token = _config.TokenTeste,
#else
                Token = _config.Token,
#endif
                TokenType = TokenType.Bot,
                EnableCompression = true,

                AutoReconnect = true,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = true,

            };

            Usuario user = new Usuario();
            _cliente = new Cliente(cfg, user);
            DependencyCollection dep = null;
            using (var d = new DependencyCollectionBuilder())
            {
                d.AddInstance(user);
                dep = d.Build();
            }


            _todosOsComandos = new ModuloComandos(new CommandsNextConfiguration
            {

#if DEBUG
                StringPrefix = _config.PrefixTeste,
#else
                StringPrefix = _config.Prefix,
#endif                                                              
                CaseSensitive = false,
                EnableDms = true,
                Dependencies = dep,
                EnableDefaultHelp = false,
                EnableMentionPrefix = true,
                IgnoreExtraArguments = true,
            }, Cliente.Client);

            await Cliente.Client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
