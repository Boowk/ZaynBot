﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Grupos
{
    [Group("grupo")]
    public class GrupoComando : BaseCommandModule
    {
        [Command("criar")]
        [Description("Permite criar um novo grupo.")]
        [UsoAtributo("grupo [nome]")]
        [Cooldown(1, 60, CooldownBucketType.User)]
        public async Task GrupoCriar(CommandContext ctx, [RemainingText] string nome = "")
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.GetPersonagem(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;

            if (personagem.Batalha.LiderGrupo != 0)
            {
                await ctx.RespondAsync($"Você precisa sair do Grupo atual antes! {ctx.User.Mention}");
                return;
            }

            if (string.IsNullOrWhiteSpace(nome))
            {
                await ctx.RespondAsync($"Você precisa dar um nome ao seu Grupo! {ctx.User.Mention}");
                return;
            }

            if (nome.Length > 20)
            {
                await ctx.RespondAsync($"O nome do Grupo não pode ser maior que 20 letras! {ctx.User.Mention}");
                return;
            }

            personagem.Batalha.LiderGrupo = ctx.User.Id;
            personagem.Batalha.NomeGrupo = nome;
            usuario.Salvar();
            await ctx.RespondAsync($"O Grupo **{nome}** foi criado! {ctx.User.Mention}");
        }

        DiscordEmoji emojiSim;
        DiscordEmoji emojiNao;

        [Command("convidar")]
        [Description("Permite convidar um novo membro para o grupo.")]
        [UsoAtributo("convidar [id|menção]")]
        [ExemploAtributo("convidar 53057768")]
        [ExemploAtributo("convidar @Usuario")]
        [Cooldown(1, 60, CooldownBucketType.User)]
        public async Task GrupoConvidar(CommandContext ctx, DiscordMember userConvidado)
        {
            await ctx.TriggerTypingAsync();

            if (userConvidado.Id == ctx.User.Id)
            {
                await ctx.RespondAsync($"Hahaha, muito engraçadinho você em!! {ctx.User.Mention}.");
                return;
            }

            UsuarioRPG.GetPersonagem(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;

            if (personagem.Batalha.LiderGrupo == 0)
            {
                await ctx.RespondAsync($"Você precisa criar um Grupo antes de começar a convidar! {ctx.User.Mention}.");
                return;
            }

            if (personagem.Batalha.LiderGrupo != ctx.User.Id)
            {
                await ctx.RespondAsync($"Somente o lider do Grupo pode convidar! {ctx.User.Mention}.");
                return;
            }

            foreach (var item in personagem.Batalha.Jogadores)
            {
                if (item == userConvidado.Id)
                {
                    await ctx.RespondAsync($"Hhahaa, convindado duas vez a mesma pessoa?? {ctx.User.Mention}.");
                    return;
                }
            }

            emojiSim = DiscordEmoji.FromName(ctx.Client, ":regional_indicator_s:");
            emojiNao = DiscordEmoji.FromName(ctx.Client, ":regional_indicator_n:");

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithTitle($"**Você recebeu um convite para entrar no grupo: {personagem.Batalha.NomeGrupo}!**");
            embed.WithDescription($"{ctx.User.Mention} está lhe convidando.");
            embed.WithFooter("'S' para aceitar | 'N' para recusar");
            embed.WithTimestamp(DateTime.Now);
            DiscordMessage mensagem = await ctx.RespondAsync($"Atenção {userConvidado.Mention}", embed: embed.Build());


            var interacao = ctx.Client.GetInteractivity();
            Task[] opcoes = new Task[2];
            CancellationTokenSource token = new CancellationTokenSource();
            opcoes[0] = mensagem.WaitForReactionAsync(userConvidado, emojiSim, TimeSpan.FromSeconds(60))
                .ContinueWith(async x => await GetReacaoMissao(x.Result, userConvidado, token, ctx), token.Token);
            opcoes[1] = mensagem.WaitForReactionAsync(userConvidado, emojiNao, TimeSpan.FromSeconds(60))
                    .ContinueWith(async x => await GetReacaoMissao(x.Result, userConvidado, token, ctx), token.Token);

            await mensagem.CreateReactionAsync(emojiSim);
            await mensagem.CreateReactionAsync(emojiNao);

            await Task.WhenAny(opcoes);
        }

        public async Task GetReacaoMissao(InteractivityResult<MessageReactionAddEventArgs> reacao, DiscordUser discordUserConvidado, CancellationTokenSource token, CommandContext ctx)
        {
            token.Cancel();
            await ctx.TriggerTypingAsync();
            int i = 1;
            if (reacao.TimedOut)
            {
                if (i != 0)
                    await ctx.RespondAsync($"{ctx.User.Mention} o seu convite foi recusado por {discordUserConvidado.Mention}!");
                i--;
                return;
            }

            var rec = reacao.Result;
            if (rec.Emoji.Equals(emojiSim))
            {
                UsuarioRPG.UsuarioGet(discordUserConvidado, out UsuarioRPG usuarioConvidado);
                PersonagemRPG personagemConvidado = usuarioConvidado.Personagem;
                if (personagemConvidado.Batalha.LiderGrupo != 0)
                {
                    await ctx.RespondAsync($"Você precisa sair do grupo atual para entrar em outro! {discordUserConvidado.Mention}.");
                    return;
                }

                UsuarioRPG.GetPersonagem(ctx, out UsuarioRPG usuario);
                PersonagemRPG personagem = usuario.Personagem;
                if (personagem.Batalha.LiderGrupo == 0 || personagem.Batalha.LiderGrupo != ctx.User.Id)
                {
                    await ctx.RespondAsync($"O grupo foi desfeito, não é possível entrar! {discordUserConvidado.Mention}.");
                    return;
                }
                if (personagem.Batalha.Jogadores.Count > 5)
                {

                    await ctx.RespondAsync($"A quantidade máxima do grupo foi alcançada, não é possível entrar! {discordUserConvidado.Mention}.");
                    return;
                }

                personagemConvidado.Batalha.LiderGrupo = usuario.Id;
                personagem.Batalha.Jogadores.Add(usuarioConvidado.Id);
                usuario.Salvar();
                usuarioConvidado.Salvar();
                await ctx.RespondAsync($"{discordUserConvidado.Mention} aceitou o convite do {ctx.User.Mention}!");
                return;
            }

            if (rec.Emoji.Equals(emojiNao))
            {
                await ctx.RespondAsync($"{ctx.User.Mention} o seu convite foi recusado por {discordUserConvidado.Mention}!");
            }
        }

        [Command("sair")]
        [Description("Permite sair do grupo atual.")]
        [UsoAtributo("sair")]
        [Cooldown(1, 60, CooldownBucketType.User)]
        public async Task GrupoSair(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.GetPersonagem(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;

            if (personagem.Batalha.LiderGrupo == 0)
            {
                await ctx.RespondAsync($"Você não está em nenhum grupo para sair! {ctx.User.Mention}.");
                return;
            }

            if (personagem.Batalha.LiderGrupo == ctx.User.Id && personagem.Batalha.Jogadores.Count >= 1)
            {
                await ctx.RespondAsync($"Você precisa remover os membros antes de sair do grupo! {ctx.User.Mention}.");
                return;
            }
            else if (personagem.Batalha.LiderGrupo == ctx.User.Id)
            {
                await ctx.RespondAsync($"Você saiu do grupo! {ctx.User.Mention}.");
                personagem.Batalha = new BatalhaRPG();
                usuario.Salvar();
                return;
            }

            if (personagem.Batalha.LiderGrupo != ctx.User.Id)
            {
                UsuarioRPG userLider = await UsuarioRPG.UsuarioGetAsync(personagem.Batalha.LiderGrupo);
                userLider.Personagem.Batalha.Jogadores.Remove(ctx.User.Id);
                userLider.Salvar();
                await ctx.RespondAsync($"Você saiu do grupo xxxx! {ctx.User.Mention}.");

                personagem.Batalha = new BatalhaRPG();
                usuario.Salvar();
                return;
            }
        }
    }
}
