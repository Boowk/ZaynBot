using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using ZaynBot.Comandos;
using ZaynBot.Eventos;

namespace ZaynBot
{
    public class Program
    {
        public static ConfigBot Config { get; private set; }

        private DiscordClient _client;

        static void Main() => new Program().RodarOBotAsync().GetAwaiter().GetResult();

        public async Task RodarOBotAsync()
        {
            Config = ConfigBot.LoadFromFile(Extensoes.EntrarPasta("") + "config.json");
            if (Config == null)
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
                Token = Config.TokenTeste,
#else
                Token = Config.Token,
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
            _client = new DiscordClient(cfg);
            _client.Ready += Client_Ready;
            _client.GuildAvailable += Guilda.Disponivel;
            _client.ClientErrored += Client_ClientErrored;
            _client.MessageCreated += Mensagem.Criada;
            _client.MessageUpdated += Mensagem.Editada;
            _client.MessageDeleted += Mensagem.Apagada;
            _client.VoiceStateUpdated += Voice.VoiceJoin;

            #region Prefixo
            string[] prefix = new string[1];
#if DEBUG
            prefix[0] = Config.PrefixTeste;
#else
            prefix[0] = Config.Prefix;
#endif
            #endregion

            CommandsNextConfiguration cnc = new CommandsNextConfiguration
            {
                StringPrefixes = prefix,
                EnableDms = false,
                CaseSensitive = false,
                EnableDefaultHelp = false,
                EnableMentionPrefix = true,
                IgnoreExtraArguments = true,
            };
            CommandsNextExtension cnt = _client.UseCommandsNext(cnc);
            cnt.CommandExecuted += Cnt_CommandExecuted;
            cnt.CommandErrored += Cnt_CommandErrored;
            cnt.SetHelpFormatter<IAjuda>();
            cnt.RegisterCommands<Basicos>();
            cnt.RegisterCommands<Economia>();




            new ModuloBanco();
            await _client.ConnectAsync();
            await Task.Delay(-1);
        }

        private async Task Cnt_CommandErrored(CommandErrorEventArgs e)
        {
            CommandContext ctx = e.Context;
            switch (e.Exception)
            {
                case ChecksFailedException ex:
                    if (!(ex.FailedChecks.FirstOrDefault(x => x is CooldownAttribute) is CooldownAttribute my))
                        break;
                    else
                    {
                        TimeSpan t = TimeSpan.FromSeconds(my.GetRemainingCooldown(ctx).TotalSeconds);
                        if (t.Hours >= 1)
                            await ctx.RespondAsync($"Aguarde {t.Hours} horas e {t.Minutes} minutos para usar este comando! {ctx.Member.Mention}.");
                        else if (t.Minutes >= 1)
                            await ctx.RespondAsync($"Aguarde {t.Minutes} minutos e {t.Seconds} segundos para usar este comando! {ctx.Member.Mention}.");
                        else
                            await ctx.RespondAsync($"Aguarde {t.Seconds} segundos para usar este comando! {ctx.Member.Mention}.");
                    }
                    break;
                case UnauthorizedException ux:
                    break;
                case CommandNotFoundException cf:
                    if (e.Command != null)
                        if (e.Command.Name == "ajuda")
                        {
                            DiscordEmoji x = DiscordEmoji.FromName(ctx.Client, ":no_entry_sign:");
                            await ctx.RespondAsync($"{x} | {ctx.User.Mention} o comando {e.Context.RawArgumentString} não existe.*");
                        }
                    break;
                default:
                    e.Context.Client.DebugLogger.LogMessage(LogLevel.Debug, "Erro", $"[{e.Context.User.Username.RemoverAcentos()}({e.Context.User.Id})] tentou usar '{e.Command?.QualifiedName ?? "<comando desconhecido>"}' mas deu erro: {e.Exception.ToString()}\nstack:{e.Exception.StackTrace}\ninner:{e.Exception?.InnerException}.", DateTime.Now);
                    break;
            }
        }

        private Task Cnt_CommandExecuted(CommandExecutionEventArgs e)
        {
            e.Context.Client.DebugLogger.LogMessage(LogLevel.Info, "ZAYN", $"({e.Context.Guild.Id}) {e.Context.Guild.Name.RemoverAcentos()} ({e.Context.User.Id}) {e.Context.User.Username.RemoverAcentos()} executou '{e.Command.QualifiedName}'", DateTime.Now);
            return Task.CompletedTask;
        }

        private Task Client_ClientErrored(ClientErrorEventArgs e)
        {
           // e.Client.DebugLogger.LogMessage(LogLevel.Error, "ZAYN", $"Um erro aconteceu: {e.Exception.GetType()}: {e.Exception.Message}", DateTime.Now);
            return Task.CompletedTask;
        }

        private Task Client_Ready(ReadyEventArgs e)
        {
            e.Client.DebugLogger.LogMessage(LogLevel.Info, "ZAYN", "Cliente está pronto!", DateTime.Now);
            _client.UpdateStatusAsync(new DiscordActivity($"..ajuda", ActivityType.ListeningTo), UserStatus.DoNotDisturb);
            return Task.CompletedTask;
        }
    }
}
