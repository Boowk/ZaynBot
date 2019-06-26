using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ZaynBot.RPG.Data.Raças;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoReencarnar
    {
        CancellationTokenSource _cts;

        [Command("reencarnar")]
        [Description("Reencarnar nascendo um novo persoonagem.\n\n" +
            "Uso: z!reencarnar")]
        [Cooldown(1, 120, CooldownBucketType.User)]
        public async Task Reencarnar(CommandContext ctx)
        {
            RPGUsuario usuario = await ModuloBanco.UsuarioConsultarAsync(ctx);
            if (usuario.Personagem != null)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você precisa morrer antes de tentar reencarnar novamente!");
                return;
            }
            await ctx.RespondAsync($"{ctx.User.Mention}, bem-vindo ao Dragons & Zayn's RPG! Prepare-se para uma grande aventura. Mas antes, primeiro, você precisa escolher a sua raça " +
                "que será parte do seu personagem até a sua próxima jornada. Se você não tiver muitas raças para escolher, " +
                "não desanime, você ira desbloquear mais enquanto avança.");
            await ctx.TriggerTypingAsync();
            await Task.Delay(1500);
            await ctx.RespondAsync($"Por favor, escolha a raça que você gostaria de escolher, {ctx.User.Mention}.");
            await ctx.TriggerTypingAsync();
            await Task.Delay(1500);
            StringBuilder racas = new StringBuilder();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithAuthor($"Reencarnação do {ctx.User.Username}", icon_url: ctx.User.AvatarUrl);
            embed.WithTitle("**⌈Raças⌋**");
            embed.WithFooter("Clique no emoji para escolhar a sua raça. Se estiver com dúvidas, escreva z!raca <nome>");
            embed.WithColor(DiscordColor.Goldenrod);
            ListaEmojisSelecao emojis = new ListaEmojisSelecao(ctx);
            foreach (var item in usuario.RacasDisponiveisId)
            {
                racas.Append($"{emojis.ProxEmoji()} - {ModuloBanco.RacaConsultar(item).Nome}\n");
            }
            embed.WithDescription(racas.ToString());
            DiscordMessage mensagem = await ctx.RespondAsync(embed: embed.Build());
            emojis.ResetSelecao();
            _cts = new CancellationTokenSource();
            var interacao = ctx.Client.GetInteractivityModule();
            Task[] opcoes;
            try
            {
                opcoes = new Task[usuario.RacasDisponiveisId.Count];
                int index = 0;
                foreach (var item in usuario.RacasDisponiveisId)
                {
                    DiscordEmoji emoji = emojis.ProxEmoji();
                    await mensagem.CreateReactionAsync(emoji);
                    Func<DiscordEmoji, bool> emojiFun = x => x.Equals(emoji);
                    opcoes[index] = interacao.WaitForMessageReactionAsync(emojiFun, mensagem, ctx.User, TimeSpan.FromSeconds(60))
                        .ContinueWith(x => GetRacaEscolhido(ModuloBanco.RacaConsultar(item), x.Result, usuario, ctx), _cts.Token);
                    index++;
                }
            }
            catch
            {
                await ctx.RespondAsync("Não tenho permissão para adicionar emojis :(");
                return;
            }
            await Task.WhenAny(opcoes);
        }

        public async Task GetRacaEscolhido(RPGRaça racaEscolhida, ReactionContext reacao, RPGUsuario usuario, CommandContext ctx)
        {
            _cts.Cancel();
            if (reacao == null)
                return;
            usuario.Personagem = new RPGPersonagem(racaEscolhida);
            ModuloBanco.UsuarioAlterar(usuario);
            await ctx.RespondAsync($"Raça escolhida: {racaEscolhida.Nome}");
            await ctx.TriggerTypingAsync();
            await Task.Delay(1500);
            await ctx.RespondAsync($"Parabéns {ctx.User.Mention}, você acabou de completar a criação do seu persoangem!\n" +
                $"A ajuda pode ser encontrada digitando z!ajuda, que lhe dirá os comandos do bot.\n" +
                $"Também temos um sistema de arquivos(wiki) que você pode encontrar escrevendo z!convite\n" +
                $"Não se esqueça de pedir ajuda no nosso servidor caso esteja preso em alguma área.\n" +
                $"Divirta-se e aproveite o seu tempo aqui!!");
            await ctx.TriggerTypingAsync();
            await Task.Delay(5000);
            RPGEmbed embed = new RPGEmbed(ctx, "Historia do");
            embed.Embed.WithDescription("Você está dentro de uma casa pegando fogo.\n" +
                "Vozes falando: 'Ajuda!!'\n" +
                "Você escuta pessoas pedindo por ajuda no quarto ao lado.\n" +
                "Você vai até o quarto.\n" +
                "Ao chegar no quarto, você não encontra ninguém.");
            await ctx.RespondAsync(embed: embed.Build());
            await ctx.TriggerTypingAsync();
            await Task.Delay(5000);
            embed = new RPGEmbed(ctx, "Historia do");
            embed.Embed.WithDescription("Dentro do quarto, você percebe uma caixa mágica no centro.\n" +
                "Mas quando tenta se aproximar, uma viga de madeira pegando fogo cai em você.\n");
            await ctx.RespondAsync(embed: embed.Build());
            await ctx.TriggerTypingAsync();
            await Task.Delay(5000);
            embed = new RPGEmbed(ctx, "Historia do");
            embed.Embed.WithDescription(ModuloBanco.RegiaoConsultar(usuario.Personagem.LocalAtualId).Descrição);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
