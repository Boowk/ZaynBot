using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.RPG
{
    public class ListaEmojisSelecao
    {
        public int SelecaoAtual { get; private set; } = 0;
        readonly CommandContext _ctx;
        public Dictionary<int, string> Emojis { get; private set; }

        public ListaEmojisSelecao(CommandContext ctx) : this()
        {
            _ctx = ctx;
        }

        public ListaEmojisSelecao()
        {
            Emojis = new Dictionary<int, string>()
            {
                {1, ":one:"},
                {2, ":two:" },
                {3, ":three:" },
                {4, ":four:" },
                {5, ":five:" },
                {6, ":six:" },
                {7, ":seven:" },
                {8, ":eight:" },
                {9, ":nine:" },
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
