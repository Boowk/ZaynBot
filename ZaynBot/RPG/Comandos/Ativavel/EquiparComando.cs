using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Comandos.Ativavel
{
    public class EquiparComando : BaseCommandModule
    {

        [Command("equipar")]
        [Aliases("eq")]
        [Description("Permite equipar itens que tem durabilidade.")]
        [UsoAtributo("equipar [item id]")]
        [ExemploAtributo("equipar 1:1")]
        public async Task ComandoEquiparAb(CommandContext ctx, [RemainingText] string nome)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.TryGetPersonagemRPG(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;
            if (string.IsNullOrWhiteSpace(nome))
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, não tem como equipar o vento.");
                return;
            }
            ItemRPG item = null;
            bool temItem = personagem.Inventario.Itens.TryGetValue(nome, out ItemDataRPG itemData);
            if (!temItem)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, este item não foi encontrado na sua mochila.");
                return;
            }
            item = ModuloBanco.ItemGet(itemData.Id);

            switch (item.TipoItem)
            {
                case TipoItemEnum.Arma:
                case TipoItemEnum.Botas:
                case TipoItemEnum.Couraca:
                case TipoItemEnum.Escudo:
                case TipoItemEnum.Helmo:
                case TipoItemEnum.Luvas:
                    break;
                default:
                    await ctx.RespondAsync($"{ctx.User.Mention}, este item não é equipavel.");
                    return;
            }

            if (itemData.Durabilidade == 0)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, este item está danificado. Repare-o antes.");
                return;
            }
            // Desequipamos o item antigo, caso tenha
            bool isItemEquipado = personagem.Inventario.Equipamentos.TryGetValue(item.TipoItem, out ItemRPG i);
            if (isItemEquipado)
                personagem.Inventario.DesequiparItem(i, personagem);
            item.Durabilidade = itemData.Durabilidade;
            personagem.Inventario.Equipamentos.Add(item.TipoItem, item);
            // Incrementa-se todos os atributos do item.
            //personagem.Raca.DefesaFisica += item.DefesaFisica;
            //personagem.Raca.DefesaMagica += item.DefesaMagica;
            //personagem.Raca.AtaqueFisico += item.AtaqueFisico;
            //personagem.Raca.AtaqueMagico += item.AtaqueMagico;
            personagem.Inventario.Itens.Remove(nome);
            if (isItemEquipado)
                await ctx.RespondAsync($"{ctx.User.Mention}, você desequipou **({i.Nome})** para equipar **({item.Nome})**.");
            else
                await ctx.RespondAsync($"{ctx.User.Mention}, você equipou **({item.Nome})**.");
            UsuarioRPG.Salvar(usuario);
        }
    }
}
