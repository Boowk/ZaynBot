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
using ZaynBot.RPG.Comandos.Combate;
using ZaynBot.RPG.Comandos.Viajar;
using ZaynBot.RPG.Exceptions;

namespace ZaynBot
{
    public class ModuloComando
    {
        public CommandsNextModule Comandos { get; }

        public ModuloComando(CommandsNextConfiguration ccfg, DiscordClient client)
        {
            Comandos = client.UseCommandsNext(ccfg);
            Comandos.CommandExecuted += ComandoExecutado;
            Comandos.CommandErrored += ComandoAconteceuErro;
            Comandos.SetHelpFormatter<AjudaFormatador>();

            Comandos.RegisterCommands<ComandosTestes>();
            Comandos.RegisterCommands<ComandoAjuda>();
            Comandos.RegisterCommands<ComandosGrupoInfo>();
            Comandos.RegisterCommands<ComandosAdministracao>();
            Comandos.RegisterCommands<ComandoPerfil>();
            Comandos.RegisterCommands<ComandoConvite>();
            Comandos.RegisterCommands<ComandoInfo>();


            #region ComandosRPG
            Comandos.RegisterCommands<ComandosPersonagem>();
            // Comandos.RegisterCommands<GrupoGuilda>();
            Comandos.RegisterCommands<ComandoReencarnar>();
            Comandos.RegisterCommands<ComandoLocalizacao>();
            Comandos.RegisterCommands<ComandoExplorar>();
            Comandos.RegisterCommands<ComandoInimigos>();
            Comandos.RegisterCommands<ComandoAtacar>();
            Comandos.RegisterCommands<ComandoLeste>();
            Comandos.RegisterCommands<ComandoNorte>();
            Comandos.RegisterCommands<ComandoOeste>();
            Comandos.RegisterCommands<ComandoSul>();
            Comandos.RegisterCommands<ComandoFalarCom>();
            Comandos.RegisterCommands<ComandoMissao>();
            Comandos.RegisterCommands<ComandoRaca>();
            Comandos.RegisterCommands<ComandoInventario>();
            Comandos.RegisterCommands<ComandoPegar>();
            Comandos.RegisterCommands<ComandoLoot>();
            Comandos.RegisterCommands<ComandoColetar>();
            Comandos.RegisterCommands<ComandoHabilidade>();
            Comandos.RegisterCommands<ComandoHabilidades>();
            #endregion
        }

        private async Task ComandoAconteceuErro(CommandErrorEventArgs e)
        {
            CommandContext ctx = e.Context;

            if (e.Exception is ChecksFailedException ex)
            {
                if (!(ex.FailedChecks.FirstOrDefault(x => x is CooldownAttribute) is CooldownAttribute my))
                {
                    return;
                }
                else
                {
                    int tempo = Convert.ToInt32((my.GetRemainingCooldown(ctx).TotalSeconds));
                    await ctx.RespondAsync($"{ctx.Member.Mention}, você podera usar esse comando em " + tempo + " segundos.");
                    e.Context.Client.DebugLogger.LogMessage(LogLevel.Info, e.Context.Guild.Name, $"{ctx.Message.Author} deve esperar {tempo} segundos para usar {ctx.Message.Content}", DateTime.Now);
                    return;
                }

            }
            if (e.Exception is ArgumentException ax)
            {
                await ctx.RespondAsync($"{ctx.Member.Mention}, você está colocando algum parâmetro errado. Utilize z!ajuda {e.Command?.QualifiedName ?? "comando digitado"}.");
                ctx.Client.DebugLogger.LogMessage(LogLevel.Info, ctx.Guild.Name, $"{ctx.Message.Author} Colocou parâmetros errados no comando {e.Command?.QualifiedName}.", DateTime.Now);
                return;
            }
            if (e.Exception is UnauthorizedException)
            {
                ctx.Client.DebugLogger.LogMessage(LogLevel.Info, ctx.Guild.Name, $"Sem permissão para falar no canal {ctx.Channel.Name}.", DateTime.Now);
                return;
            }
            if (e.Exception is InvalidOperationException || e.Exception is CommandNotFoundException)
            {
                if (e.Command != null)
                    if (e.Command.Name == "ajuda")
                    {
                        DiscordEmoji x = DiscordEmoji.FromName(ctx.Client, ":no_entry_sign:");
                        await ctx.RespondAsync($"**{x} | {ctx.User.Mention}, o comando{e.Context.RawArgumentString} não existe.**");
                    }
                ctx.Client.DebugLogger.LogMessage(LogLevel.Info, ctx.Guild.Name, $"{ctx.Member.Username}, tentou executar o comando {ctx.Message.Content} mas não existe.", DateTime.Now);
                return;
            }
            if (e.Exception is PersonagemNullException px)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, {px.ToString()}");
                ctx.Client.DebugLogger.LogMessage(LogLevel.Info, ctx.Guild.Name, $"{ctx.Member.Username}, tentou executar o comando {ctx.Message.Content} mas não tem um personagem.", DateTime.Now);
                return;
            }

            if (e.Exception is NotFoundException nx)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, usuario não encontrado.");
                ctx.Client.DebugLogger.LogMessage(LogLevel.Info, ctx.Guild.Name, $"{ctx.Member.Username}, tentou executar o comando {ctx.Message.Content} mas colocou um usuario inválido", DateTime.Now);
                return;
            }
            //if(e.Exception is AggregateException)
            //{
            //    ctx.Client.DebugLogger.LogMessage(LogLevel.Info, ctx.Guild.Name, $"{ctx.Member.Username}, tentou executar o comando {ctx.Message.Content} mas não existe.", DateTime.Now);
            //    return;
            //}
            e.Context.Client.DebugLogger.LogMessage(LogLevel.Error, e.Context.Guild.Name, $"{e.Context.User.Username} tentou executar '{e.Command?.QualifiedName ?? "<comando desconhecido>"}' mas deu erro: {e.Exception.GetType()}: {e.Exception.Message ?? "<sem mensagem>"}", DateTime.Now);
            return;
        }

        private Task ComandoExecutado(CommandExecutionEventArgs e)
        {
            e.Context.Client.DebugLogger.LogMessage(LogLevel.Info, e.Context.Guild.Name, $"{e.Context.User.Username} executou com sucesso '{e.Command.QualifiedName}'", DateTime.Now);
            return Task.CompletedTask;
        }
    }
}
