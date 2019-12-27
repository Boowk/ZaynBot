using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    class ComandoItemUsar : BaseCommandModule
    {

        [Command("usar")]
        [Aliases("u")]
        [Description("Permite usar itens que estão na sua mochila.")]
        [ComoUsar("usar [+quantidade|] [item]")]
        [Exemplo("usar 2 frasco vermelho")]
        public async Task ComandoStatusAb(CommandContext ctx, int quantidade, [RemainingText] string itemNome = "")
        {
            await ctx.TriggerTypingAsync();
            if (quantidade <= 0)
            {
                await ctx.ExecutarComandoAsync("ajuda usar-item");
                return;
            }

            if (string.IsNullOrEmpty(itemNome))
            {
                await ctx.ExecutarComandoAsync("ajuda usar-item");
                return;
            }

            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
            RPGRegiao regiaoAtual = ModuloBanco.GetRegiaoData(usuario.Personagem.RegiaoAtualId);
            itemNome = itemNome.ToLower();

            if (usuario.Personagem.Mochila.Itens.TryGetValue(itemNome, out RPGMochilaItemData itemData))
            {
                if (quantidade > itemData.Quantidade)
                {
                    await ctx.RespondAsync($"{ctx.User.Mention} você somente tem {itemData.Quantidade} [{itemNome.FirstUpper()}] na mochila!".Bold());
                    return;
                }
                RPGItem item = ModuloBanco.GetItem(itemData.Id);
                switch (item.Tipo)
                {
                    case Entidades.Enuns.EnumItem.Pocao:
                        usuario.Personagem.Mochila.RemoverItem(itemNome, quantidade);
                        string vidaRestaura = usuario.RecuperarVida(item.VidaRestaura * quantidade).Text();
                        string magiaRestaurada = usuario.RecuperarMagia(item.MagiaRestaura * quantidade).Text();
                        usuario.Salvar();
                        await ctx.RespondAsync($"{ctx.User.Mention} você usou {quantidade} [{item.Nome}]! Restaurado {Emojis.PontosVida()} {vidaRestaura} e {Emojis.PontosPoder()} {magiaRestaurada}!".Bold());
                        break;
                    default:
                        await ctx.RespondAsync($"{ctx.User.Mention} [{item.Nome}] não é usável!");
                        break;
                }
            }
            else
            {
                await ctx.RespondAsync($"{ctx.User.Mention} {itemNome.FirstUpper()} não foi encontrado na mochila!".Bold());
            }
        }

        [Command("usar-item")]
        [ComoUsar("usar-item [item]")]
        [Exemplo("usar-item frasco vermelho")]
        public async Task ComandoStatusAb(CommandContext ctx, [RemainingText] string itemNome = "")
        {
            await ComandoStatusAb(ctx, 1, itemNome);
        }
    }
}
