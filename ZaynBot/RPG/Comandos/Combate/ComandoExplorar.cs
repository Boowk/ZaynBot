using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.RPG.Comandos.Combate
{
    public class ComandoExplorar
    {
        [Command("explorar")]
        [Aliases("ex")]
        [Description("Explora a região para encontrar inimigos.")]
        public async Task ComandoExplorarAb(CommandContext ctx)
        {
            RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioBaseAsync(ctx);
            RPGPersonagem personagem = usuario.Personagem;
            RPGRegiao localAtual = usuario.GetRPGRegiao();

            if (localAtual.Mobs.Count == 0)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você procura, mas não encontra ninguém.");
                return;
            }
            if (personagem.Batalha == null)
                personagem.Batalha = new RPGBatalha();
            if (personagem.Batalha.Inimigos.Count == 0)
            {
                List<RPGMob> pesos = localAtual.Mobs;

                int somaPeso = 0;
                for (int i = 0; i < pesos.Count; i++)
                {
                    somaPeso += pesos[i].ChanceDeAparecer;
                }

                Random r = new Random();
                int sorteio = r.Next(0, somaPeso);
                int posicaoEscolhida = -1;
                do
                {
                    posicaoEscolhida++;
                    sorteio -= pesos[posicaoEscolhida].ChanceDeAparecer;
                } while (sorteio > 0);
                RPGMob inimigo = pesos[posicaoEscolhida];

                personagem.Batalha.Inimigos.Add(inimigo);

                RPGUsuario.UpdateRPGUsuario(usuario);
                await ctx.RespondAsync($"{ctx.User.Mention}, {inimigo.Nome} apareceu!");
            }
            else
                await ctx.RespondAsync($"{ctx.User.Mention}, mate os inimigos que você já tem.");

            //embed.WithTitle($"Exploração do {ctx.User.Username}");
            //embed.AddField("Inimigos encontrados", InimigosApareceu.ToString());
            //embed.WithColor(DiscordColor.Red);

            //await ctx.RespondAsync(ctx.User.Mention + ", " + inimigoMensagem);
        }
    }
}
