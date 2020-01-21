using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoCortar : BaseCommandModule
    {
        [Command("cortar")]
        [Description("Permite cortar arvores por recursos.")]
        [ComoUsar("cortar")]
        [Cooldown(1, 300, CooldownBucketType.User)]
        public async Task CortarAbAsync(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var jogador = ModuloBanco.GetJogador(ctx);

            jogador.Proficiencias.TryGetValue(EnumProficiencia.Cortar, out var proff);
            ProficienciaCortar cortar = proff as ProficienciaCortar;
            int quantidade = cortar.CalcMadeiraExtra();
            jogador.Equipamentos.TryGetValue(EnumTipo.Machado, out var machado);
            string madeira = "";
            switch (machado)
            {
                case null:
                    madeira = jogador.Mochila.AdicionarItem("galho", quantidade);
                    break;
            }
            jogador.Salvar();
            await ctx.RespondAsync($"Você cortou {quantidade} [{madeira.FirstUpper()}] {ctx.User.Mention}!".Bold());
        }
    }
}
