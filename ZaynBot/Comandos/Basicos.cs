using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Atributos;

namespace ZaynBot.Comandos
{
    public class Basicos
    {
        [Command("conquistas")]
        [Description("Exibe as suas conquistas ou a de um usuário.")]
        [ComoUsar("conquistas")]
        [ComoUsar("conquistas @user")]
        [ComoUsar("conquistas user")]
        [Cooldown(1, 5, CooldownBucketType.User)]
        public async Task ConquistaAsync(CommandContext ctx, [RemainingText] DiscordUser user = null)
        {
            await ctx.TriggerTypingAsync();
            if (user == null) user = ctx.User;
            var usuario = ModuloBanco.GetUsuario(user.Id);
            StringBuilder srb = new StringBuilder();
            foreach (var item in usuario.Conquistas)
                srb.AppendLine($"{item.Value.Progresso} {item.Value.Nome}.".Bold());
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Conquistas", user);
            embed.WithDescription(srb.ToString());
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
