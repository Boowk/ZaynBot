using DSharpPlus;
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
        private DiscordEmbedBuilder eb;
        private StringBuilder sr;
        private StringBuilder srExemplos;
        private StringBuilder srUsos;

        public IAjudaComando(CommandContext ctx) : base(ctx)
        {
            sr = new StringBuilder();
            srExemplos = new StringBuilder();
            srUsos = new StringBuilder();
            eb = new DiscordEmbedBuilder();
        }


        public override BaseHelpFormatter WithCommand(Command command)
        {
            sr.Append($"**{command.Name.PrimeiraLetraMaiuscula()}**\n");
            sr.Append($"*{command.Description}*\n");
            foreach (var item in command.ExecutionChecks)
            {
                switch (item)
                {
                    case ExemploAtributo e:
                        srExemplos.Append($"`z!{e.Exemplo}`\n");
                        break;
                    case UsoAtributo u:
                        srUsos.Append($"`z!{u.Uso}`\n");
                        break;
                }
            }
            if (string.IsNullOrEmpty(srUsos.ToString()))
                srUsos.Append($"`z!{command.Name}`");
            if (string.IsNullOrEmpty(srExemplos.ToString()))
                srExemplos.Append($"`Não precisa de exemplo.`");
            sr.Append($"**Como usar:**\n {srUsos.ToString()}");
            sr.Append($"**Exemplos:**\n {srExemplos.ToString()}");
            return this;
        }

        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {
            StringBuilder str = new StringBuilder();
            str.Append("**Comandos:** ");
            str.AppendLine(string.Join(", ", subcommands.Select(xc => xc.Name)));
            sr.Append("```css\nLista de comandos```\n" +
                "Use `z!ajuda [comando]` para obter mais ajuda sobre o comando específico, por exemplo: `z!ajuda ajuda`\n\n");
            sr.Append(str.ToString());
            sr.Append("```csharp\n# Não inclua os colchetes do exemplo quando utilizar o comando!```");
            return this;
        }

        public override CommandHelpMessage Build()
        {
            eb.WithColor(DiscordColor.Cyan);
            eb.WithDescription(sr.ToString());
            return new CommandHelpMessage(embed: eb.Build());
        }
    }
}
