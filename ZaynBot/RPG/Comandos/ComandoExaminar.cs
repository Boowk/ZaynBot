using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Data.Itens;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoExaminar
    {
        [Command("examinar")]
        [Description("Mostra uma breve descrição do item.\n\n" +
           "Exemplo: z!examinar [nome item]\n\n" +
            "Uso: z!examinar espada")]
        public async Task ComandoExaminarAb(CommandContext ctx, [RemainingText] string nome)
        {
            RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioBaseAsync(ctx);
            RPGPersonagem personagem = usuario.Personagem;
            if (string.IsNullOrWhiteSpace(nome))
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você precisa informar um nome válido.");
                return;
            }
            RPGItem item = Itens.GetItem(nome);
            if (item == null)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, esse item é muito antigo, impossível examina-lo!");
                return;
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Item", ctx);
            embed.WithTitle(item.Nome.PrimeiraLetraMaiuscula().Titulo());
            embed.WithDescription(item.Descricao);
            embed.WithColor(DiscordColor.Green);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
