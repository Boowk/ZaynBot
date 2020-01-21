using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Comandos
{
    class ComandoItemUsar : BaseCommandModule
    {
        [Command("usar")]
        [Aliases("u")]
        [Description("Permite usar itens que estão na sua mochila.")]
        [ComoUsar("usar [+quantidade|] [item]")]
        [Exemplo("usar 2 frasco vermelho")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoUsarAb(CommandContext ctx, int quantidade, [RemainingText] string itemNome = "")
        {
            await ctx.TriggerTypingAsync();
            if (quantidade <= 0)
            {
                await ctx.ExecutarComandoAsync("ajuda usar");
                return;
            }

            if (string.IsNullOrEmpty(itemNome))
            {
                await ctx.ExecutarComandoAsync("ajuda usar");
                return;
            }

            var usuario = ModuloBanco.GetJogador(ctx);
            itemNome = itemNome.ToLower();

            if (usuario.Mochila.TryGetValue(itemNome, out var itemData))
            {
                if (quantidade > itemData)
                {
                    await ctx.RespondAsync($"{ctx.User.Mention} você somente tem {itemData} [{itemNome.FirstUpper()}] na mochila!".Bold());
                    return;
                }
                ModuloBanco.TryGetItem(itemNome, out var item);
                switch (item.Tipo)
                {
                    case EnumTipo.Pocao:
                        usuario.Mochila.RemoverItem(itemNome, quantidade);
                        string vidaRestaura = usuario.RecuperarVida(item.VidaRestaura * quantidade).Text();
                        string magiaRestaurada = usuario.RecuperarMagia(item.MagiaRestaura * quantidade).Text();
                        usuario.Salvar();
                        await ctx.RespondAsync($"{ctx.User.Mention} você usou {quantidade} [{item.Nome}]! Restaurado {Emojis.PontosVida} {vidaRestaura} e {Emojis.PontosPoder} {magiaRestaurada}!".Bold());
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

        [Command("usar")]
        [ComoUsar("usar [item]")]
        [Exemplo("usar frasco vermelho")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoUsarAb(CommandContext ctx, [RemainingText] string itemNome = "")
        {
            await ComandoUsarAb(ctx, 1, itemNome);
        }
    }
}
