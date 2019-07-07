using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Combate
{
    public class ComandoHabilidades
    {
        [Command("habilidades")]
        [Aliases("hab")]
        [Description("Exibe todas as suas habilidades.\n\n" +
            "Uso: z!habilidades")]
        public async Task ComandoHabilidadesAb(CommandContext ctx)
        {
            RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioBaseAsync(ctx);
            RPGPersonagem personagem = usuario.Personagem;

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao();
            StringBuilder str = new StringBuilder();
            foreach (var item in personagem.Habilidades)
            {
                str.Append($"`{item.Value.Nome.PrimeiraLetraMaiuscula()}`, ");
            }
            embed.AddField("Habilidades".Titulo(), str.ToString());
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
