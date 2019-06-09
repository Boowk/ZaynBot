using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.RPG.Comandos.Viajar
{
    public class ComandoLeste
    {
        [Command("leste")]
        [Aliases("l")]
        [Description("Explora uma a área ao leste.")]
        public async Task ComandoPersonagemAb(CommandContext ctx)
        {
            RPGUsuario usuario = await Banco.ConsultarUsuarioPersonagemAsync(ctx);
            if (usuario.Personagem == null) return;
            RPGPersonagem personagem = usuario.Personagem;

            RPGRegião localAtual = Banco.ConsultarRegions(usuario.Personagem.LocalAtualId);

            foreach (var item in localAtual.SaidasRegioes)
            {
                if (item.Direcao == EnumDirecoes.Leste)
                {
                    usuario.Personagem.LocalAtualId = item.RegiaoId;
                    Banco.AlterarUsuario(usuario);
                    localAtual = Banco.ConsultarRegions(item.RegiaoId);
                    RPGEmbed embed = new RPGEmbed(ctx, "Viajem do");
                    embed.Embed.WithDescription("Você foi para o leste.");
                    embed.Embed.AddField(localAtual.Nome, localAtual.Descrição);
                    await ctx.RespondAsync(embed: embed.Build());
                    return;
                }
            }
            await ctx.RespondAsync($"{ctx.User.Mention}, não tem caminho nessa direção.");
        }
    }
}
