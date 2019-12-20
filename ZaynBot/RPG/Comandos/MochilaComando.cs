﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class MochilaComando : BaseCommandModule
    {

        [Command("mochila")]
        [Aliases("m")]
        [Description("Exibe o que tem dentro da mochila do seu personagem e a capacidade atual/max.")]
        [ComoUsar("mochila [pagina|]")]
        [Exemplo("mochila 2")]
        [Exemplo("mochila")]
        [Cooldown(1, 3, CooldownBucketType.User)]

        public async Task ComandoMochilaAb(CommandContext ctx, int pagina = 0)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Inventário", ctx);
            embed.WithColor(DiscordColor.Purple);
            if (usuario.Personagem.Inventario.Itens.Count == 0)
                embed.WithDescription("Nem um farelo dentro.");
            else
            {
                StringBuilder str = new StringBuilder();
                int index = pagina * 10;
                int quantidades = 0;

                for (int i = pagina * 10; i < usuario.Personagem.Inventario.Itens.Count; i++)
                {
                    RPGItem itemData = ModuloBanco.ItemGet(usuario.Personagem.Inventario.Itens.Values[index].Id);
                    ItemDataRPG item = usuario.Personagem.Inventario.Itens.Values[index];
                    str.Append($"**{item.Quantidade} - {itemData.Nome.PrimeiraLetraMaiuscula()}**(ID {usuario.Personagem.Inventario.Itens.Keys[index]})");
                    if (item.DurabilidadeAtual > 0)
                        str.Append($" - *Durab. {item.DurabilidadeAtual}/{itemData.DurabilidadeMax}*");
                    str.Append("\n");
                    index++;
                    quantidades++;
                    if (quantidades == 10)
                        break;
                }
                embed.WithDescription(str.ToString());
                embed.WithFooter($"Página {pagina} | {usuario.Personagem.Inventario.Itens.Count} Itens diferentes");
            }
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
