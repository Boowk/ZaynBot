using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoMissao
    {
        [Command("missao")]
        [Description("Mostra a missão atual.\n\n" +
            "Uso: z!missao")]
        public async Task ComandoMissaoAb(CommandContext ctx)
        {
            RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioBaseAsync(ctx);
            RPGPersonagem personagem = usuario.Personagem;
            if (personagem.MissaoEmAndamento == null)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você não tem nenhuma missão ativa!");
                return;
            }
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Missão", ctx);
            embed.WithTitle(personagem.MissaoEmAndamento.Nome.Titulo());
            embed.WithDescription(personagem.MissaoEmAndamento.Descricao);
            embed.WithColor(DiscordColor.HotPink);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
