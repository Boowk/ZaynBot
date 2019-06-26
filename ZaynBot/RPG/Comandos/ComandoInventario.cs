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
            RPGUsuario usuario = await ModuloBanco.UsuarioConsultarPersonagemAsync(ctx);
            if (usuario.Personagem == null) return;
            RPGPersonagem personagem = usuario.Personagem;
            RPGEmbed embed = new RPGEmbed(ctx, "Inventário do");
            DiscordEmoji mochila = DiscordEmoji.FromGuildEmote(ModuloCliente.Client, 590908712324038659);
            embed.Embed.WithTitle($"{mochila} {personagem.Inventario.PesoAtual}/{personagem.Inventario.PesoMaximo}");
            if (personagem.Inventario.PesoAtual == 0)
            {
                embed.Embed.WithDescription("Nem um farelo dentro.");
            }
            else
            {

            }
            embed.Embed.WithColor(DiscordColor.Purple);
            embed.Embed.WithFooter($"Página {pagina}");
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
