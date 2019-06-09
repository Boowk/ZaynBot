using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.RPG
{
    public class ListaEmojis
    {
        public int SelecaoAtual { get; private set; } = 0;
        readonly CommandContext _ctx;
        public Dictionary<int, string> Emojis { get; private set; }

        public ListaEmojis(CommandContext ctx) : this()
        {
            _ctx = ctx;
        }

        public ListaEmojis()
        {
            Emojis = new Dictionary<int, string>()
            {
                {1, ":one:"},
                {2, ":two:" }
            };
        }

        public DiscordEmoji ProxEmoji()
        {
            SelecaoAtual++;
            DiscordEmoji emoji = DiscordEmoji.FromName(_ctx.Client, Emojis[SelecaoAtual]);
            return emoji;
        }

        public void ResetSelecao()
        {
            SelecaoAtual = 0;
        }
    }
}
