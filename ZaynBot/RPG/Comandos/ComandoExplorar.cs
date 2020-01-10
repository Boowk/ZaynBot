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
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoExplorarAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
            RPGRegiao regiaoAtual = ModuloBanco.GetRegiaoData(usuario.Personagem.RegiaoAtualId);
            if (regiaoAtual.Dificuldade == 0)
            {
                await ctx.RespondAsync($"Esta região não tem creaturas {ctx.User.Mention}!");
                return;
            }
            //Pegamos um mob aleatorio baseado no nível da região
            usuario.Personagem.Batalha.Mob = ModuloBanco.GetMob(regiaoAtual);
            usuario.Personagem.Batalha.Turno = 0;
            //Sorteamos o item que o mob deixaria cair, caso fosse morto.
            MobItemDropRPG dropSorteado = usuario.Personagem.Batalha.Mob.SortearDrop();
            int quantidade = Sortear.Valor(1, dropSorteado.QuantMax);
            //Salvamos o item sorteado
            usuario.Personagem.Batalha.Mob.Item = new MobItemSorteadoRPG();
            usuario.Personagem.Batalha.Mob.Item.ItemID = dropSorteado.ItemId;
            usuario.Personagem.Batalha.Mob.Item.QuantidadeRestante = quantidade;

            RPGUsuario.Salvar(usuario);
            await ctx.RespondAsync($"{ctx.User.Mention} você explorou `{regiaoAtual.Nome}`! Encontrou: < {usuario.Personagem.Batalha.Mob.Nome.Underline()} >!".Bold());
        }
    }
}
