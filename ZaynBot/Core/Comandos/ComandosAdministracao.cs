using DiscordBotsList.Api;
using DiscordBotsList.Api.Objects;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MongoDB.Driver;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ZaynBot.Core.Entidades;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.Core.Comandos
{
    [Group("adm")]
    [Description("Comandos administrativos. - Somente disponível após doação. Verificar no comando convite.")]
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

        [Command("resetar")]
        [RequireOwner]
        [Hidden]
        public async Task Deletar(CommandContext ctx, DiscordUser member)
        {
            await ctx.TriggerTypingAsync();
            Expression<Func<RPGUsuario, bool>> filtro = x => x.Id.Equals(member.Id);
            ModuloBanco.UsuarioColecao.DeleteOne(filtro);
            await ctx.RespondAsync("Membro resetado.");
        }

        [Command("remake")]
        [RequireOwner]
        [Hidden]
        public async Task Remake(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            int quantidade = 0;
            await ModuloBanco.UsuarioColecao.Find(FilterDefinition<RPGUsuario>.Empty)
                .ForEachAsync(x =>
                {
                    Expression<Func<RPGUsuario, bool>> filtro = f => f.Id.Equals(x.Id);
                    if (x.Personagem != null)
                    {
                        x.Personagem = null;
                        quantidade++;
                        ModuloBanco.UsuarioColecao.ReplaceOne(filtro, x);
                    }
                }).ConfigureAwait(false);
            await ctx.RespondAsync($"{quantidade} refeitos.");
        }
    }
}
