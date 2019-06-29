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
        private static ConcurrentDictionary<ulong, CancellationTokenSource> _token = new ConcurrentDictionary<ulong, CancellationTokenSource>();

        public static void AdicionarOuAtualizar(ulong id, CancellationTokenSource cts)
         => _token.AddOrUpdate(id, cts, (key, oldValue) => cts);

        public static void CancelarToken(ulong id)
        {
            _token.TryRemove(id, out CancellationTokenSource token);
            if (token != null)
                token.Cancel();
        }
    }
}
