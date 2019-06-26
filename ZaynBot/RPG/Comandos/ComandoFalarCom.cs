using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoFalarCom
    {
        [Group("falar", CanInvokeWithoutSubcommand = true)]
        [Description("Permite falar com os npcs.\n\n" +
            "Uso: z!falar com *[npc]*\n\n" +
            "Exemplo: z!falar com *voz estranha*")]
        public class GrupoGuilda
        {
            public async Task ExecuteGroupAsync(CommandContext ctx)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, uso correto é: z!falar com *[npc]*\n\n" +
            "Exemplo: z!falar com *voz estranha*");
            }

            [Command("com")]
            [Description("Permite falar com os npcs.\n\n" +
            "Uso: z!falar com *[npc]*\n\n" +
            "Exemplo: z!falar com *voz estranha*")]
            [Cooldown(1, 10, CooldownBucketType.User)]
            public async Task ComandoGuildaInfo(CommandContext ctx, [RemainingText]string nome)
            {
                RPGUsuario usuario = await ModuloBanco.UsuarioConsultarPersonagemAsync(ctx);
                if (usuario.Personagem == null) return;
                RPGPersonagem personagem = usuario.Personagem;
                if (string.IsNullOrWhiteSpace(nome))
                {
                    await ctx.RespondAsync($"{ctx.User.Mention}, você quer falar com quem?");
                    return;
                }
                string nomeMinusculo = nome.ToLower();
                RPGRegião localAtual = ModuloBanco.RegiaoConsultar(personagem.LocalAtualId);
                RPGNpc npc = localAtual.Npcs.Find(x => x.Nome.ToLower() == nomeMinusculo);
                if (npc == null)
                {
                    await ctx.RespondAsync($"{ctx.User.Mention} , {nome} não foi encontrado.");
                    return;
                }
                CancellationTokenSource cts = new CancellationTokenSource();
                CancelamentoToken.AdicionarOuAtualizar(ctx, cts);
                RPGEmbed embed = new RPGEmbed(ctx, "Conversa do", npc);
                embed.Titulo(npc.Nome);
                embed.Embed.WithFooter("Clique no emoji para escolhar um diálogo.");
                embed.Embed.WithColor(DiscordColor.Goldenrod);
                ListaEmojisSelecao emojis = new ListaEmojisSelecao(ctx);
                StringBuilder dialogos = new StringBuilder($"{npc.FalaInicial}\n");
                foreach (var item in npc.Perguntas)
                {
                    bool missaoConcluida = false;
                    foreach (var missaoId in personagem.MissoesConcluidasId)
                    {
                        if (item.MissaoId == missaoId)
                        {
                            missaoConcluida = true;
                            break;
                        }
                    }
                    if (missaoConcluida != true)
                    {
                        if (personagem.MissaoEmAndamento != null)
                        {
                            if (item.MissaoId == personagem.MissaoEmAndamento.Id)
                            {
                                dialogos.Append($"{emojis.ProxEmoji()} - {item.MissaoAtivaPergunta}\n");
                                continue;
                            }
                        }
                        dialogos.Append($"{emojis.ProxEmoji()} - {item.Pergunta}\n");
                    }
                }
                embed.Embed.WithDescription(dialogos.ToString());
                DiscordMessage mensagem = await ctx.RespondAsync(embed: embed.Build());
                emojis.ResetSelecao();
                var interacao = ctx.Client.GetInteractivityModule();
                Task[] opcoes;
                try
                {
                    opcoes = new Task[npc.Perguntas.Count];
                    int index = 0;
                    foreach (var item in npc.Perguntas)
                    {
                        DiscordEmoji emoji = emojis.ProxEmoji();
                        await mensagem.CreateReactionAsync(emoji);
                        Func<DiscordEmoji, bool> emojiFun = x => x.Equals(emoji);
                        opcoes[index] = interacao.WaitForMessageReactionAsync(emojiFun, mensagem, ctx.User, TimeSpan.FromSeconds(60))
                            .ContinueWith(x => GetReacao(npc, item, x.Result, usuario, ctx, cts), cts.Token);
                        index++;
                    }
                }
                catch
                {
                    await ctx.RespondAsync("Não tenho permissão para adicionar emojis :(");
                    return;
                }
                await Task.WhenAny(opcoes);

                //if (mensagem == null)
                //{
                //    if (npc.Logica[quantidade].Loja)
                //    {
                //        StringBuilder itens = new StringBuilder();
                //        DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
                //        embed.WithTitle($"Itens a venda do {npc.Nome}");

                //        foreach (var item in npc.ItensAVenda)
                //        {
                //            itens.Append($"{item.Value.Item.Nome} - {item.Value.Preco} Zayns\n");
                //        }

                //        embed.WithDescription(itens.ToString() + "\nPara comprar escreva o nome do item.");
                //        embed.WithColor(DiscordColor.Green);
                //        await ctx.RespondAsync(ctx.User.Mention, embed: embed.Build());

                //        m = await Interactivity.WaitForMessageAsync(
                //            x => x.Channel.Id == ctx.Channel.Id
                //            && x.Author.Id == ctx.User.Id, TimeSpan.FromSeconds(30));


                //        if (m == null)
                //            return;

                //        if (!npc.ItensAVenda.TryGetValue(m.Message.Content.ToLower(), out NpcVenda itemNpc))
                //        {
                //            await ctx.RespondAsync($"{ctx.User.Mention}, {npc.Nome} não vende {m.Message.Content}.");
                //            return;
                //        }
                //        //if (!personagem.Mochila.TryGetValue("zayn", out Item zayn))
                //        //{
                //        //    await ctx.RespondAsync($"{ctx.User.Mention}, você não tem Zayns para comprar {itemNpc.Item.Nome}.");
                //        //    return;
                //        //}
                //        //if (zayn.Quantidade >= itemNpc.Preco)
                //        //{

                //        //}
                //        //else
                //        //{
                //        //    await ctx.RespondAsync($"{ctx.User.Mention}, você tem só {zayn.Quantidade}, você precisa de {itemNpc.Preco} para comprar 1 únidade.");
                //        //    return;
                //        //}

                //        await ctx.RespondAsync($"{ctx.User.Mention}, quantos {itemNpc.Item.Nome} você deseja comprar?");
                //        m = await Interactivity.WaitForMessageAsync(
                //            x => x.Channel.Id == ctx.Channel.Id
                //            && x.Author.Id == ctx.User.Id, TimeSpan.FromSeconds(30));


                //        if (m == null)
                //        {
                //            return;
                //        }
                //        quantidade = 0;
                //        try
                //        {
                //            quantidade = Convert.ToInt32(m.Message.Content);
                //        }
                //        catch
                //        {
                //            await ctx.RespondAsync($"{ctx.User.Mention}, você precisa informar um número inteiro.");
                //            return;
                //        }
                //        if (quantidade < 1)
                //        {
                //            await ctx.RespondAsync($"{ctx.User.Mention}, você precisa informar um número inteiro maior que 0.");
                //            return;
                //        }

                //        //if (itemNpc.Preco * quantidade > zayn.Quantidade)
                //        //{
                //        //    await ctx.RespondAsync($"{ctx.User.Mention}, você não tem zayn o suficiente para comprar {quantidade}. Você tem somente para comprar {Convert.ToInt32(zayn.Quantidade / itemNpc.Preco)}.");
                //        //    return;
                //        //}

                //        //personagem.Mochila.RemoverItemQuantidade("zayn", itemNpc.Preco * quantidade);
                //        //Item itemComprado = itemNpc.Item.Clone();
                //        //itemComprado.Quantidade = quantidade;
                //        //personagem.Mochila.Adicionar(itemComprado);

                //        await ctx.RespondAsync($"{ctx.User.Mention}, você acabou de comprar do {npc.Nome} {quantidade} {itemNpc.Item.Nome}");


                //    }
                //    else
                //    {
                //        await ctx.RespondAsync($"{ctx.User.Mention}, não existe esta opção.");
                //    }
                //}
            }

            public async Task GetReacao(RPGNpc npc, RPGNpcPergunta perguntaEscolhida, ReactionContext reacao, RPGUsuario usuario, CommandContext ctx, CancellationTokenSource cts)
            {
                cts.Cancel();
                if (reacao == null)
                    return;
                if (usuario.Personagem.MissaoEmAndamento == null)
                {
                    RPGEmbed embed = new RPGEmbed(ctx, "Diálogo do", npc);
                    embed.DescricaoFala(npc, perguntaEscolhida.Resposta);

                    await ctx.RespondAsync(embed: embed.Build());
                    await EnviarMissao(perguntaEscolhida, usuario, ctx);
                    return;
                }
                if (perguntaEscolhida.MissaoId == usuario.Personagem.MissaoEmAndamento.Id)
                {
                    RPGEmbed embed = new RPGEmbed(ctx, "Missão do");
                    embed.Embed.WithDescription(usuario.Personagem.MissaoEmAndamento.Descricao);
                    await ctx.RespondAsync(embed: embed.Build());
                }
                else
                {
                    RPGEmbed embed = new RPGEmbed(ctx, "Diálogo do", npc);
                    embed.DescricaoFala(npc, perguntaEscolhida.Resposta);
                    await ctx.RespondAsync(embed: embed.Build());
                    await EnviarMissao(perguntaEscolhida, usuario, ctx);
                }
            }

            public async Task EnviarMissao(RPGNpcPergunta perguntaEscolhida, RPGUsuario usuario, CommandContext ctx)
            {
                if (perguntaEscolhida.Missao == true)
                {
                    CancellationTokenSource cts = new CancellationTokenSource();
                    CancelamentoToken.AdicionarOuAtualizar(ctx, cts);
                    DiscordEmoji emojiSim = DiscordEmoji.FromName(ModuloCliente.Client, ":regional_indicator_s:");
                    DiscordEmoji emojiNao = DiscordEmoji.FromName(ModuloCliente.Client, ":regional_indicator_n:");
                    RPGMissao missao = ModuloBanco.MissaoConsultar(perguntaEscolhida.MissaoId);
                    RPGEmbed embedMissao = new RPGEmbed(ctx, "Missão do");
                    embedMissao.Embed.WithTitle("Você recebeu uma missão");
                    embedMissao.Embed.WithFooter("S para aceitar, N para recusar");
                    embedMissao.Embed.WithDescription(missao.Descricao);
                    DiscordMessage mensagem = await ctx.RespondAsync(embed: embedMissao.Build());
                    var interacao = ctx.Client.GetInteractivityModule();
                    Task[] opcoes = new Task[2];
                    bool emojiFunSim(DiscordEmoji x) => x.Equals(emojiSim);
                    bool emojiFunNao(DiscordEmoji x) => x.Equals(emojiNao);
                    opcoes[0] = interacao.WaitForMessageReactionAsync(emojiFunNao, mensagem, ctx.User, TimeSpan.FromSeconds(60))
                        .ContinueWith(x => GetReacaoMissao(missao, x.Result, usuario, ctx, cts), cts.Token);
                    opcoes[1] = interacao.WaitForMessageReactionAsync(emojiFunSim, mensagem, ctx.User, TimeSpan.FromSeconds(60))
                        .ContinueWith(x => GetReacaoMissao(missao, x.Result, usuario, ctx, cts), cts.Token);
                    try
                    {
                        await mensagem.CreateReactionAsync(emojiSim);
                        await mensagem.CreateReactionAsync(emojiNao);
                    }
                    catch
                    {
                        await ctx.RespondAsync("Não tenho permissão para adicionar emojis :(");
                        cts.Cancel();
                        return;
                    }
                    await Task.WhenAny(opcoes);
                }
            }

            public async Task GetReacaoMissao(RPGMissao missao, ReactionContext reacao, RPGUsuario usuario, CommandContext ctx, CancellationTokenSource cts)
            {
                cts.Cancel();
                if (reacao == null)
                    return;
                if (reacao.Emoji.GetDiscordName() == ":regional_indicator_s:")
                {
                    usuario.Personagem.MissaoEmAndamento = missao;
                    ModuloBanco.UsuarioAlterar(usuario);
                    await ctx.RespondAsync($"{ctx.User.Mention}, missão `{missao.Nome}` aceita!");
                }
            }
        }
    }
}
