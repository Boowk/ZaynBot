using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Exibir
{
    public class MochilaComando : BaseCommandModule
    {
        //[Command("mochila")]
        //[Aliases("m")]
        //[Description("Exibe o que tem dentro da mochila do seu personagem e a capacidade.")]
        //[UsoAtributo("mochila [pagina|]")]
        //[ExemploAtributo("mochila 2")]
        //[ExemploAtributo("mochila")]
        //[Cooldown(1, 10, CooldownBucketType.User)]

        //public async Task ComandoMochilaAb(CommandContext ctx, int pagina = 0)
        //{
        //    await ctx.TriggerTypingAsync();
        //    UsuarioRPG.GetPersonagem(ctx, out UsuarioRPG usuario);
        //    PersonagemRPG personagem = usuario.Personagem;
        //    DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Inventário", ctx);
        //    embed.WithColor(DiscordColor.Purple);
        //    if (personagem.Inventario.Itens.Count == 0)
        //        embed.WithDescription("Nem um farelo dentro.");
        //    else
        //    {
        //        StringBuilder str = new StringBuilder();
        //        int index = pagina * 10;
        //        int quantidades = 0;

        //        for (int i = pagina * 10; i < personagem.Inventario.Itens.Count; i++)
        //        {
        //            ItemRPG itemData = ModuloBanco.ItemGet(personagem.Inventario.Itens.Values[index].Id);
        //            ItemDataRPG item = personagem.Inventario.Itens.Values[index];
        //            str.Append($"**{item.Quantidade} - {itemData.Nome.PrimeiraLetraMaiuscula()}**(ID {personagem.Inventario.Itens.Keys[index]})");
        //            if (item.Durabilidade > 0)
        //                str.Append($" - *Durab. {item.Durabilidade}/{itemData.Durabilidade}*");
        //            str.Append("\n");
        //            index++;
        //            quantidades++;
        //            if (quantidades == 10)
        //                break;
        //        }
        //        embed.WithDescription(str.ToString());
        //        embed.WithFooter($"Página {pagina} | {personagem.Inventario.Itens.Count} Itens diferentes");
        //    }
        //    await ctx.RespondAsync(embed: embed.Build());
        //}
    }
}
