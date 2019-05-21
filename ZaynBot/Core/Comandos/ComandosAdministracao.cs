using DiscordBotsList.Api;
using DiscordBotsList.Api.Objects;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using ZaynBot.Core.Entidades;

namespace ZaynBot.Core.Comandos
{
    [Group("adm")]
    [Description("Comandos administrativos.")]
    public class ComandosAdministracao
    {
        [Command("botjogando")]
        [RequireOwner]
        [Hidden]
        public async Task ComandoAdmBotJogando(CommandContext ctx, [RemainingText] string texto = "")
        {
            await ctx.TriggerTypingAsync();
            AuthDiscordBotListApi DblApi = new AuthDiscordBotListApi(CoreBot.Id, CoreBot.DiscordBotsApiKey);
            IDblSelfBot me = await DblApi.GetMeAsync();
            await me.UpdateStatsAsync(CoreBot.QuantidadeServidores);

            await ModuloCliente.Client.UpdateStatusAsync(new DiscordGame(texto));
            await ctx.RespondAsync("Status jogando alterado com sucesso para " + CoreBot.QuantidadeServidores + " servidores!");
        }

        [Command("foryou")]
        [RequireOwner]
        [Hidden]
        public async Task ComandoAdmForyou(CommandContext ctx, DiscordGuild f, ulong g, [RemainingText] string texto)
        {
            await ctx.TriggerTypingAsync();
            DiscordGuild ff = await ModuloCliente.Client.GetGuildAsync(f.Id);
            DiscordChannel gg = ff.GetChannel(g);
            await gg.SendMessageAsync(texto);
        }

        [Command("sudo")]
        [RequireOwner]
        [Hidden]
        public async Task Sudo(CommandContext ctx, DiscordUser member, [RemainingText] string command)
        {
            await ctx.TriggerTypingAsync();
            var cmds = ctx.CommandsNext;
            await cmds.SudoAsync(member, ctx.Channel, command);
        }
    }
}
