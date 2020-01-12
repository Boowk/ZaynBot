using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoItemVender : BaseCommandModule
    {
        [Command("vender")]
        [Aliases("v")]
        [Description("Permite vender itens que estão na sua mochila.")]
        [ComoUsar("vender [+quantidade|] [item]")]
        [Exemplo("vender 2 frasco vermelho")]
        public async Task ComandoVenderAb(CommandContext ctx, int quantidade, [RemainingText] string itemNome = "")
        {
            await ctx.TriggerTypingAsync();

            if (quantidade <= 0)
            {
                await ctx.ExecutarComandoAsync("ajuda vender");
                return;
            }

            if (string.IsNullOrEmpty(itemNome))
            {
                await ctx.ExecutarComandoAsync("ajuda vender");
                return;
            }

            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
            RPGRegiao localAtual = RPGRegiao.GetRegiao(usuario.Personagem.RegiaoAtualId);
            if (localAtual.LojaItensId.Count == 0)
            {
                await ctx.RespondAsync($"Ninguém está comprando itens aqui {ctx.User.Mention}!");
                return;
            }

            itemNome = itemNome.ToLower();

            if (usuario.Personagem.Mochila.Itens.TryGetValue(itemNome, out RPGMochilaItemData itemData))
            {
                if (quantidade > itemData.Quantidade)
                {
                    await ctx.RespondAsync($"{ctx.User.Mention} você somente tem {itemData.Quantidade} [{itemNome.FirstUpper()}] na mochila!".Bold());
                    return;
                }
                RPGItem item = ModuloBanco.GetItem(itemData.Id);
                if (item.Preco != 0)
                {
                    int valorReal = Convert.ToInt32(item.Preco * 0.3) * quantidade;
                    usuario.Personagem.Mochila.RemoverItem(itemNome, quantidade);
                    usuario.Personagem.Mochila.AdicionarItem("moeda de Zeoin", new RPGMochilaItemData()
                    {
                        Id = 0,
                        Quantidade = valorReal
                    });
                    usuario.Salvar();
                    await ctx.RespondAsync($"{ctx.User.Mention} você vendeu {quantidade} [{itemNome.FirstUpper()}] por {valorReal} Zeoin!".Bold());

                }
                else
                    await ctx.RespondAsync($"{ctx.User.Mention} não é possivel vender {itemNome.FirstUpper()}!".Bold());
            }
            else
                await ctx.RespondAsync($"{ctx.User.Mention} [{itemNome.FirstUpper()}] não foi encontrado na mochila!".Bold());
        }

        [Command("vender")]
        [ComoUsar("vender [item]")]
        [Exemplo("vender frasco vermelho")]
        public async Task ComandoVenderAb(CommandContext ctx, [RemainingText] string itemNome = "")
        {
            await ComandoVenderAb(ctx, 1, itemNome);
        }
    }
}
