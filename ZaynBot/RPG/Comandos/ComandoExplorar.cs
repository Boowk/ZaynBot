using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using RandomNameGeneratorLibrary;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoExplorar : BaseCommandModule
    {
        [Command("explorar")]
        [Aliases("ex")]
        [Description("Permite encontrar criaturas para atacar.")]
        [ComoUsar("explorar")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoExplorarAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var jogador = ModuloBanco.GetJogador(ctx);
            var vidaSorteada = Sortear.Valor(2, 4);
            var danoSoorteado = Sortear.Valor(5, 8);
            var personGenerator = new PersonNameGenerator();
            RPGMob mob = new RPGMob((jogador.AtaqueFisicoBase + jogador.AtaqueFisicoExtra) * vidaSorteada)
            {
                Nome = personGenerator.GenerateRandomFirstName(),
                AtaqueFisico = jogador.VidaMaxima / danoSoorteado,
            };
            jogador.Batalha = new RPGBatalha(mob);
            jogador.Salvar();
            await ctx.RespondAsync($"{ctx.User.Mention} você explorou! Encontrou: < {mob.Nome.Underline()} >!".Bold());
        }
    }
}
