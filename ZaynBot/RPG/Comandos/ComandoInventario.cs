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
    [Group("inventario", CanInvokeWithoutSubcommand = true)]
    [Description("Exibe inventário do seu personagem e a capacidade. \n\n" +
"Uso: z!inventario [página]\n\n" +
        "Exemplo: z!inventario 2")]
    [Aliases("inv")]
    public class ComandoInventario
    {
        public async Task ExecuteGroupAsync(CommandContext ctx, int pagina = 0)
        {
            RPGUsuario usuario = await ModuloBanco.UsuarioConsultarPersonagemAsync(ctx);
            if (usuario.Personagem == null) return;
            RPGPersonagem personagem = usuario.Personagem;
            RPGEmbed embed = new RPGEmbed(ctx, "Inventário do");
            embed.Embed.WithTitle($"Peso {personagem.Inventario.PesoAtual}/{personagem.Inventario.PesoMaximo}");
            embed.Embed.WithColor(DiscordColor.Purple);
            embed.Embed.WithFooter($"Página {pagina}");
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
