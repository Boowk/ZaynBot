using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    [Group("info")]
    [Description("Comandos informativos.")]
    public class GrupoInfo
    {
        [Command("ping")]
        [Description("Exibe o tempo de resposta do bot ao servidor do discord.")]
        public async Task ComandoInfoPing(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var emoji = DiscordEmoji.FromName(ctx.Client, ":ping_pong:");
            await ctx.RespondAsync($"{emoji} Pong! Ping: {ctx.Client.Ping}ms");
        }

        [Group("ranque")]
        [Description("Comandos comparativos.")]
        public class GrupoRanque
        {
            [Command("nivel")]
            [Description("Exibe os maiores niveis do server ou global.")]
            [Cooldown(1, 10, CooldownBucketType.User)]
            public async Task ComandoInfoRanqueNivel(CommandContext ctx, [Description("Servidor ou Global")] string server = "servidor")
            {
                await ctx.TriggerTypingAsync();
                if (server == "servidor")
                {
                    //Para fazer: Limpar
                    IMongoClient client = new MongoClient("mongodb://localhost");
                    IMongoDatabase database = client.GetDatabase("zaynbot");
                    IMongoCollection<RPGUsuario> col = database.GetCollection<RPGUsuario>("usuarios");
                    StringBuilder gg = new StringBuilder();

                    List<ulong> ids = new List<ulong>();
                    List<int> niveis = new List<int>();
                    List<DiscordUser> usuarios = new List<DiscordUser>();

                    await col.Find(FilterDefinition<RPGUsuario>.Empty).Limit(10).Sort("{Nivel: -1}")
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
}
