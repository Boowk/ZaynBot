using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Threading.Tasks;

namespace ZaynBot.Atributos
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class Exemplo : CheckBaseAttribute
    {
        public string ExemploMsg { get; }

        public Exemplo(string exemplo) => ExemploMsg = exemplo;

        public override Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help) => Task.FromResult(true);
    }
}
