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
    public class ComandoLoot
    {
        [Command("loot")]
        [Description("Ver os loots que estão no chão.\n\n" +
         "Uso: z!loot")]
        public async Task ComandoLootAb(CommandContext ctx, int id = 0)
        {
            RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioBaseAsync(ctx);
            RPGPersonagem personagem = usuario.Personagem;

            // Se a vida do personagem for igual ou menor que 0, não poderá atacar.
            if (personagem.PontosDeVida <= 0)
            {
                await ctx.RespondAsync($"**{ctx.User.Mention}, você está sem vida.**");
                return;
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.Padrao("Itens no chão", ctx);
            StringBuilder str = new StringBuilder();
            if (personagem.ItensNoChao == null)
                personagem.ItensNoChao = new Dictionary<string, RPGItem>();
            if (personagem.ItensNoChao.Count == 0)
                embed.AddField("Itens".Titulo(), "Você não matou nenhum inimigo.");
            else
            {
                foreach (var item in personagem.ItensNoChao)
                {
                    str.Append($"{item.Value.Quantidade} - {item.Key.PrimeiraLetraMaiuscula()}\n");
                }
                embed.AddField("Itens".Titulo(), str.ToString());
            }

            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
