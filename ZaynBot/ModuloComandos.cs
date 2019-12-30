using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.Exceptions;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZaynBot.Core;
using ZaynBot.Core.Comandos;
using ZaynBot.RPG.Comandos;
using ZaynBot.RPG.Exceptions;

namespace ZaynBot
{
    public class ModuloComandos
    {
        public static CommandsNextExtension Comandos { get; private set; }

        public ModuloComandos(CommandsNextConfiguration ccfg, DiscordClient client)
        {
            Comandos = client.UseCommandsNext(ccfg);
            Comandos.CommandExecuted += ComandoExecutado;
            Comandos.CommandErrored += ComandoAconteceuErro;
            Comandos.SetHelpFormatter<IAjudaComando>();

            Comandos.RegisterCommands<ComandoAjuda>();
            Comandos.RegisterCommands<ComandoConvite>();
            Comandos.RegisterCommands<ComandoInfo>();
            Comandos.RegisterCommands<ComandoVotar>();
            Comandos.RegisterCommands<ComandoUsuario>();
            Comandos.RegisterCommands<ComandoAdm>();
            Comandos.RegisterCommands<ComandoMod>();

            Comandos.RegisterCommands<TopComando>();

            Comandos.RegisterCommands<ComandoLoja>();
            Comandos.RegisterCommands<ComandoItemUsar>();
            Comandos.RegisterCommands<ComandoItemComprar>();
            Comandos.RegisterCommands<ComandoCriarPersonagem>();
            Comandos.RegisterCommands<ComandoStatus>();
            Comandos.RegisterCommands<ComandoProficiencia>();
            Comandos.RegisterCommands<ComandoLocal>();
            Comandos.RegisterCommands<ComandoExplorar>();
            Comandos.RegisterCommands<ComandoTutorial>();
            Comandos.RegisterCommands<ComandoAtacar>();
            Comandos.RegisterCommands<ComandoMochila>();
            Comandos.RegisterCommands<ComandoExaminar>();
            Comandos.RegisterCommands<ComandoViajar>();
            Comandos.RegisterCommands<ComandoItemVender>();
            //Comandos.RegisterCommands<DesequiparComando>();
            //Comandos.RegisterCommands<ReceitaComando>();
        }

        //Envia mensagem ao receber um erro no bot.
        private async Task ComandoAconteceuErro(CommandErrorEventArgs e)
        {
            CommandContext ctx = e.Context;
            switch (e.Exception)
            {
                case ChecksFailedException ex:
                    if (!(ex.FailedChecks.FirstOrDefault(x => x is CooldownAttribute) is CooldownAttribute my))
                        return;
                    else
                    {
                        TimeSpan t = TimeSpan.FromSeconds(my.GetRemainingCooldown(ctx).TotalSeconds);
                        if (t.Days >= 1)
                            await ctx.RespondAsync($"Aguarde {t.Days} dias e ({t.Hours} horas para usar este comando! {ctx.Member.Mention}.");
                        else if (t.Hours >= 1)
                            await ctx.RespondAsync($"Aguarde {t.Hours} horas e {t.Minutes} minutos para usar este comando! {ctx.Member.Mention}.");
                        else if (t.Minutes >= 1)
                            await ctx.RespondAsync($"Aguarde {t.Minutes} minutos e {t.Seconds} segundos para usar este comando! {ctx.Member.Mention}.");
                        else
                            await ctx.RespondAsync($"Aguarde {t.Seconds} segundos para usar este comando! {ctx.Member.Mention}.");
                    }
                    return;
                case ArgumentException ax:
                    await ctx.RespondAsync($"**Aconteceu um erro:** {ax.ToString()}");
                    return;
                case CommandNotFoundException cf:
                    if (e.Command != null)
                        if (e.Command.Name == "ajuda")
                        {
                            DiscordEmoji x = DiscordEmoji.FromName(ctx.Client, ":no_entry_sign:");
                            await ctx.RespondAsync($"{x} | {ctx.User.Mention} o comando {e.Context.RawArgumentString} não existe.*");
                        }
                    return;
                case PersonagemNullException px:
                    await ctx.RespondAsync(px.ToString());
                    return;
                case NotFoundException nx:
                    await ctx.RespondAsync($"{ctx.User.Mention}, usuario não encontrado.");
                    return;
                case PersonagemNoLifeException pnx:
                    DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
                    emb.WithDescription($"{ctx.User.Mention} acabou de morrer!\n" +
                        $"Você perdeu um pouco de experiencia e moedas.");
                    emb.WithImageUrl("https://cdn.discordapp.com/attachments/651848690033754113/657218098033721365/RIP.png\n");
                    await ctx.RespondAsync(ctx.User.Mention, embed: emb.Build());
                    return;
                case MensagemException mx:
                    await ctx.RespondAsync(mx.Message);
                    break;
                case UnauthorizedException ux:
                    break;
                case InvalidOperationException iox:
                    break;
                default:
                    e.Context.Client.DebugLogger.LogMessage(LogLevel.Error, e.Context.Guild.Id.ToString(), $"{e.Context.User.Id} tentou executar '{e.Command?.QualifiedName ?? "<comando desconhecido>"}' mas deu erro: {e.Exception.GetType()}: {e.Exception.Message ?? "<sem mensagem>"}", DateTime.Now);
                    DiscordGuild MundoZayn = await ModuloCliente.Client.GetGuildAsync(420044060720627712);
                    DiscordChannel CanalRPG = MundoZayn.GetChannel(600736364484493424);
                    await CanalRPG.SendMessageAsync($"{e.Context.User.Id} tentou executar '{e.Command?.QualifiedName ?? "<comando desconhecido>"}' mas deu erro: {e.Exception.GetType()}: {e.Exception.Message ?? "<sem mensagem>"}\n{e.Context.Message.JumpLink}");
                    break;
            }
        }

        private Task ComandoExecutado(CommandExecutionEventArgs e)
        {
            e.Context.Client.DebugLogger.LogMessage(LogLevel.Info, $"({e.Context.Guild.Id}) {e.Context.Guild.Name.RemoverAcentos()}", $"({e.Context.User.Id}) {e.Context.User.Username.RemoverAcentos()} executou '{e.Command.QualifiedName}'", DateTime.Now);
            return Task.CompletedTask;
        }
    }
}
