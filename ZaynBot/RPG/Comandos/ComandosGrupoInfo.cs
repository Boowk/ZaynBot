using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    //[Group("info")]
    //[Description("Comandos informativos.")]
    public class ComandosGrupoInfo : BaseCommandModule
    {
        //[Command("ping")]
        //[Description("Exibe o tempo de resposta do bot ao servidor do discord.")]
        //public async Task ComandoInfoPing(CommandContext ctx)
        //{
        //    await ctx.TriggerTypingAsync();
        //    var emoji = DiscordEmoji.FromName(ctx.Client, ":ping_pong:");
        //    await ctx.RespondAsync($"{emoji} Pong! Ping: {ctx.Client.Ping}ms");
        //}

        //[Group("ranque")]
        //[Description("Comandos comparativos.")]
        //public class GrupoRanque
        //{
        //    [Command("nivel")]
        //    [Description("Exibe os maiores niveis de conta global.")]
        //    [Cooldown(1, 10, CooldownBucketType.User)]
        //    public async Task ComandoInfoRanqueNivel(CommandContext ctx)
        //    {
        //        await ctx.TriggerTypingAsync();
        //        //Para fazer: Limpar
        //        StringBuilder texto = new StringBuilder();
        //        List<ListaRanque> lista = new List<ListaRanque>();

        //        await ModuloBanco.UsuarioColecao.Find(FilterDefinition<RPGUsuario>.Empty).Limit(10).Sort("{Nivel: -1}")
        //            .ForEachAsync(x =>
        //            {
        //                lista.Add(new ListaRanque
        //                {
        //                    Id = x.Id,
        //                    Nivel = x.Nivel
        //                });
        //            }).ConfigureAwait(false);
        //        int index = 1;
        //        DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
        //        embed.WithTitle("Ranque de nível global");
        //        embed.WithTimestamp(DateTime.Now);

        //        foreach (var item in lista)
        //        {
        //            try
        //            {
        //                DiscordMember membro = await ctx.Guild.GetMemberAsync(item.Id);
        //                texto.Append($"{index} - {membro.Mention}- Nivel: {item.Nivel}\n");
        //            }
        //            catch
        //            {
        //                DiscordUser usuario = await ctx.Client.GetUserAsync(item.Id);
        //                texto.Append($"{index} - {usuario.Username}#{usuario.Discriminator} - Nivel: {item.Nivel}\n");
        //            }
        //            index++;
        //        }
        //        embed.WithDescription(texto.ToString());
        //        embed.WithColor(DiscordColor.Chartreuse);
        //        await ctx.RespondAsync(embed: embed.Build());
        //    }

        //    private class ListaRanque
        //    {
        //        public ulong Id { get; set; }
        //        public int Nivel { get; set; }
        //    }
        //}
    }
}
