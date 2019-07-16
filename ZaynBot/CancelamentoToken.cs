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
        private static ConcurrentDictionary<ulong, CancellationTokenSource> Token = new ConcurrentDictionary<ulong, CancellationTokenSource>();

        public static void AdicionarOuAtualizar(ulong id, CancellationTokenSource cts)
         => Token.AddOrUpdate(id, cts, (key, oldValue) => cts);

        public static void CancelarToken(ulong id)
        {
            Token.TryRemove(id, out CancellationTokenSource token);
            if (token != null)
                token.Cancel();
        }
    }
}
