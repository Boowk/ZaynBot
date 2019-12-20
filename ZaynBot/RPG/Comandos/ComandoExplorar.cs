using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoExplorar : BaseCommandModule
    {
        [Command("explorar")]
        [Aliases("ex")]
        [Description("Explora mais detalhadamente a região, encontrando novos inimigos, mais minérios e mais arvores")]
        [ComoUsar("explorar")]
        [Cooldown(1, 60, CooldownBucketType.User)]
        public async Task ComandoExplorarAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);

            RPGRegiao regiaoAtual = ModuloBanco.GetRegiaoData(usuario.Personagem.RegiaoAtualId);
            usuario.Personagem.Batalha.Mob = ModuloBanco.GetMob(regiaoAtual);
            usuario.Personagem.Batalha.Turno = 0;
            RPGUsuario.Salvar(usuario);
            await ctx.RespondAsync($"{ctx.User.Mention} você explorou `{regiaoAtual.Nome}`!".Bold());
        }
    }
}
