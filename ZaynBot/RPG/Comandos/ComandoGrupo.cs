using DSharpPlus.CommandsNext;
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
using static DSharpPlus.CommandsNext.CommandsNextExtension;

namespace ZaynBot.RPG.Comandos
{
    [Group("grupo")]
    [Description("Grupo de comandos relacionado ao grupo.")]
    [UsoAtributo("grupo [comando]")]

    public class ComandoGrupo : BaseCommandModule
    {
        [GroupCommand]
        public async Task GroupCommandAsync(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.ExecutarComandoAsync("ajuda grupo");
        }

        [Command("criar")]
        [Description("Permite criar um novo grupo.")]
        [UsoAtributo("grupo [nome]")]
        [Cooldown(1, 30, CooldownBucketType.User)]
        public async Task GrupoCriar(CommandContext ctx, [RemainingText] string nome = "")
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetPersonagem(ctx, out RPGUsuario usuario);
            RPGPersonagem personagem = usuario.Personagem;

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

        [Command("convidar")]
        [Description("Permite convidar um novo membro para o grupo. Note que precisam estar no mesmo servidor.")]
        [UsoAtributo("convidar [id|menção]")]
        [ExemploAtributo("convidar 53057768")]
        [ExemploAtributo("convidar @Usuario")]
        [Cooldown(1, 30, CooldownBucketType.User)]
        public async Task GrupoConvidar(CommandContext ctx, DiscordMember userConvidado)
        {
            await ctx.TriggerTypingAsync();

            if (userConvidado.Id == ctx.User.Id)
            {
                await ctx.RespondAsync($"Hahaha, muito engraçadinho você em!! {ctx.User.Mention}.");
                return;
            }

            RPGUsuario.GetPersonagem(ctx, out RPGUsuario usuario);
            RPGPersonagem personagem = usuario.Personagem;

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
            opcoes[0] = mensagem.WaitForReactionAsync(userConvidado, emojiSim, TimeSpan.FromSeconds(30))
                .ContinueWith(async x => await GetReacaoMissao(x.Result, userConvidado, token, ctx), token.Token);
            opcoes[1] = mensagem.WaitForReactionAsync(userConvidado, emojiNao, TimeSpan.FromSeconds(30))
                    .ContinueWith(async x => await GetReacaoMissao(x.Result, userConvidado, token, ctx), token.Token);

            await mensagem.CreateReactionAsync(emojiSim);
            await mensagem.CreateReactionAsync(emojiNao);

            await Task.WhenAny(opcoes);
        }

        DiscordEmoji emojiSim;
        DiscordEmoji emojiNao;
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
                RPGUsuario.UsuarioGet(discordUserConvidado, out RPGUsuario usuarioConvidado);
                RPGPersonagem personagemConvidado = usuarioConvidado.Personagem;
                if (personagemConvidado.Batalha.LiderGrupo != 0)
                {
                    await ctx.RespondAsync($"Você precisa sair do grupo atual para entrar em outro! {discordUserConvidado.Mention}.");
                    return;
                }

                RPGUsuario.GetPersonagem(ctx, out RPGUsuario usuario);
                RPGPersonagem personagem = usuario.Personagem;
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
                await ctx.RespondAsync($"{discordUserConvidado.Mention} aceitou o seu convite {ctx.User.Mention}!");
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
        [Cooldown(1, 30, CooldownBucketType.User)]
        public async Task GrupoSair(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetPersonagem(ctx, out RPGUsuario usuario);
            RPGPersonagem personagem = usuario.Personagem;

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

            if (personagem.Batalha.LiderGrupo == ctx.User.Id && personagem.Batalha.Mobs.Count >= 1)
            {
                await ctx.RespondAsync($"Você precisa terminar a batalha antes de sair do grupo! {ctx.User.Mention}.");
                return;
            }

            if (personagem.Batalha.LiderGrupo == ctx.User.Id)
            {
                await ctx.RespondAsync($"Você desfez o grupo! {ctx.User.Mention}.");
                personagem.Batalha = new RPGBatalha();
                usuario.Salvar();
                return;
            }

            if (personagem.Batalha.LiderGrupo != ctx.User.Id)
            {
                RPGUsuario userLider = await RPGUsuario.UsuarioGetAsync(personagem.Batalha.LiderGrupo);
                userLider.Personagem.Batalha.Jogadores.Remove(ctx.User.Id);
                userLider.Salvar();
                personagem.Batalha = new RPGBatalha();
                usuario.Salvar();
                await ctx.RespondAsync($"Você saiu do grupo **{userLider.Personagem.Batalha.NomeGrupo}**! {ctx.User.Mention}.");
                return;
            }
        }

        [Command("remover")]
        [Description("Permite remover membros do grupo atual.")]
        [UsoAtributo("remover")]
        [Cooldown(1, 30, CooldownBucketType.User)]
        public async Task GrupoRemover(CommandContext ctx, DiscordUser userRemovido)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetPersonagem(ctx, out RPGUsuario usuario);
            RPGPersonagem personagem = usuario.Personagem;


            if (userRemovido.Id == ctx.User.Id)
            {
                await ctx.RespondAsync($"Como se remove você mesmo? {ctx.User.Mention}.");
                return;
            }

            if (personagem.Batalha.LiderGrupo == 0)
            {
                await ctx.RespondAsync($"Você não está em nenhum grupo para remover alguém! {ctx.User.Mention}.");
                return;
            }

            if (personagem.Batalha.LiderGrupo != ctx.User.Id)
            {
                await ctx.RespondAsync($"Você não é o lider do grupo para remover alguém! {ctx.User.Mention}.");
                return;
            }

            RPGUsuario userJogadorRemovido = await RPGUsuario.UsuarioGetAsync(userRemovido.Id);
            if (userJogadorRemovido.Personagem.Batalha.LiderGrupo != ctx.User.Id)
            {
                await ctx.RespondAsync($"Não está no mesmo grupo que você! {ctx.User.Mention}.");
                return;
            }

            userJogadorRemovido.Personagem.Batalha = new RPGBatalha();
            userJogadorRemovido.Salvar();
            personagem.Batalha.Jogadores.Remove(userJogadorRemovido.Id);
            usuario.Salvar();
            await ctx.RespondAsync($"Você removeu {userRemovido.Mention} do grupo! {ctx.User.Mention}.");
        }
    }
}
