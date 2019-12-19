using DSharpPlus.CommandsNext;
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
    class TopComando : BaseCommandModule
    {
        [Command("top")]
        [Description("Exibe os 10 personagens mais evoluidos")]
        [ComoUsar("top")]
        [Cooldown(1, 60, CooldownBucketType.User)]
        public async Task TopComandoAb(CommandContext ctx)
        {
            var f = await ModuloBanco.UsuarioColecao.Find(FilterDefinition<RPGUsuario>.Empty).Limit(10)
                .SortByDescending(x => x.Personagem.NivelAtual).ToListAsync();
            StringBuilder str = new StringBuilder();

            int pos = 1;
            foreach (var item in f)
            {
                var g = await ctx.Client.GetUserAsync(item.Id);
                str.AppendLine($"**{pos}.** {g.Username}#{g.Discriminator} - *Nível {item.Personagem.NivelAtual}*");
                pos++;
            }
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithDescription(str.ToString());

            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
