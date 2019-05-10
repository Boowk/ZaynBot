﻿using DSharpPlus;
using DSharpPlus.CommandsNext;
using System;
using System.IO;
using System.Threading.Tasks;
using ZaynBot._Gameplay.Mundos.Anker;
using ZaynBot.Comandos;
using ZaynBot.Entidades;

namespace ZaynBot
{
    public class Program
    {
        private ModuloComando _todosOsComandos;
        private ModuloCliente _cliente;
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
            Bot.DiscordBotsApiKey = _config.DiscordBotsKey;

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

            Anker mundoAnker = new Anker();
            await Banco.AtualizarBancoAllAsync();
            await ModuloCliente.Client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
