using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoViajar : BaseCommandModule
    {
        [Command("viajar")]
        [Description("Permite viajar para outra área.")]
        [ComoUsar("viajar [nome]")]
        [Exemplo("viajar para o espaço")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ViajarAbAsync(CommandContext ctx, [RemainingText] string area = "")
        {
            await ctx.TriggerTypingAsync();
            if (string.IsNullOrWhiteSpace(area))
            {
                await ctx.ExecutarComandoAsync("ajuda viajar");
                return;
            }
            area = area.ToLower();
            var jogador = ModuloBanco.GetJogador(ctx);

            if (ModuloBanco.TryGetRegiao(jogador, out var regiao))
            {
                if (regiao.Saidas.Contains(area))
                {
                    jogador.Batalha = new RPGBatalha();
                    jogador.RegiaoAtual = area;
                    jogador.Salvar();
                    await ctx.RespondAsync($"Você foi para [{area.FirstUpper()}] {ctx.User.Mention}!".Bold());
                }
                else
                    await ctx.RespondAsync($"Área {area.FirstUpper()} não encontrada {ctx.User.Mention}!");
            }
            else
                await ctx.RespondAsync($"Área ainda não adicionado no banco de dados, será adicionado em breve {ctx.User.Mention}!");
        }
    }
}
