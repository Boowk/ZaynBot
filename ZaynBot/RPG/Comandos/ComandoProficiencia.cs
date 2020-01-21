﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoProficiencia : BaseCommandModule
    {
        [Command("proficiencia")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        [Description("Permite exibir todas as proficiencia do seu personagem.")]
        [ComoUsar("proficiencia")]
        public async Task HabilidadeComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var jogador = ModuloBanco.GetJogador(ctx);
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Proficiencia", ctx);
            embed.WithDescription($"Pontos disponíveis: {jogador.ProficienciaPontos}".Bold());
            foreach (var item in jogador.Proficiencias)
                embed.AddField(item.Value.Nome.ToString().Titulo().Bold(), $"{item.Value.Pontos}".Bold(), true);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}

