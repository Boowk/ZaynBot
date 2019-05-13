﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZaynBot.Entidades;
using ZaynBot.Entidades.EntidadesRpg;
using ZaynBot.Entidades.EntidadesRpg.Mapa;
using ZaynBot.Funções;

namespace ZaynBot.Comandos.ComandosRpg
{
    public class ComandoExplorar
    {
        [Command("explorar")]
        [Aliases("ex")]
        [Description("Explora a região para encontrar inimigos.")]
        public async Task ExplorarInimigos(CommandContext ctx) // [Description("norte,sul,oeste,leste")] string direcao = "nenhuma")
        {
            Usuario usuario = Banco.ConsultarUsuario(ctx.User.Id);
            Personagem personagem = usuario.Personagem;

            if (personagem.CampoBatalha.Party == true)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, somente o lider da part pode usar esse comando!");
                return;
            }
            Região localAtual = Banco.ConsultarRegions(personagem.LocalAtualId);

            if (localAtual.Inimigos.Count == 0)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, parece que está região não tem nenhum inimigo.");
                return;
            }

            if (personagem.CampoBatalha.Inimigos.Count < 2)
            {
                Sortear sortear = new Sortear();
                Mob inimigo = sortear.ListaMob(localAtual.Inimigos);

                personagem.CampoBatalha.Inimigos.Add(inimigo);

                string inimigoMensagem = $"{inimigo.Nome} com {inimigo.PontosDeVidaMaxima.Texto()} de vida, apareceu na sua frente!";
                Banco.AlterarUsuario(usuario);
                await ctx.RespondAsync(ctx.User.Mention + ", " + inimigoMensagem);
            }
            else
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, mate os inimigos que você já tem.");
                return;
            }

            //embed.WithTitle($"Exploração do {ctx.User.Username}");
            //embed.AddField("Inimigos encontrados", InimigosApareceu.ToString());
            //embed.WithColor(DiscordColor.Red);

            //await ctx.RespondAsync(ctx.User.Mention + ", " + inimigoMensagem);
        }
    }
}
