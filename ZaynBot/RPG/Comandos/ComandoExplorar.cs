using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoExplorar : BaseCommandModule
    {
        [Command("explorar")]
        [Description("Procura por inimigos na região atual.")]
        [UsoAtributo("explorar")]
        [Cooldown(1, 3, CooldownBucketType.User)]
        public async Task ExplorarComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
            RPGPersonagem personagem = usuario.Personagem;


            if (personagem.VidaAtual <= 0)
            {
                await ctx.RespondAsync($"Você está morto {ctx.User.Mention}!");
                return;
            }
            if (personagem.Batalha.LiderGrupo == 0)
            {
                await ctx.RespondAsync($"Você deve criar um Grupo antes, {ctx.User.Mention}!");
                return;
            }
            if (personagem.Batalha.LiderGrupo != ctx.User.Id)
            {
                await ctx.RespondAsync($"Somente o lider do grupo pode usar este comando, {ctx.User.Mention}!");
                return;
            }
            if (personagem.Batalha.MobsVivos.Count > 0)
            {
                await ctx.RespondAsync($"Você precisa terminar a batalha atual antes de fazer isso, {ctx.User.Mention}!");
                return;
            }
            RPGRegiao localAtual = usuario.RegiaoGet();
            if (localAtual.Dificuldade == 0)
            {
                await ctx.RespondAsync($"Esta região não tem inimigos! {ctx.User.Mention}!");
                return;
            }

            int mobSorteado = AparecerMob(localAtual, personagem, ctx);
            RPGUsuario.Salvar(usuario);

            await new ComandoBatalha().BatalhaComandoAb(ctx);
            // await ctx.RespondAsync($"**<{mobSorteado}>** mobs apareceu! {ctx.User.Mention}.");
        }

        /// <summary>
        /// Sorteia 1 a 6 inimigos com base na dificuldade da zona
        /// </summary>
        /// <param name="localAtual"></param>
        /// <param name="personagem"></param>
        /// <param name="ctx"></param>
        /// <returns>Quantiadde de mobs sorteados</returns>
        public static int AparecerMob(RPGRegiao localAtual, RPGPersonagem personagem, CommandContext ctx)
        {
            List<RPGMob> mobs = ModuloBanco.MobsGet(localAtual.Dificuldade);
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
                RPGMob mobSorteado = mobs[posicaoEscolhida];

                int incr = 1;
                bool naoAdicionou = true;
                do
                {
                    try
                    {
                        personagem.Batalha.MobsVivos.Add($"{mobSorteado.Nome.ToLower()} {incr}", mobSorteado);
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

            return personagem.Batalha.MobsVivos.Count;
        }
    }
}
