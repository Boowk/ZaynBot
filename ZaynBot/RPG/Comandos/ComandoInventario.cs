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
    [Group("mochila", CanInvokeWithoutSubcommand = true)]
    [Description("Exibe o que tem dentro da mochila do seu personagem e a capacidade.\n\n" +
"Uso: z!mochila [página]\n\n" +
        "Exemplo: z!mochila 2")]
    [Aliases("m")]
    public class ComandoInventario
    {
        public async Task ExecuteGroupAsync(CommandContext ctx, int pagina = 0)
        {
            RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioComPersonagemAsync(ctx);
            RPGPersonagem personagem = usuario.Personagem;
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Inventário", ctx);
            DiscordEmoji mochila = DiscordEmoji.FromGuildEmote(ModuloCliente.Client, 590908712324038659);
            embed.WithTitle($"{mochila} {personagem.Inventario.PesoAtual}/{personagem.Inventario.PesoMaximo}");
            if (personagem.Inventario.PesoAtual == 0)
            {
                embed.WithDescription("Nem um farelo dentro.");
            }
            else
            {

            }
            embed.WithColor(DiscordColor.Purple);
            embed.WithFooter($"Página {pagina}");
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
