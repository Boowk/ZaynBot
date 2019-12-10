﻿using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using ZaynBot.Core.Atributos;
using ZaynBot.Core.Comandos;

namespace ZaynBot.Core
{
    public class IAjudaComando : BaseHelpFormatter
    {
        private DiscordEmbedBuilder _embed;
        private StringBuilder _srExemplos;
        private StringBuilder _srUsos;
        private StringBuilder _srSubCommands;
        private bool _comandoAjuda;
        public IAjudaComando(CommandContext ctx) : base(ctx)
        {
            if (ctx.RawArguments.Count == 0)
                _comandoAjuda = true;
            else
                _comandoAjuda = false;

            if (!_comandoAjuda)
            {
                _embed = new DiscordEmbedBuilder();
                _srExemplos = new StringBuilder();
                _srUsos = new StringBuilder();
            }
        }


        public override BaseHelpFormatter WithCommand(Command command)
        {
            if (!_comandoAjuda)
            {
                foreach (var item in command.ExecutionChecks)
                {
                    switch (item)
                    {
                        case ExemploAtributo e:
                            _srExemplos.Append($"`z!{e.Exemplo}`\n");
                            break;
                        case UsoAtributo u:
                            _srUsos.Append($"`z!{u.Uso}`\n");
                            break;
                    }
                }

                _embed.WithTitle($"**{command.Name.PrimeiraLetraMaiuscula()}**");
                _embed.WithDescription(command.Description);
                if (_srUsos.Length != 0)
                    _embed.AddField("**Como usar**", _srUsos.ToString());
                if (_srExemplos.Length != 0)
                    _embed.AddField("**Exemplos**", _srExemplos.ToString());
            }
            return this;
        }

        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {
            if (!_comandoAjuda)
            {
                _srSubCommands = new StringBuilder();
                foreach (var item in subcommands)
                    _srSubCommands.Append($"`{item.Name}` , ");
                _embed.AddField("**Comandos**", _srSubCommands.ToString());
            }
            return this;
        }

        public override CommandHelpMessage Build()
        {
            if (!_comandoAjuda)
            {
                _embed.WithColor(DiscordColor.CornflowerBlue);

                return new CommandHelpMessage(embed: _embed.Build());
            }
            return new CommandHelpMessage(content: MensagemAjuda());
        }

        public string MensagemAjuda()
        {
            return "```css\nLista de comandos``` \n" +
            "Use `z!ajuda [comando]` para obter mais ajuda sobre o comando específico, por exemplo: `z!ajuda ajuda` \n\n" +
            "**1. Core -** `ajuda` , `convite` , `info` , `prefix` , `votar` , `usuario`\n" +
            "**2. RPG -** `status` , `criar-personagem` , `regiao` , `explorar` , `batalha` \n" +
            "**3. Grupo -** `criar` , `convidar` , `sair` , `remover`\n" +
            "```csharp\n# Não inclua os colchetes do exemplo quando utilizar o comando!```";
        }
    }
}