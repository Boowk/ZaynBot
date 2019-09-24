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
        [Command("mochila")]
        [Aliases("m")]
        [Description("Exibe o que tem dentro da mochila do seu personagem e a capacidade.")]
        [UsoAtributo("mochila [pagina|]")]
        [ExemploAtributo("mochila 2")]
        [ExemploAtributo("mochila")]

        public async Task ComandoMochilaAb(CommandContext ctx, int pagina = 0)
        {
            // Avisa que está escrevendo no discord
            await ctx.TriggerTypingAsync();
            // Recupera os dados do jogador e o armazena na variavel usuario
            UsuarioRPG.TryGetPersonagemRPG(ctx, out UsuarioRPG usuario);
            // Faz uma indicação do persongem do usuario
            PersonagemRPG personagem = usuario.Personagem;
            // Prepara uma nova mensagem 
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Inventário", ctx);
            //DiscordEmoji mochila = DiscordEmoji.FromGuildEmote(ModuloCliente.Client, 590908712324038659);
            embed.WithColor(DiscordColor.Purple);
            // Se não tiver nenhum 
            if (personagem.Inventario.Itens.Count == 0)
                // Avisa que não tem nenhum item
                embed.WithDescription("Nem um farelo dentro.");
            else
            {
                //  tem capacidade para 10 Itens.
                StringBuilder str = new StringBuilder();
                int index = pagina * 10;
                int quantidades = 0;

                // Recupera 10 itens de uma forma bem complicada
                for (int i = pagina * 10; i < personagem.Inventario.Itens.Count; i++)
                {
                    ItemRPG itemData = ModuloBanco.ItemGet(personagem.Inventario.Itens.Values[index].Id);
                    ItemDataRPG item = personagem.Inventario.Itens.Values[index];
                    str.Append($"**{item.Quantidade} - {itemData.Nome.PrimeiraLetraMaiuscula()}**(ID {personagem.Inventario.Itens.Keys[index]})");
                    if (item.Durabilidade > 0)
                        str.Append($" - *Durab. {item.Durabilidade}/{itemData.Durabilidade}*");
                    str.Append("\n");
                    index++;
                    quantidades++;
                    if (quantidades == 10)
                        break;
                }
                embed.WithDescription(str.ToString());
                embed.WithFooter($"Página {pagina} | {personagem.Inventario.Itens.Count} Itens diferentes");
            }
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
