using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using ZaynBot.Atributos;

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
    }
}
