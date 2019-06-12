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
        [Description("Falar com <npc>")]
        public class GrupoGuilda
        {


            public async Task ExecuteGroupAsync(CommandContext ctx)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, o uso correto é falar com <npc>.");
            }

            [Command("com")]
            [Description("Conversa com algum npc")]
            [Cooldown(1, 1, CooldownBucketType.User)]
            public async Task ComandoGuildaInfo(CommandContext ctx, [Description("Nome"), RemainingText]string nome)
            {
                RPGUsuario usuario = await Banco.ConsultarUsuarioPersonagemAsync(ctx);
                if (usuario.Personagem == null) return;
                RPGPersonagem personagem = usuario.Personagem;
                if (string.IsNullOrWhiteSpace(nome))
                {
                    await ctx.RespondAsync($"{ctx.User.Mention}, você quer falar com o que?");
                    return;
                }

                string nomeMinusculo = nome.ToLower();
                RPGRegião localAtual = Banco.ConsultarRegions(personagem.LocalAtualId);
                RPGNpc npc = localAtual.Npcs.Find(x => x.Nome.ToLower() == nomeMinusculo);
                if (npc == null)
                {
                    await ctx.RespondAsync($"{ctx.User.Mention} , {nome} não foi encontrado.");
                    return;
                }

                DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
                embed.WithAuthor($"Conversa do {ctx.User.Username}", icon_url: ctx.User.AvatarUrl);
                embed.WithTitle($"**⌈{npc.Nome}⌋**");
                embed.WithFooter("Clique no emoji para escolhar um diálogo.");
                embed.WithColor(DiscordColor.Goldenrod);
                ListaEmojisSelecao emojis = new ListaEmojisSelecao(ctx);
                StringBuilder dialogos = new StringBuilder();
                foreach (var item in npc.Perguntas)
                {
                    dialogos.Append($"{emojis.ProxEmoji()} - {item.Pergunta}\n");
                }
                embed.WithDescription(dialogos.ToString());
                DiscordMessage mensagem = await ctx.RespondAsync(embed: embed.Build());
                emojis.ResetSelecao();
                CancellationTokenSource cts = new CancellationTokenSource();
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
                            .ContinueWith(x => GetReacao(item, x.Result, usuario, ctx, cts), cts.Token);
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

            public async Task GetReacao(RPGNpcPergunta perguntaEscolhida, ReactionContext reacao, RPGUsuario usuario, CommandContext ctx, CancellationTokenSource cts)
            {
                cts.Cancel();
                if (reacao == null)
                    return;

                RPGEmbed embed = new RPGEmbed(ctx, "Historia do");
                embed.Embed.WithDescription(perguntaEscolhida.Resposta);
                await ctx.RespondAsync(embed: embed.Build());
            }
        }
    }
}
