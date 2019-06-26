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
    [Description("Exibe inventário do seu personagem. E executa outros comandos relacionados.\n\n" +
"Exemplo: z!inventario")]
    [Aliases("inv")]
    public class ComandosInventario
    {
        public async Task ExecuteGroupAsync(CommandContext ctx)
        {
            RPGUsuario usuario = await ModuloBanco.UsuarioConsultarPersonagemAsync(ctx);
            if (usuario.Personagem == null) return;
            RPGPersonagem personagem = usuario.Personagem;
            RPGEmbed embed = new RPGEmbed(ctx, "Inventário do");
            //embed.Titulo(npc.Nome);
            embed.Embed.WithColor(DiscordColor.Purple);
            await ctx.RespondAsync(embed: embed.Build());
        }

        //[Command("raca")]
        //[Aliases("r")]
        //public async Task ComandoPersonagemRaca(CommandContext ctx)
        //{
        //}
    }
}
