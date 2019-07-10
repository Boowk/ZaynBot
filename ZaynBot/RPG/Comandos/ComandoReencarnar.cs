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
        bool _usuarioNull = true;

        [Command("reencarnar")]
        [Description("Reencarna em um novo persoonagem.\n\n" +
            "Uso: z!reencarnar")]
        [Cooldown(1, 120, CooldownBucketType.User)]
        public async Task Reencarnar(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario usuario = ModuloBanco.GetUsuario(ctx.User.Id);
            if (usuario != null)
            {
                _usuarioNull = false;
                if (usuario.Personagem.PontosDeVida > 0)
                {
                    await ctx.RespondAsync($"{ctx.User.Mention}, você precisa morrer antes de tentar reencarnar novamente!");
                    return;
                }
            }
            StringBuilder racas = new StringBuilder();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Reencarnação", ctx);
            embed.WithTitle("⌈Raças disponíveis⌋");
            embed.WithFooter("Clique no emoji para escolhar a sua raça. Se estiver com dúvidas, escreva z!raca [nome]");
            embed.WithColor(DiscordColor.Goldenrod);
            ListaEmojisSelecao emojis = new ListaEmojisSelecao(ctx);
            usuario = new RPGUsuario(ctx.User.Id);
            foreach (var item in usuario.RacasDisponiveisId)
            {
                racas.Append($"{emojis.ProxEmoji()} - {ModuloBanco.RacaConsultar(item).Nome}\n");
            }
            embed.WithDescription(racas.ToString());
            DiscordMessage mensagem = await ctx.RespondAsync($"{ctx.User.Mention}, você está prestes a entrar em um mundo de fantasia. Bem vindo ao Dragons & Zayn's RPG! " +
                $"Vamos começar escolhendo primeiro a sua raça, que será parte do seu personagem até o fim da sua jornada.", embed: embed.Build());
            emojis.ResetSelecao();
            _cts = new CancellationTokenSource();
            CancelamentoToken.AdicionarOuAtualizar(ctx.User.Id, _cts);
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
            DiscordGuild MundoZayn = await ModuloCliente.Client.GetGuildAsync(420044060720627712);
            DiscordChannel CanalRPG = MundoZayn.GetChannel(519176927265947689);
            await CanalRPG.SendMessageAsync($"*{ctx.User.Username} acabou de reencarnar como {racaEscolhida.Nome}.*");
            if (_usuarioNull == true)
            {
                ModuloBanco.UsuarioColecao.InsertOne(usuario);
            }
            else
                RPGUsuario.UpdateRPGUsuario(usuario);
            await ctx.RespondAsync($"{ctx.User.Mention}, você escolheu `{racaEscolhida.Nome}`.");
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Historia", ctx);
            embed.WithDescription(usuario.GetRPGRegiao().Descrição);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
