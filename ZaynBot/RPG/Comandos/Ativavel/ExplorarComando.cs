using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
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
        [Cooldown(1, 10, CooldownBucketType.User)]
        [Description("Procura por inimigos na região atual.")]
        [UsoAtributo("explorar")]
        [Cooldown(1, 3, CooldownBucketType.User)]
        public async Task ExplorarComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.GetPersonagem(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;

            if (personagem.Batalha.LiderGrupoInimigo != 0)
            {
                await ctx.RespondAsync($"Termine a batalha contra outros jogadores antes! {ctx.User.Mention}.");
                return;
            }

            if (personagem.Batalha.LiderGrupo == 0)
            {
                await ctx.RespondAsync($"Você deve criar um Grupo antes! {ctx.User.Mention}.");
                return;
            }

            RegiaoRPG localAtual = usuario.RegiaoGet();

            if (localAtual.Dificuldade == 0)
            {
                await ctx.RespondAsync($"Esta região não tem inimigos! {ctx.User.Mention}.");
                return;
            }

            if (personagem.Batalha.Mobs.Count == 0)
            {
                int mobSorteado = AparecerMob(localAtual, personagem, ctx);

                await ctx.RespondAsync($"**<{mobSorteado}>** mobs apareceu! {ctx.User.Mention}.");
                UsuarioRPG.Salvar(usuario);
            }
            else
                await ctx.RespondAsync($"Você precisa terminar a batalha atual para fazer isso! {ctx.User.Mention}.");
        }

        /// <summary>
        /// Sorteia 1 a 6 inimigos com base na dificuldade da zona
        /// </summary>
        /// <param name="localAtual"></param>
        /// <param name="personagem"></param>
        /// <param name="ctx"></param>
        /// <returns>Quantiadde de mobs sorteados</returns>
        public static int AparecerMob(RegiaoRPG localAtual, PersonagemRPG personagem, CommandContext ctx)
        {
            List<MobRPG> mobs = ModuloBanco.MobsGet(localAtual.Dificuldade);
            int quantidadeMob = Sortear.Valor(1, 6);

            int somaPeso = 0;
            foreach (var item in mobs)
                somaPeso += item.ChanceDeAparecer;
            for (int i = 0; i < quantidadeMob; i++)
            {
                int sorteio = Sortear.Valor(0, somaPeso);
                int posicaoEscolhida = -1;
                do
                {
                    posicaoEscolhida++;
                    sorteio -= mobs[posicaoEscolhida].ChanceDeAparecer;
                } while (sorteio > 0);
                MobRPG mobSorteado = mobs[posicaoEscolhida];

                int incr = 1;
                bool naoAdicionou = true;
                do
                {
                    try
                    {
                        personagem.Batalha.Mobs.Add($"{mobSorteado.Nome} {incr}", mobSorteado);
                        naoAdicionou = false;
                    }
                    catch
                    {
                        incr++;
                        naoAdicionou = true;
                    }
                } while (naoAdicionou);
            }

            personagem.Batalha.Turno = 0;

            return personagem.Batalha.Mobs.Count;
        }
    }
}
