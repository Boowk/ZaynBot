﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoInimigos
    {
        [Command("inimigos")]
        [Aliases("ins")]
        [Description("Veja todos os inimigos a sua volta.")]
        public async Task VerInimigos(CommandContext ctx)
        {
            RPGUsuario usuario = await Banco.ConsultarUsuarioPersonagemAsync(ctx);
            if (usuario.Personagem == null) return;
            RPGPersonagem personagem = usuario.Personagem;

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();

            if (personagem.CampoBatalha.Inimigos.Count == 0)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, parece que você não tem nenhum inimigo. :P");
                return;
            }
            StringBuilder inimigos = new StringBuilder();
            int quant = 0;
            foreach (var item in personagem.CampoBatalha.Inimigos)
            {
                inimigos.Append($"{item.Nome}(ID {quant}) [Vida: {string.Format("{0:N2}", item.PontosDeVida)}|{string.Format("{0:N2}", item.PontosDeVidaMaxima)}]\n");
                quant++;
            }

            embed.WithTitle($"Inimigos do {ctx.User.Username}");
            embed.WithDescription($"{quant} ainda vivos.");
            embed.AddField("Inimigos", inimigos.ToString());
            embed.WithColor(DiscordColor.Red);
            await ctx.RespondAsync(ctx.User.Mention, embed: embed.Build());
        }
    }
}