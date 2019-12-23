using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;

namespace ZaynBot.Core.Comandos
{
    public class ComandoUsuario : BaseCommandModule
    {
        [Command("usuario")]
        [Description("Exibe o perfil de um usuario do discord")]
        [ComoUsar("usuario [id|menção]")]
        [Cooldown(1, 4, CooldownBucketType.User)]
        [Priority(0)]
        public async Task ComandoUsuarioAb(CommandContext ctx, DiscordUser usuario = null)
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

        [Command("usuario")]
        [Cooldown(1, 4, CooldownBucketType.User)]
        public async Task ComandoUsuarioAb(CommandContext ctx, int usuarioId = 0)
        {
            if (usuarioId.ToString().Length < 17)
            {
                await ctx.TriggerTypingAsync();
                await ctx.ExecutarComandoAsync("ajuda usuario");
            }
        }

        [Command("usuario")]
        [Cooldown(1, 4, CooldownBucketType.User)]
        public async Task ComandoUsuarioAb(CommandContext ctx, string usuarioId)
        {
            await ctx.TriggerTypingAsync();
            await ctx.ExecutarComandoAsync("ajuda usuario");
        }
    }
}
