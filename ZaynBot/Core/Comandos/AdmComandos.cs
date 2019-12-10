using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace ZaynBot.Core.Comandos
{
    [Group("adm")]
    [Hidden]
    [RequireOwner]
    public class AdmComandos : BaseCommandModule
    {
        [Command("mp")]
        public async Task MensagemPrivadaComando(CommandContext ctx, DiscordGuild guilda, DiscordUser usuario, [RemainingText] string texto = "")
        {
            await ctx.TriggerTypingAsync();
            DiscordMember membro = await guilda.GetMemberAsync(usuario.Id);
            await membro.SendMessageAsync(texto);
            await ctx.RespondAsync("**Enviado.**");
        }

        [Command("sudo")]
        public async Task Sudo(CommandContext ctx, DiscordUser member, [RemainingText] string command)
        {
            await ctx.TriggerTypingAsync();

            var invocation = command.Substring(2);
            var cmd = ctx.CommandsNext.FindCommand(invocation, out var args);
            if (cmd == null)
            {
                await ctx.RespondAsync("Comando não encontrado");
                return;
            }

            var cfx = ctx.CommandsNext.CreateFakeContext(member, ctx.Channel, "", "z!", cmd, args);
            await ctx.CommandsNext.ExecuteCommandAsync(cfx);
        }
    }
}
