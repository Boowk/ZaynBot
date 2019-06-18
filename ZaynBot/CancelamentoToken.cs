using DSharpPlus.CommandsNext;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ZaynBot
{
    public static class CancelamentoToken
    {
        public static ConcurrentDictionary<ulong, CancellationTokenSource> Token { get; private set; } = new ConcurrentDictionary<ulong, CancellationTokenSource>();

        public static void AdicionarOuAtualizar(CommandContext ctx, CancellationTokenSource cts)
        {
            Token.AddOrUpdate(ctx.User.Id, cts, (key, oldValue) => cts);
        }

        public static void CancelarToken(CommandContext ctx) => CancelarToken(ctx.User.Id);

        public static void CancelarToken(ulong id)
        {
            Token.TryGetValue(id, out CancellationTokenSource token);
            if (token != null)
            {
                token.Cancel();
            }
        }
    }
}
