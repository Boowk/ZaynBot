using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoMinerar : BaseCommandModule
    {
        [Command("minerar")]
        [Description("Permite minerar rochas por recursos.")]
        [ComoUsar("minerar")]
        [Cooldown(1, 300, CooldownBucketType.User)]
        public async Task MinerarAbAsync(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var jogador = ModuloBanco.GetJogador(ctx);

            jogador.Proficiencias.TryGetValue(EnumProficiencia.Minerar, out var proff);
            ProficienciaMinerar minerar = proff as ProficienciaMinerar;
            int quantidade = minerar.CalcMinerioExtra();
            jogador.Equipamentos.TryGetValue(EnumTipo.Picareta, out var picareta);
            string minerio = "";
            switch (picareta)
            {
                case null:
                    minerio = jogador.Mochila.AdicionarItem("pedra", quantidade);
                    break;
            }
            jogador.Salvar();
            await ctx.RespondAsync($"Você minerou {quantidade} [{minerio.FirstUpper()}] {ctx.User.Mention}!");
        }
    }
}