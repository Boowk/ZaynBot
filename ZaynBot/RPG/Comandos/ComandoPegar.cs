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
    public class ComandoPegar
    {
        [Command("pegar")]
        [Description("Permite pegar um objeto do local atual.\n\n" +
            "Uso: z!pegar [nome]\n\n" +
            "Exemplo: z!pegar espada")]
        public async Task VerInimigos(CommandContext ctx)
        {
            RPGUsuario usuario = await ModuloBanco.UsuarioConsultarPersonagemAsync(ctx);
            if (usuario.Personagem == null) return;
            RPGPersonagem personagem = usuario.Personagem;

            //DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            //embed.WithAuthor($"Ação do {ctx.User.Username}", icon_url: ctx.User.AvatarUrl);
            //embed.WithFooter("Se estiver com dúvidas, escreva z!ajuda");
            //embed.WithColor(DiscordColor.Green);
            //embed.Timestamp = DateTime.Now;


        }
    }
}
