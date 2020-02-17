using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Atributos;
using static DSharpPlus.CommandsNext.CommandsNextExtension;

namespace ZaynBot.Comandos
{
    public class Basicos : BaseCommandModule
    {
        [Command("conquistas")]
        [Description("Exibe as suas conquistas ou a de um usuário.")]
        [ComoUsar("conquistas [ @user | user ]")]
        [Exemplo("conquistas")]
        [Exemplo("conquistas @user")]
        [Exemplo("conquistas user")]
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
        [ComoUsar("ajuda [ comando ]")]
        [Exemplo("ajuda")]
        [Exemplo("ajuda conquistas")]
        [Cooldown(1, 5, CooldownBucketType.User)]
        public async Task ComandoAjudaAsync(CommandContext ctx, params string[] comando)
        {
            await ctx.TriggerTypingAsync();
            await new DefaultHelpModule().DefaultHelpAsync(ctx, comando);
        }

        [Command("ping")]
        [Description("Mostra o tempo de resposta do bot com o discord.")]
        [Exemplo("ping")]
        [Cooldown(1, 5, CooldownBucketType.User)]
        public async Task ComandoPingAsync(CommandContext ctx, params string[] comando)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync($"Ping: {ctx.Client.Ping} ms.".Bold());
        }

        [Command("avatar")]
        [Description("Mostra o seu avatar ou de um usuário.")]
        [ComoUsar("avatar [ @user | user ]")]
        [Exemplo("avatar")]
        [Exemplo("avatar @user")]
        [Exemplo("avatar user")]
        [Cooldown(1, 5, CooldownBucketType.User)]
        public async Task ComandoPingAsync(CommandContext ctx, [RemainingText] DiscordUser user = null)
        {
            await ctx.TriggerTypingAsync();
            if (user == null) user = ctx.User;
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithDescription($"[Clique aqui para baixar!]({user.AvatarUrl})");
            embed.WithImageUrl(user.AvatarUrl);
            await ctx.RespondAsync(embed: embed.Build());
        }

        [Command("convite")]
        [Description("Exibe o convite para adicionar o bot no seu servidor.")]
        [Exemplo("convite")]
        [Cooldown(1, 5, CooldownBucketType.User)]
        public async Task ComandoConviteAsync(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync("https://discordapp.com/api/oauth2/authorize?client_id=459873132975620134&permissions=16384&scope=bot");
        }

        [Command("votar")]
        [Description("Exibe o convite para votar no bot pelo site do top.gg.")]
        [Exemplo("votar")]
        [Cooldown(1, 5, CooldownBucketType.User)]
        public async Task ComandoVotarAsync(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync("https://top.gg/bot/459873132975620134/vote");
        }

        [Command("info")]
        [Description("Exibe uma breve descrição do bot.")]
        [ComoUsar("info")]
        [Cooldown(1, 5, CooldownBucketType.User)]
        public async Task ComandoInfoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.AddField("Servidores".Titulo(), Eventos.Guilda.ServerCount.ToString(), true);
            embed.WithColor(DiscordColor.Blue);
            embed.WithThumbnailUrl("https://blog.jonygames.com.br/wp-content/uploads/2017/07/RPG-a-sigla-que-mudou-o-mundo-dos-jogos.jpg");
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
