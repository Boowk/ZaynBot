using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.RPG.Comandos.Ativavel
{
    public class ExplorarComando : BaseCommandModule
    {
        [Command("explorar")]
        [Aliases("ex")]
        [Cooldown(1, 2, CooldownBucketType.User)]
        [Description("Procura por inimigos na região atual.")]
        [UsoAtributo("explorar")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task ExplorarComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.TryGetPersonagemRPG(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;
            RegiaoRPG localAtual = usuario.RegiaoGet();

            if (localAtual.Mobs.Count == 0)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você procura mas, parece que esta região não tem inimigos.");
                return;
            }

            if (personagem.Batalha.Inimigos.Count == 0)
            {
                List<MobRPG> pesos = localAtual.Mobs;

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
                MobRPG inimigo = pesos[posicaoEscolhida];

                personagem.Batalha.Inimigos.Add(inimigo);

                UsuarioRPG.Salvar(usuario);
                await ctx.RespondAsync($"**<{inimigo.Nome}>** apareceu! {ctx.User.Mention}.");
            }
            else
                await ctx.RespondAsync($"{ctx.User.Mention}, mate os inimigos que você já tem.");
        }
    }
}
