using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Exceptions;
using System;
using System.Linq;
using System.Threading.Tasks;
using ZaynBot.Comandos.Administração;
using ZaynBot.Comandos.Informações;
using ZaynBot.Comandos.Rpg;

namespace ZaynBot.Comandos
{
    public class ModuloComandos
    {
        public CommandsNextModule Comandos { get; }

        public ModuloComandos(CommandsNextConfiguration ccfg, DiscordClient client)
        {
            Comandos = client.UseCommandsNext(ccfg);
            Comandos.CommandExecuted += ComandoExecutado;
            Comandos.CommandErrored += ComandoAconteceuErro;
            Comandos.SetHelpFormatter<AjudaFormatador>();
            RegistrarComandos();
        }

        public void RegistrarComandos()
        {
            Comandos.RegisterCommands<ComandosTestes>();
            Comandos.RegisterCommands<Ajuda>();
            Comandos.RegisterCommands<ComandosInfo>();
            Comandos.RegisterCommands<ComandosAdm>();


            #region ComandosRPG
            Comandos.RegisterCommands<Perfil>();
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
                    await ctx.RespondAsync($"{ctx.Member.Mention}, você podera usar esse comando em " + my.GetRemainingCooldown(ctx).Seconds + " segundos.");
                    e.Context.Client.DebugLogger.LogMessage(LogLevel.Info, e.Context.Guild.Name, $"Autor: {ctx.Message.Author} deve esperar {my.GetRemainingCooldown(ctx).Seconds} segundos para usar {ctx.Message.Content}", DateTime.Now);
                    return;
                }

            }
            if (e.Exception is ArgumentException ax)
            {
                await ctx.RespondAsync($"{ctx.Member.Mention}, você está esquecendo de algum parâmetro? Utilize z!ajuda {e.Command?.QualifiedName ?? "comando digitado"}.");
            }
            if (e.Exception is UnauthorizedException)
            {
                ctx.Client.DebugLogger.LogMessage(LogLevel.Info, ctx.Guild.Name, $"Sem permissão para falar no canal {ctx.Channel.Name}.", DateTime.Now);
                return;
            }
            if (e.Exception is InvalidOperationException || e.Exception is CommandNotFoundException)
            {
                ctx.Client.DebugLogger.LogMessage(LogLevel.Info, ctx.Guild.Name, $"{ctx.Member.Username}, tentou executar o comando {ctx.Message.Content} mas não existe.", DateTime.Now);
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
