using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Entidades;

namespace ZaynBot.Comandos.Informações
{
    public static class TopRanque
    {
        public static async Task TopRanqueAb(CommandContext ctx, string server)
        {
            if (server == "servidor")
            {
                IMongoClient client = new MongoClient("mongodb://localhost");
                IMongoDatabase database = client.GetDatabase("zaynbot");
                IMongoCollection<Usuario> col = database.GetCollection<Usuario>("usuarios");
                StringBuilder gg = new StringBuilder();

                List<ulong> ids = new List<ulong>();
                List<int> niveis = new List<int>();
                List<DiscordUser> usuarios = new List<DiscordUser>();

                await col.Find(FilterDefinition<Usuario>.Empty).Limit(10).Sort("{Nivel: -1}")
                    .ForEachAsync(x =>
                    {
                        ids.Add(x.Id);
                        niveis.Add(x.Nivel);
                    }).ConfigureAwait(false);
                int index = 0;
                foreach (var item in ids)
                {
                    DiscordUser u = await ctx.Client.GetUserAsync(item);
                    gg.Append($"{u.Username} - Nivel: {niveis[index]}\n");
                    index++;
                }
                await ctx.RespondAsync(gg.ToString());
            }
        }
    }
}
