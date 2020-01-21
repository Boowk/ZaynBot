using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoAtribuir : BaseCommandModule
    {
        [Command("atribuir")]
        [Description("Permite atribuir pontos na proficiência desejada.")]
        [ComoUsar("atribuir [+quantidade] [proficiência]")]
        [Exemplo("atribuir 1 força")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task ComandoAtribuirAb(CommandContext ctx, int quantidade, [RemainingText] string proficienciaText = "")
        {
            await ctx.TriggerTypingAsync();

            if (string.IsNullOrEmpty(proficienciaText))
            {
                await ctx.ExecutarComandoAsync("ajuda atribuir");
                return;
            }

            if (quantidade < 0)
            {
                await ctx.ExecutarComandoAsync("ajuda atribuir");
                return;
            }

            var jogador = ModuloBanco.GetJogador(ctx.User.Id);
            if (jogador.ProficienciaPontos == 0 || jogador.ProficienciaPontos < quantidade)
                await ctx.RespondAsync($"Você não tem {quantidade} ponto(s) para estar atribuindo {ctx.User.Mention}!".Bold());
            else
            {
                if (jogador.TryGetProficiencia(proficienciaText, out RPGProficiencia proff))
                {
                    proff.Pontos += quantidade;
                    jogador.ProficienciaPontos -= quantidade;
                    jogador.Salvar();
                    await ctx.RespondAsync($"{quantidade} ponto(s) atribuido em {proff.Nome} {ctx.User.Mention}!".Bold());
                }
                else
                {
                    await ctx.RespondAsync($"Proficiência não encontrada {ctx.User.Mention}!".Bold());
                }
            }
        }

        [Command("atribuir")]
        [ComoUsar("atribuir [proficiência]")]
        [Exemplo("atribuir força")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task ComandoAtribuirAb(CommandContext ctx, [RemainingText] string proficienciaText = "")
            => await ComandoAtribuirAb(ctx, 1, proficienciaText);
    }
}
