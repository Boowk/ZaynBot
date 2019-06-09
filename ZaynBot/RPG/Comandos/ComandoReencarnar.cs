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
        [Description("Reencarnar criando um persoonagem novo.")]
        [Cooldown(1, 120, CooldownBucketType.User)]
        public async Task Reencarnar(CommandContext ctx)
        {
            RPGUsuario usuario = await Banco.ConsultarUsuarioAsync(ctx);
            if (usuario.Personagem != null)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você precisa morrer antes de tentar reencarnar novamente!");
                return;
            }

            await ctx.RespondAsync($"{ctx.User.Mention}, bem-vindo ao Dragons & Zayn's RPG! Prepare-se para uma grande aventura. Mas antes, primeiro, você precisa escolher a sua raça " +
                "que será parte do seu personagem até a sua próxima jornada. Se você não tiver muitas raças para escolher, " +                "não desanime, você ira desbloquear mais enquanto avança.");
            await Task.Delay(1500);
            await ctx.RespondAsync($"Por favor, escolha a raça que você gostaria de escolher, {ctx.User.Mention}.");
            await Task.Delay(1500);
            StringBuilder racas = new StringBuilder();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithAuthor($"Reencarnação do {ctx.User.Username}", icon_url: ctx.User.AvatarUrl);
            embed.WithTitle("**⌈Raças⌋**");
            embed.WithFooter("Clique no emoji para escolhar a sua raça. Se tiver dúvidas, escreva z!raca <nome>");
            embed.WithColor(DiscordColor.Goldenrod);
            ListaEmojis emojis = new ListaEmojis(ctx);
            foreach (var item in usuario.RacasDisponiveisId)
            {
                racas.Append($"{emojis.ProxEmoji()} - {Banco.ConsultarRaca(item).Nome}\n");
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
                        .ContinueWith(x => GetRacaEscolhido(Banco.ConsultarRaca(item), x.Result, usuario, ctx), _cts.Token);
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
            if (reacao == null)
            {
                _cts.Cancel();
                return;
            }
            ListaEmojis emojis = new ListaEmojis();
            usuario.Personagem = new RPGPersonagem(racaEscolhida);
            Banco.AlterarUsuario(usuario);
            await ctx.RespondAsync($"Raça escolhida: {racaEscolhida.Nome}");
            await Task.Delay(1500);
            await ctx.RespondAsync($"Parabéns {ctx.User.Mention}, você acabou de completar a criação do seu persoangem!\n" +
                $"A ajuda pode ser encontrada digitando z!ajuda, que lhe dirá os comandos do bot.\n" +
                $"Também temos um sistema de arquivos(wiki) que você pode encontrar escrevendo z!convite\n" +
                $"Não se esqueça de pedir ajuda no nosso servidor caso esteja preso em alguma área.\n" +
                $"Divirta-se e aproveite o seu tempo aqui!!");
            await Task.Delay(4000);
            RPGEmbed embed = new RPGEmbed(ctx, "Historia do");
            embed.Embed.WithDescription("Você está dentro de uma casa pegando fogo.\n" +
                "Pessoas: - 'Socorro!!'\n" +
                "Você escuta pessoas pedindo por socorro no quarto ao lado.\n" +
                "Você tenta chegar onde está vindo as vozes.\n" +
                "Ao chegar no quarto, você não encontra ninguém.");
            await ctx.RespondAsync(embed: embed.Build());
            await Task.Delay(4000);
            embed = new RPGEmbed(ctx, "Historia do");
            embed.Embed.WithDescription("Você percebe que tem uma caixa mágica no meio do quarto.\n" +
                "Mas quando tenta se aproximar, uma viga de madeira pegando fogo cai em você.\n" +
                "Você morreu.");
            await ctx.RespondAsync(embed: embed.Build());
            await Task.Delay(3500);
            embed = new RPGEmbed(ctx, "Historia do");
            embed.Embed.WithDescription(Banco.ConsultarRegions(usuario.Personagem.LocalAtualId).Descrição);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
