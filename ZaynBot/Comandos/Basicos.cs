using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Atributos;
using static DSharpPlus.CommandsNext.CommandsNextExtension;

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
        public async Task ComandoConquistaAsync(CommandContext ctx, [RemainingText] DiscordUser user = null)
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

        [Command("ajuda")]
        [Aliases("h", "?", "help")]
        [Description("Mostra todos os comandos disponíveis. Se usado em algum comando existente, explica como usar, suas abreviações e exemplos.")]
        [Exemplo("ajuda")]
        [Exemplo("ajuda conquistas")]
        [Cooldown(1, 5, CooldownBucketType.User)]
        public async Task ComandoAjudaAsync(CommandContext ctx, params string[] comando)
        {
            await ctx.TriggerTypingAsync();
            await new DefaultHelpModule().DefaultHelpAsync(ctx, comando);
        }
    }
}
