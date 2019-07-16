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

        private StringBuilder MessageBuilder { get; }

        public AjudaFormatador()
        {
            this.MessageBuilder = new StringBuilder();
        }


        public IHelpFormatter WithCommandName(string nome)
        {
            _nome = nome.PrimeiraLetraMaiuscula();
            return this;
        }

        public IHelpFormatter WithDescription(string descricao)
        {
            _descricao = descricao;
            return this;
        }

        public IHelpFormatter WithGroupExecutable()
         => this;


        public IHelpFormatter WithAliases(IEnumerable<string> aliases)
        {
            _abreviacoes = string.Join(", ", aliases.Select(Formatter.InlineCode));
            return this;
        }

        public IHelpFormatter WithArguments(IEnumerable<CommandArgument> arguments)
        => this;

        public IHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {
            if (_nome == null)
            {
                this.MessageBuilder.Append("**Comandos:** ");
            }
            MessageBuilder.AppendLine(string.Join(", ", subcommands.Select(xc => xc.Name)));
            return this;
        }

        public CommandHelpMessage Build()
        {
            if (_nome == null)
            {
                StringBuilder str = new StringBuilder();
                str.Append("```css\nLista de comandos```\n" +
                    "Use `z!ajuda [comando]` para obter mais ajuda sobre o comando específico, por exemplo: `z!ajuda ajuda`\n\n");
                str.Append(MessageBuilder.ToString());
                str.Append("```csharp\n# Não inclua os colchetes do exemplo quando utilizar o comando!```");

                return new CommandHelpMessage(str.ToString());
            }

            EmbedBuilder = new DiscordEmbedBuilder();
            EmbedBuilder.WithColor(DiscordColor.Cyan);
            EmbedBuilder.WithTitle($"**{_nome}**");
            _descricao = string.IsNullOrEmpty(_descricao) ? "Sem descrição" : _descricao;
            EmbedBuilder.WithDescription(_descricao);
            if (_abreviacoes != null)
                EmbedBuilder.AddField("**Abreviações**", _abreviacoes);
            if (MessageBuilder.ToString() != "")
                EmbedBuilder.AddField("**Subcomandos**", MessageBuilder.ToString());

            return new CommandHelpMessage(embed: EmbedBuilder.Build());
        }
    }
}
