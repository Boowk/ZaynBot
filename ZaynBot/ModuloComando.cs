using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using ZaynBot.Core;
using ZaynBot.Core.Comandos;
using ZaynBot.RPG.Comandos;
using ZaynBot.RPG.Comandos.Ativavel;
using ZaynBot.RPG.Comandos.Exibir;
using ZaynBot.RPG.Comandos.Viajar;
using ZaynBot.RPG.Exceptions;

namespace ZaynBot
{
    public class ModuloComando
    {
        public static CommandsNextExtension Comandos { get; private set; }

        public ModuloComando(CommandsNextConfiguration ccfg, DiscordClient client)
        {
            Comandos = client.UseCommandsNext(ccfg);
            Comandos.CommandExecuted += ComandoExecutado;
            Comandos.CommandErrored += ComandoAconteceuErro;
            Comandos.SetHelpFormatter<IAjudaComando>();


            Comandos.RegisterCommands<AjudaComando>();
            Comandos.RegisterCommands<PrefixComando>();

            Comandos.RegisterCommands<StatusComando>();

            //Comandos.RegisterCommands<DencansarComando>();
            //Comandos.RegisterCommands<AdmComandos>();
            //Comandos.RegisterCommands<ConviteComando>();
            //Comandos.RegisterCommands<InformacaoComando>();
            //Comandos.RegisterCommands<VotarComando>();

            #region ComandosRPG

            //Comandos.RegisterCommands<StatusComando>();
            //// Comandos.RegisterCommands<GrupoGuilda>();
            //Comandos.RegisterCommands<ReencarnarComando>();
            //Comandos.RegisterCommands<LocalComando>();
            //Comandos.RegisterCommands<ExplorarComando>();
            //Comandos.RegisterCommands<InimigosComandos>();
            //Comandos.RegisterCommands<AtacarComando>();
            //Comandos.RegisterCommands<LesteComando>();
            //Comandos.RegisterCommands<NorteComando>();
            //Comandos.RegisterCommands<OesteComando>();
            //Comandos.RegisterCommands<SulComando>();
            //Comandos.RegisterCommands<MochilaComando>();
            //Comandos.RegisterCommands<HabilidadeComando>();
            ////Comandos.RegisterCommands<ExaminarComando>();
            //Comandos.RegisterCommands<EquiparComando>();
            //Comandos.RegisterCommands<DesequiparComando>();
            //Comandos.RegisterCommands<PersonagemComando>();
            //Comandos.RegisterCommands<ReceitaComando>();
            #endregion
        }

        // Envia mensagem ao receber um erro no bot
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
                        e.Context.Client.DebugLogger.LogMessage(LogLevel.Info, e.Context.Guild.Id.ToString(), $"{ctx.Message.Author.Id} tentou usar {ctx.Message.Content}: {t.TotalSeconds}", DateTime.Now);
                    }
                    return;
                case ArgumentException ax:
                    await ctx.RespondAsync($"{ctx.Member.Mention}, você está colocando algum parâmetro errado. Utilize z!ajuda {e.Command?.QualifiedName ?? "comando digitado"}.");
                    ctx.Client.DebugLogger.LogMessage(LogLevel.Info, ctx.Guild.Id.ToString(), $"{ctx.Message.Author.Id} parâmetros errados no comando {e.Command?.QualifiedName}.", DateTime.Now);
                    return;
                case UnauthorizedException ux:
                    return;
                case InvalidOperationException cff:
                case CommandNotFoundException cf:
                    if (e.Command != null)
                        if (e.Command.Name == "ajuda")
                        {
                            DiscordEmoji x = DiscordEmoji.FromName(ctx.Client, ":no_entry_sign:");
                            await ctx.RespondAsync($"{x} | {ctx.User.Mention} o comando{e.Context.RawArgumentString} não existe.*");
                        }
                    ctx.Client.DebugLogger.LogMessage(LogLevel.Info, ctx.Guild.Id.ToString(), $"{ctx.Member.Id} tentou o comando não existente {ctx.Message.Content}.", DateTime.Now);
                    return;
                case PersonagemNullException px:
                    await ctx.RespondAsync($"{ctx.User.Mention}, {px.ToString()}");
                    return;
                case NotFoundException nx:
                    await ctx.RespondAsync($"{ctx.User.Mention}, usuario não encontrado.");
                    return;
                case PersonagemNoLifeException pnx:
                    await ctx.RespondAsync($"{ctx.User.Mention}, {pnx.ToString()}");
                    return;
                default:
                    e.Context.Client.DebugLogger.LogMessage(LogLevel.Error, e.Context.Guild.Id.ToString(), $"{e.Context.User.Id} tentou executar '{e.Command?.QualifiedName ?? "<comando desconhecido>"}' mas deu erro: {e.Exception.GetType()}: {e.Exception.Message ?? "<sem mensagem>"}", DateTime.Now);
                    DiscordGuild MundoZayn = await ModuloCliente.Client.GetGuildAsync(420044060720627712);
                    DiscordChannel CanalRPG = MundoZayn.GetChannel(600736364484493424);
                    await CanalRPG.SendMessageAsync($"{e.Context.User.Id} tentou executar '{e.Command?.QualifiedName ?? "<comando desconhecido>"}' mas deu erro: {e.Exception.GetType()}: {e.Exception.Message ?? "<sem mensagem>"}");
                    break;
            }
        }

        private Task ComandoExecutado(CommandExecutionEventArgs e)
        {
            e.Context.Client.DebugLogger.LogMessage(LogLevel.Info, e.Context.Guild.Id.ToString(), $"{e.Context.User.Id} executou '{e.Command.QualifiedName}'", DateTime.Now);
            return Task.CompletedTask;
        }
    }
}
