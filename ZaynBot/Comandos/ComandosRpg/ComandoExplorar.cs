using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Entidades;
using ZaynBot.Entidades.EntidadesRpg;

namespace ZaynBot.Comandos.ComandosRpg
{
    public class ComandoExplorar
    {
        [Command("explorar")]
        [Aliases("ex")]
        [Description("Explora a região para encontrar inimigos.")]
        [Cooldown(max_uses: 1, reset: 6, bucket_type: CooldownBucketType.User)]
        public async Task ExplorarInimigos(CommandContext ctx) // [Description("norte,sul,oeste,leste")] string direcao = "nenhuma")
        {
            Usuario usuario = Banco.ConsultarUsuario(ctx.User.Id);
            Personagem personagem = usuario.Personagem;

            if (personagem.LocalAtual.Inimigos.Count == 0)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, parece que está região não tem nenhum inimigo.");
                return;
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            string inimigoMensagem = "";

            if (personagem.CampoBatalha.Inimigos.Count <= 2)
            {
                //List<Mob> pesos = personagem.LocalAtual.Inimigos;

                //int somaPeso = 0;
                //for (int i = 0; i < pesos.Count; i++)
                //{
                //    somaPeso += pesos[i].ChanceDeAparecer;
                //}

                //Random r = new Random();
                //int sorteio = r.Next(0, somaPeso);
                //int posicaoEscolhida = -1;
                //do
                //{
                //    posicaoEscolhida++;
                //    sorteio -= pesos[posicaoEscolhida].ChanceDeAparecer;
                //} while (sorteio > 0);              

                //int ultimoId = 0;
                //if (personagem.Inimigos.Count == 0)
                //{
                //    ultimoId = 0;
                //}
                //else
                //{
                //    ultimoId = 1 + (personagem.Inimigos[personagem.Inimigos.Count - 1].Id);
                //}

                //inimigo.Id = ultimoId;
                //personagem.Inimigos.Add(inimigo);


                //inimigoMensagem = $"{inimigo.Nome}(ID {inimigo.Id}) apareceu na sua frente.";
            }
            else
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, mate os inimigos que você já tem.");
                return;
            }

            //embed.WithTitle($"Exploração do {ctx.User.Username}");
            //embed.AddField("Inimigos encontrados", InimigosApareceu.ToString());
            //embed.WithColor(DiscordColor.Red);

            await ctx.RespondAsync(ctx.User.Mention + ", " + inimigoMensagem);
        }
    }
}
