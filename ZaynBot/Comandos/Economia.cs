using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MongoDB.Driver;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Atributos;
using ZaynBot.Entidades;

namespace ZaynBot.Comandos
{
    public class Economia : BaseCommandModule
    {

        [Command("dinheiro")]
        [Aliases("din")]
        [Description("Exibe o seu dinheiro ou o de um usuário.")]
        [ComoUsar("dinheiro [ @user | user ]")]
        [Exemplo("dinheiro")]
        [Exemplo("dinheiro @user")]
        [Exemplo("dinheiro user")]
        [Cooldown(1, 5, CooldownBucketType.User)]
        public async Task ComandoDinheiroAsync(CommandContext ctx, DiscordUser user = null)
        {
            await ctx.TriggerTypingAsync();
            if (user == null) user = ctx.User;
            var usuario = ModuloBanco.GetUsuario(user.Id);
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao($"Dinheiro", user);
            embed.AddField("Disponível".Titulo(), $"R$ {usuario.Real.Text()}", true);
            await ctx.RespondAsync(embed: embed.Build());
        }


        [Command("top")]
        [Description("Top 10 pessoas ricas do discord!")]
        [ComoUsar("top")]
        [Cooldown(1, 60, CooldownBucketType.User)]
        public async Task ComandoTopAsync(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var f = await ModuloBanco.Usuarios.Find(FilterDefinition<EntidadeUsuario>.Empty).Limit(10)
                .SortByDescending(x => x.Real).ToListAsync();
            StringBuilder str = new StringBuilder();

            int pos = 1;
            foreach (var item in f)
            {
                var g = await ctx.Client.GetUserAsync(item.Id);
                str.AppendLine($"{pos}. {g.Username}#{g.Discriminator} - *R$ {item.Real.Text()}*".Bold());
                pos++;
            }
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithDescription(str.ToString());

            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
