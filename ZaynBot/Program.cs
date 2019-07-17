using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.IO;
using System.Threading.Tasks;
using ZaynBot.Core.Entidades;
using ZaynBot.Data.Itens;
using ZaynBot.Data.Missoes;
using ZaynBot.Data.Raças;
using ZaynBot.RPG.Data.Mundos.Anker;

namespace ZaynBot
{
    public class Program
    {
        private ModuloComando _todosOsComandos;
        private ModuloCliente _cliente;
        private CoreConfig _config;
        static void Main(string[] args) => new Program().RodarOBotAsync().GetAwaiter().GetResult();

        public async Task RodarOBotAsync()
        {
            // Infelizmente no linux muda as barrinha
#if DEBUG
            string projetoRaiz = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"..\..\..\..\")) + "config.json";
#else
            string projetoRaiz = Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"../../../../")) + "config.json";
#endif
            _config = CoreConfig.LoadFromFile(projetoRaiz);
            if (_config == null)
            {
                Console.WriteLine("O arquivo config.json não existe!");
                Console.WriteLine("Coloque as informações necessarias no arquivo gerado!");
                Console.WriteLine("Aperte qualquer botão para sair...");
                Console.ReadKey();
                Environment.Exit(0);
            }
            CoreBot.DiscordBotsApiKey = _config.DiscordBotsKey;

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
#if DEBUG
                LogLevel = LogLevel.Debug,
#else
                LogLevel = LogLevel.Info,
#endif
                UseInternalLogHandler = true,
            };
            _cliente = new ModuloCliente(cfg);

            _todosOsComandos = new ModuloComando(new CommandsNextConfiguration
            {

#if DEBUG
                StringPrefix = _config.PrefixTeste,
#else
                StringPrefix = _config.Prefix,
#endif
                EnableDms = false,
                CaseSensitive = false,
                EnableDefaultHelp = false,
                EnableMentionPrefix = true,
                IgnoreExtraArguments = true,
            }, ModuloCliente.Client);
            new ModuloBanco();
            Console.WriteLine("Modulo banco concluido.");
            new Areas();
            Console.WriteLine("Áreas concluido.");
            new Racas();
            Console.WriteLine("Raças concluido.");
            new Missoes();
            Console.WriteLine("Missões concluido.");
            new Itens();
            Console.WriteLine("Itens de consulta adicionados");
            await ModuloCliente.Client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
