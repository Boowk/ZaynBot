﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Exibir
{
    public class BatalhaComando : BaseCommandModule
    {
        [Command("batalha")]
        [Description("Exibe o status da batalha atual.")]
        [UsoAtributo("batalha")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task BatalhaComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.GetPersonagem(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;
            BatalhaRPG batalha = new BatalhaRPG();

            //Caso não tenha grupo
            if (personagem.Batalha.LiderGrupo == 0)
            {
                await ctx.RespondAsync($"Você deve criar um Grupo antes! {ctx.User.Mention}.");
                return;
            }

            //Caso o lider do grupo não seja ele
            if (personagem.Batalha.LiderGrupo != ctx.User.Id)
            {
                UsuarioRPG liderUsuario = await UsuarioRPG.UsuarioGetAsync(personagem.Batalha.LiderGrupo);
                batalha = liderUsuario.Personagem.Batalha;
            }

            //Caso ele seja o lider
            if (personagem.Batalha.LiderGrupo == ctx.User.Id)
                batalha = personagem.Batalha;


            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Batalha", ctx);
            embed.WithColor(DiscordColor.PhthaloGreen);
            embed.WithTitle($"**{batalha.NomeGrupo}**".Titulo());
            DiscordUser liderUser = await ctx.Client.GetUserAsync(batalha.LiderGrupo);
            embed.WithDescription($"**Turno**: {batalha.Turno.ToString()}\n" +
                $"**Lider:** {liderUser.Mention}");


            if (batalha.Jogadores.Count != 0)
            {
                StringBuilder sr = new StringBuilder();
                foreach (var item in batalha.Jogadores)
                {
                    DiscordUser user = await ModuloCliente.Client.GetUserAsync(item);
                    UsuarioRPG jog = await UsuarioRPG.UsuarioGetAsync(item);
                    sr.AppendLine($"*{user.Mention} - Vida: {jog.Personagem.VidaAtual.Texto2Casas()}/{jog.Personagem.VidaMaxima.Texto2Casas()}*");
                }
                embed.AddField("Jogadores".Titulo(), sr.ToString(), true);
            }

            //Caso tenha mobs
            if (batalha.Mobs.Count != 0)
            {
                StringBuilder sr = new StringBuilder();
                foreach (var item in batalha.Mobs)
                    sr.AppendLine($"*{item.Key} - Vida: {item.Value.PontosDeVida}*");
                embed.AddField("**Mobs**".Titulo(), sr.ToString(), true);
            }

            ////Caso tenha jogadores inimigos
            //if (personagem.Batalha.LiderPartyInimiga != 0)
            //{

            //    return;
            //}

            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
