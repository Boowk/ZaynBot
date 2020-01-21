using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MongoDB.Driver;
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
            await ctx.TriggerTypingAsync();
            var f = await ModuloBanco.ColecaoJogador.Find(FilterDefinition<RPGJogador>.Empty).Limit(10)
                .SortByDescending(x => x.NivelAtual).ToListAsync();
            StringBuilder str = new StringBuilder();

            int pos = 1;
            foreach (var item in f)
            {
                var g = await ctx.Client.GetUserAsync(item.Id);
                str.AppendLine($"{pos}. {g.Username}#{g.Discriminator} - *Nível {item.NivelAtual}*".Bold());
                pos++;
            }
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithDescription(str.ToString());

            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
