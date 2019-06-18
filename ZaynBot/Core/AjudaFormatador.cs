using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZaynBot.Core
{
    public class AjudaFormatador : IHelpFormatter
    {
        private DiscordEmbedBuilder EmbedBuilder;

        private string _nome = null;
        private string _descricao = null;
        private string _abreviacoes = null;
        private string _subcomandos = null;

        public IHelpFormatter WithCommandName(string nome)
        {
            _nome = nome;
            return this;
        }

        public IHelpFormatter WithDescription(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public IHelpFormatter WithGroupExecutable()
        {
            return this;
        }

        public IHelpFormatter WithAliases(IEnumerable<string> aliases)
        {
            _abreviacoes = string.Join(", ", aliases.Select(Formatter.InlineCode));
            return this;
        }

        public IHelpFormatter WithArguments(IEnumerable<CommandArgument> arguments)
        {
            return this;
        }

        public IHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {
            _subcomandos = string.Join(", ", subcommands.Select(x => Formatter.InlineCode(x.Name)));
            return this;
        }

        public CommandHelpMessage Build()
        {
            EmbedBuilder = new DiscordEmbedBuilder();
            EmbedBuilder.WithTimestamp(DateTime.Now);
            EmbedBuilder.WithColor(DiscordColor.Cyan);
            EmbedBuilder.WithTitle($"**{_nome}**");
            _descricao = string.IsNullOrEmpty(_descricao) ? "Sem descrição" : _descricao;
            EmbedBuilder.WithDescription(_descricao);
            if (_abreviacoes != null)
                EmbedBuilder.AddField("**Abreviações**", _abreviacoes);
            if (_subcomandos != null)
                EmbedBuilder.AddField("**Subcomandos**", _subcomandos);
            return new CommandHelpMessage(embed: EmbedBuilder.Build());
        }
    }
}
