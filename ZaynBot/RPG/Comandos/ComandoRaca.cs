using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoRaca
    {
        [Command("raca")]
        public async Task ComandoRacaAb(CommandContext ctx, [Description("Nome."), RemainingText]string nome)
        {
            await ctx.TriggerTypingAsync();

            if (string.IsNullOrWhiteSpace(nome))
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, qual raça?");
                return;
            }

            string nomeMinusculo = nome.ToLower();
            RPGRaça raca = Banco.RacaConsultar(nomeMinusculo);
            if (raca != null)
            {
                DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
                embed.WithTitle($"Raça {raca.Nome}");
                embed.WithDescription(raca.Descricao);
                embed.AddField("Força", raca.Forca.ToString(), true);
                embed.AddField("Inteligência", raca.Inteligencia.ToString(), true);
                embed.AddField("Percepção", raca.Percepcao.ToString(), true);
                embed.AddField("Destreza", raca.Destreza.ToString(), true);
                embed.AddField("Constituição", raca.Constituicao.ToString(), true);
                embed.AddField("Sorte", raca.Sorte.ToString(), true);
                embed.WithColor(DiscordColor.Lilac);
                embed.Timestamp = DateTime.Now;
                embed.WithAuthor(ctx.User.Username, icon_url: ctx.User.AvatarUrl);
                await ctx.RespondAsync(embed: embed.Build());
            }
            else
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, raça não encontrada!");
            }
        }
    }
}
