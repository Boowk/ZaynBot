﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoLoja : BaseCommandModule
    {
        [Command("loja")]
        [Description("Permite ver os itens que estão sendo vendido na região atual.")]
        [ComoUsar("loja")]
        [Cooldown(1, 1, CooldownBucketType.User)]
        public async Task ComandoLojaAb(CommandContext ctx, int pagina = 1)
        {
            await ctx.TriggerTypingAsync();

            if (pagina < 1)
            {
                await ctx.RespondAsync($"{ctx.User.Mention} o número da página não pode ser menor que 1!".Bold());
                return;
            }
            int pagianTamanho = 10;
            int paginaAtual = pagina - 1;
            var fd = Builders<RPGItem>.Filter;
            var filter = fd.Eq(x => x.PodeComprar, true);

            FindOptions<RPGItem> options = new FindOptions<RPGItem>
            {
                Skip = paginaAtual * pagianTamanho,
                Limit = pagianTamanho,
                NoCursorTimeout = false
            };

            StringBuilder str = new StringBuilder();
            using (IAsyncCursor<RPGItem> cursor = await ModuloBanco.ItemColecao.FindAsync(filter, options))
            {
                while (await cursor.MoveNextAsync())
                {
                    IEnumerable<RPGItem> itens = cursor.Current;
                    foreach (RPGItem item in itens)
                    {
                        str.AppendLine($"[{item.Nome.Bold()}] - {item.PrecoCompra} Zeoin");
                    }
                }
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Loja", ctx);
            embed.WithDescription(str.ToString());
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}