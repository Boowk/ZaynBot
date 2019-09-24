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
using ZaynBot.RPG.Comandos.Exibir;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.Core.Comandos
{
    [Group("adm")]
    [Hidden]
    [RequireOwner]
    [Description("Comandos administrativos. - Somente disponível após doação. Verificar no comando convite.")]
    public class AdmComandos : BaseCommandModule
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
            await ModuloCliente.Client.UpdateStatusAsync(new DiscordActivity(texto));
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
            //await cmds.SudoAsync(member, ctx.Channel, command);
        }

        [Command("resetar")]
        [RequireOwner]
        [Hidden]
        public async Task Deletar(CommandContext ctx, DiscordUser member)
        {
            await ctx.TriggerTypingAsync();
            Expression<Func<UsuarioRPG, bool>> filtro = x => x.Id.Equals(member.Id);
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
            await ModuloBanco.UsuarioColecao.Find(FilterDefinition<UsuarioRPG>.Empty)
                .ForEachAsync(x =>
                {
                    Expression<Func<UsuarioRPG, bool>> filtro = f => f.Id == x.Id;
                    if (x.Personagem != null)
                    {
                        quantidade++;
                        RacaRPG Raca = x.Personagem.Raca;
                        //x.Personagem.PontosDeVida = Sortear.Atributo(Raca.Constituicao, Raca.Sorte);
                        //x.Personagem.PontosDeVidaMaxima = x.Personagem.PontosDeVida;
                        //x.Personagem.PontosDeMana = Sortear.Atributo(Raca.Inteligencia, Raca.Sorte);
                        //x.Personagem.PontosDeManaMaximo = x.Personagem.PontosDeMana;

                        //x.Personagem.AtaqueFisico = Sortear.Atributo(Raca.Forca, Raca.Sorte);
                        //x.Personagem.DefesaFisica = Sortear.Atributo(Raca.Constituicao, Raca.Sorte);
                        //x.Personagem.AtaqueMagico = Sortear.Atributo(Raca.Inteligencia, Raca.Sorte);
                        //x.Personagem.DefesaMagica = Sortear.Atributo(Raca.Inteligencia, Raca.Sorte);
                        //x.Personagem.Velocidade = Sortear.Atributo(Raca.Destreza, Raca.Sorte);
                        ModuloBanco.UsuarioColecao.ReplaceOne(filtro, x);
                    }
                }).ConfigureAwait(false);
            await ctx.RespondAsync($"{quantidade} refeitos.");
        }

        [Command("estrelas")]
        [RequireOwner]
        [Hidden]
        public async Task Estrelas(CommandContext ctx, int quantidade, DiscordMember user = null)
        {
            if (user == null)
            {
                await ctx.TriggerTypingAsync();
                UsuarioRPG.TryGetPersonagemRPG(ctx, out UsuarioRPG usuario);
                usuario.Estrelas += quantidade;
                UsuarioRPG.Salvar(usuario);
                await ctx.RespondAsync($"{ctx.User.Mention}, recebeu {quantidade} estrelas. :star:");
            }
            else
            {
                UsuarioRPG usuario = UsuarioRPG.UsuarioGet(user);
                usuario.Estrelas += quantidade;
                UsuarioRPG.Salvar(usuario);
                await ctx.RespondAsync($"{user.Mention}, recebeu {quantidade} estrelas. :star:");
            }
        }
    }
}
