using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;

namespace ZaynBot.RPG.Comandos
{
    public class UsuarioComando : BaseCommandModule
    {
        [Command("usuario")]
        [Description("Exibe o perfil de um usuario do discord")]
        [ComoUsar("usuario [id|menção]")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task UsuarioComandoAb(CommandContext ctx, DiscordUser usuario = null)
        {
            await ctx.TriggerTypingAsync();
            if (usuario == null)
                usuario = ctx.User;
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithColor(DiscordColor.Red);
            embed.WithAuthor($"{usuario.Username}#{usuario.Discriminator}", iconUrl: usuario.AvatarUrl);
            embed.WithImageUrl(usuario.AvatarUrl);
            embed.AddField("Discord Tag", $"{usuario.Username}#{usuario.Discriminator}", true);
            embed.AddField("Nome no Discord", usuario.Username, true);
            embed.AddField("ID", $"```css\n{usuario.Id}```");
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
