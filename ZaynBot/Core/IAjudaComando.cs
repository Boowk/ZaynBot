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
        public IAjudaComando(CommandContext ctx) : base(ctx)
        {

        }
        //private DiscordEmbedBuilder _embedBuilder;
        //private string _nome = null;
        //private string _uso = null;
        //private string _descricao = null;
        //private string _abreviacoes = null;

        //private StringBuilder ComandosBuilder { get; }
        //private StringBuilder ExemplosBuilder { get; }

        //public IAjudaComando()
        //{
        //    ComandosBuilder = new StringBuilder();
        //    ExemplosBuilder = new StringBuilder();
        //}


        //public IHelpFormatter WithCommandName(string nome)
        //{

        //    _nome = nome.PrimeiraLetraMaiuscula();
        //    ModuloComando.Comandos.RegisteredCommands.TryGetValue(nome, out Command comando);
        //    var atributos = comando.ExecutionChecks;
        //    foreach (var item in atributos)
        //    {
        //        switch (item)
        //        {
        //            case ExemploAtributo e:
        //                ExemplosBuilder.Append($"`z!{e.Exemplo}`\n");
        //                break;
        //            case UsoAtributo u:
        //                _uso = u.Uso;
        //                break;
        //        }
        //    }
        //    return this;
        //}

        //public IHelpFormatter WithDescription(string descricao)
        //{
        //    _descricao = descricao;
        //    return this;
        //}

        //public IHelpFormatter WithGroupExecutable()
        // => this;


        //public IHelpFormatter WithAliases(IEnumerable<string> aliases)
        //{
        //    _abreviacoes = string.Join(", ", aliases.Select(Formatter.InlineCode));
        //    return this;
        //}

        //public IHelpFormatter WithArguments(IEnumerable<CommandArgument> arguments)
        //=> this;

        //public IHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        //{
        //    if (_nome == null)
        //        this.ComandosBuilder.Append("**Comandos:** ");
        //    ComandosBuilder.AppendLine(string.Join(", ", subcommands.Select(xc => xc.Name)));
        //    return this;
        //}

        //public CommandHelpMessage Build()
        //{
        //    if (_nome == null)
        //    {
        //        StringBuilder str = new StringBuilder();
        //        str.Append("```css\nLista de comandos```\n" +
        //            "Use `z!ajuda [comando]` para obter mais ajuda sobre o comando específico, por exemplo: `z!ajuda ajuda`\n\n");
        //        str.Append(ComandosBuilder.ToString());
        //        str.Append("```csharp\n# Não inclua os colchetes do exemplo quando utilizar o comando!```");

        //        return new CommandHelpMessage(str.ToString());
        //    }

        //    _embedBuilder = new DiscordEmbedBuilder();
        //    _embedBuilder.WithColor(DiscordColor.Cyan);
        //    _embedBuilder.WithTitle($"**{_nome}**");
        //    _descricao = $"{_descricao}\n";
        //    _embedBuilder.WithDescription(_descricao);
        //    if (_uso != null)
        //        _embedBuilder.AddField("**Uso**", $"z!{_uso}");
        //    if (!string.IsNullOrWhiteSpace(ExemplosBuilder.ToString()))
        //        _embedBuilder.AddField($"**Exemplos**", ExemplosBuilder.ToString());
        //    if (_abreviacoes != null)
        //        _embedBuilder.AddField("**Abreviações**", _abreviacoes);
        //    if (ComandosBuilder.ToString() != "")
        //        _embedBuilder.AddField("**Subcomandos**", ComandosBuilder.ToString());

        //    return new CommandHelpMessage(embed: _embedBuilder.Build());
        //}


        public override BaseHelpFormatter WithCommand(Command command)
        {
            throw new NotImplementedException();
        }

        public override BaseHelpFormatter WithSubcommands(IEnumerable<Command> subcommands)
        {
            throw new NotImplementedException();
        }

        public override CommandHelpMessage Build()
        {
            throw new NotImplementedException();
        }
    }
}
