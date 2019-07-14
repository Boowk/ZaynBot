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
using ZaynBot.Data.Habilidades.Passivas;
using ZaynBot.RPG.Comandos.Combate;
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
                    Expression<Func<RPGUsuario, bool>> filtro = f => f.Id == x.Id;
                    if (x.Personagem != null)
                    {
                        quantidade++;
                        x.Personagem.Habilidades.Remove("regeneração");
                        x.Personagem.Habilidades.Add(CuraMensagemHabilidade.CuraMensagemHabilidadeAb().Nome, CuraMensagemHabilidade.CuraMensagemHabilidadeAb());
                        ModuloBanco.UsuarioColecao.ReplaceOne(filtro, x);
                    }
                }).ConfigureAwait(false);
            await ctx.RespondAsync($"{quantidade} refeitos.");
        }

        [Command("xpH")]
        [RequireOwner]
        [Hidden]
        public async Task xpH(CommandContext ctx, float quantidade, DiscordUser user = null)
        {
            if (user == null)
            {
                RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioBaseAsync(ctx);
                usuario.Personagem.Habilidades.TryGetValue("regeneração", out RPGHabilidade regen);
                regen.AdicionarExp(quantidade);
                RPGUsuario.UpdateRPGUsuario(usuario);
                await new ComandoHabilidade().ComandoHabilidadeAb(ctx, "regeneração");
            }
            else
            {
                RPGUsuario usuario = RPGUsuario.GetRPGUsuario(user);
                usuario.Personagem.Habilidades.TryGetValue("regeneração", out RPGHabilidade regen);
                regen.AdicionarExp(quantidade);
                RPGUsuario.UpdateRPGUsuario(usuario);
                await ctx.RespondAsync($"Adicionado {quantidade} xp em regeneração em {user.Username}.");
            }
        }

        [Command("testv")]
        [RequireOwner]
        public async Task Vote(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            AuthDiscordBotListApi DblApi = new AuthDiscordBotListApi(CoreBot.Id, CoreBot.DiscordBotsApiKey);
            var me = await DblApi.HasVoted(ctx.User.Id);
            await Task.CompletedTask;
        }
    }
}
