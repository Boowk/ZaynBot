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
            RPGUsuario usuario = await ModuloBanco.UsuarioConsultarPersonagemAsync(ctx);
            if (usuario.Personagem == null) return;
            RPGPersonagem personagem = usuario.Personagem;

            if (personagem.MissaoEmAndamento == null)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você não tem nenhuma missão ativa!");
                return;
            }

            RPGEmbed embed = new RPGEmbed(ctx, "Missão do");
            embed.Titulo(personagem.MissaoEmAndamento.Nome);
            embed.Embed.WithDescription(personagem.MissaoEmAndamento.Descricao);
            embed.Embed.WithColor(DiscordColor.HotPink);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
