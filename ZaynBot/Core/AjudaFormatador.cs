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

        private string nome = null;

        private string descricao = null;

        public AjudaFormatador()
        {
            EmbedBuilder = new DiscordEmbedBuilder()
                .WithTitle("**Ajuda**")
                .WithColor(DiscordColor.Blue);
        }
        public IHelpFormatter WithCommandName(string nome)
        {
            this.nome = nome;
            return this;
        }

        public IHelpFormatter WithDescription(string descricao)
        {
            this.descricao = descricao;
            return this;
        }

        public IHelpFormatter WithGroupExecutable()
        {
            return this;
        }

        public IHelpFormatter WithAliases(IEnumerable<string> aliases)
        {
            EmbedBuilder.AddField("**Abreviações**", string.Join(", ", aliases.Select(Formatter.InlineCode)), false);
            return this;
        }

        public IHelpFormatter WithArguments(IEnumerable<CommandArgument> arguments)
        {
            Dictionary<Type, string> UserFriendlyTypeNames = new Dictionary<Type, string>()
            {
                [typeof(string)] = "Texto",
                [typeof(bool)] = "Sim ou não",
                [typeof(sbyte)] = "signed byte",
                [typeof(byte)] = "byte",
                [typeof(short)] = "short",
                [typeof(ushort)] = "unsigned short",
                [typeof(int)] = "Tnteiro",
                [typeof(uint)] = "Valor real inteiro",
                [typeof(long)] = "Valor inteiro",
                [typeof(ulong)] = "Valor real inteiro",
                [typeof(float)] = "Flutuante",
                [typeof(double)] = "Real",
                [typeof(decimal)] = "Decimal",
                [typeof(DateTime)] = "Data e tempo",
                [typeof(DateTimeOffset)] = "Data e tempo",
                [typeof(TimeSpan)] = "Tempo",
                [typeof(Uri)] = "URL",
                [typeof(DiscordUser)] = "Usuario",
                [typeof(DiscordMember)] = "Membro",
                [typeof(DiscordRole)] = "Cargo",
                [typeof(DiscordChannel)] = "Canal",
                [typeof(DiscordGuild)] = "Servidor",
                [typeof(DiscordMessage)] = "Mensagem",
                [typeof(DiscordEmoji)] = "Emoji",
                [typeof(DiscordColor)] = "Cor"
            };


            var sb = new StringBuilder();

            foreach (var ovl in arguments.OrderByDescending(x => x.Name))
            {
                sb.Append('`').Append(ovl.Name).Append(" (").Append(UserFriendlyTypeNames[ovl.Type]).Append(")`: ").Append(ovl.Description ?? "Sem descrição.").Append('\n');
            }

            EmbedBuilder.AddField("**Argumentos**", sb.ToString().Trim(), false);
            return this;
        }

        public IHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {
            EmbedBuilder.AddField("**Subcomandos**", string.Join(", ", subcommands.Select(x => Formatter.InlineCode(x.Name))), false);
            return this;
        }

        public CommandHelpMessage Build()
        {
            nome = string.IsNullOrEmpty(nome) ? "Listando todos os comandos e grupos. Especifique um comando para ver mais informações." : nome;
            string doisPontos = ":";
            string links = null;
            if (nome == "Listando todos os comandos e grupos. Especifique um comando para ver mais informações.")
            {
                doisPontos = null;

            }
            else
            {
                descricao = string.IsNullOrEmpty(descricao) ? "Sem descrição" : descricao;
            }

            EmbedBuilder.WithDescription($"`{nome}`{doisPontos} {descricao}");
            return new CommandHelpMessage(links, embed: EmbedBuilder.Build());
        }
    }
}
