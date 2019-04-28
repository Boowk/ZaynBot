using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using System;
using System.IO;
using System.Threading.Tasks;
using ZaynBot.Comandos;
using ZaynBot.Entidades;
using ZaynBot.Funções;

namespace ZaynBot
{
    class Program
    {
        public DiscordClient Client { get; set; }
        private ModuloComandos _todosOsComandos;
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
            Client = new DiscordClient(cfg);
            Client.Ready += Client_Ready;
            Client.GuildAvailable += Client_GuildAvailable;
            Client.ClientErrored += Client_ClientError;
            Client.MessageCreated += Client_MessageCreated;
            Client.GuildMemberAdded += Client_GuildMemberAdded;
            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(1)
            });

            CommandsNextConfiguration ccfg = new CommandsNextConfiguration
            {

#if DEBUG
                StringPrefix = _config.PrefixTeste,
#else
                StringPrefix = _config.Prefix,
#endif
                CaseSensitive = false,

                EnableDms = true,
                EnableDefaultHelp = false,
                EnableMentionPrefix = true,
                IgnoreExtraArguments = true,
            };
            _todosOsComandos = new ModuloComandos(ccfg, Client);
            

            // Console.WriteLine($"{BancoDeDados.CarregarTodos()} usuarios salvos na HD.");
            //DoWorkAsyncInfiniteLoop();

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private async Task Client_GuildMemberAdded(GuildMemberAddEventArgs e)
        {
            await UsuarioNovoEntrouGuilda.EventoBemVindoAsync(e);
        }

        //private async Task DoWorkAsyncInfiniteLoop()
        //{
        //    while (true)
        //    {
        //        FazerSemParar();
        //        await Task.Delay(TimeSpan.FromMinutes(5));

        //    }
        //}

        //private async Task FazerSemParar()
        //{

        //    Console.WriteLine($"Cache com {BancoDeDados.CacheUsuarios.Count} usuarios!");
        //    Console.WriteLine($"Cache limpo: {BancoDeDados.SalvarUsuarios()} usuarios salvos!");

        //DiscordGuild f = await Client.GetGuildAsync(508615273515843584);
        //DiscordChannel g = f.GetChannel(551469878268395530);
        //Random r = new Random();
        //string imagem = "";
        //string resposta = "";
        //switch (r.Next(0, 5))
        //{
        //    case 0:
        //        imagem = "https://cdn.discordapp.com/attachments/519347764547813388/552232509996531713/1546396750586.png";
        //        resposta = "Lucas";
        //        break;
        //    case 1:
        //        imagem = "https://cdn.discordapp.com/attachments/519347764547813388/552232585145745409/leogamer4.PNG";
        //        resposta = "Leo gamer";
        //        break;
        //    case 2:
        //        imagem = "https://cdn.discordapp.com/attachments/519347764547813388/552232619899748393/z.jpg";
        //        resposta = "Fusion";
        //        break;
        //    case 3:
        //        imagem = "https://cdn.discordapp.com/attachments/551469878268395530/552248135326367774/Screenshot_15.png";
        //        resposta = "José italo";
        //        break;
        //    case 4:
        //        imagem = "https://cdn.discordapp.com/attachments/552242642373836811/552242661877219348/JPEG_20181117_000919.jpg";
        //        resposta = "Fusion 2";
        //        break;
        //}
        //DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
        //embed.WithImageUrl(imagem);
        //embed.WithTitle($"**{resposta} apareceu!**");
        //embed.WithColor(DiscordColor.Red);
        //await g.SendMessageAsync(embed: embed.Build());
        //var i = Client.GetInteractivityModule();
        //var m = await i.WaitForMessageAsync(
        //    x => x.Channel == g
        //    && x.Content == "bang!", TimeSpan.FromSeconds(20));

        //if (m == null)
        //{
        //    await g.SendMessageAsync($"**O {resposta} fugiu!**");
        //}
        //else
        //if (m.Message.Content == "bang!")
        //{
        //    DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
        //    emb.WithImageUrl("https://media.tenor.com/images/a6405b266a920c98b482b17defa3c44f/tenor.gif");
        //    await g.SendMessageAsync($"**{m.User.Mention} matou o {resposta}!**", embed: emb.Build());
        //}

        //    await Task.CompletedTask;
        //}

        private async Task Client_MessageCreated(MessageCreateEventArgs e)
        {
            if (e.Message.Author.IsBot) return;

            //Console.WriteLine($"[{DateTime.Now}] [{e.Guild.Name}] [{e.Message.Author}] {e.Message.Content}");
            await Task.CompletedTask;
        }

        private Task Client_Ready(ReadyEventArgs e)
        {
            e.Client.DebugLogger.LogMessage(LogLevel.Info, "RPG", "Cliente está pronto para processar eventos.", DateTime.Now);
            Client.UpdateStatusAsync(new DSharpPlus.Entities.DiscordGame("Funcionando em uma VPS."));
            return Task.CompletedTask;

        }

        private Task Client_GuildAvailable(GuildCreateEventArgs e)
        {
            e.Client.DebugLogger.LogMessage(LogLevel.Info, "RPG", $"Guilda disponível: {e.Guild.Name}", DateTime.Now);
            return Task.CompletedTask;
        }

        private Task Client_ClientError(ClientErrorEventArgs e)
        {
            e.Client.DebugLogger.LogMessage(LogLevel.Error, "ERRO", $"Um erro aconteceu: {e.Exception.GetType()}: {e.Exception.Message}", DateTime.Now);

            return Task.CompletedTask;
        }
    }
}
