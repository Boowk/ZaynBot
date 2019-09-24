using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Threading.Tasks;

namespace ZaynBot.Core.Atributos
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ExemploAtributo : CheckBaseAttribute
    {
        public string Exemplo { get; }

        public ExemploAtributo(string exemplo) => Exemplo = exemplo;

        public override Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help) => Task.FromResult(true);
    }
}
