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
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task ComandoEquiparAb(CommandContext ctx, [RemainingText] string nome)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.GetPersonagem(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;
            if (string.IsNullOrWhiteSpace(nome))
            {
                await ctx.RespondAsync($"Informe o id do item que deseja equipar! {ctx.User.Mention}.");
                return;
            }
            ItemRPG item = null;
            bool temItem = personagem.Inventario.Itens.TryGetValue(nome, out ItemDataRPG itemData);
            if (!temItem)
            {
                await ctx.RespondAsync($"Este item não foi encontrado na sua mochila! {ctx.User.Mention}.");
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
                    await ctx.RespondAsync($"Este item não é equipavel! {ctx.User.Mention}.");
                    return;
            }

            if (itemData.Durabilidade == 0)
            {
                await ctx.RespondAsync($"Este item está danificado! {ctx.User.Mention}.");
                return;
            }
            // Desequipamos o item antigo, caso tenha
            bool isItemEquipado = personagem.Inventario.Equipamentos.TryGetValue(item.TipoItem, out ItemRPG i);
            if (isItemEquipado)
                personagem.Inventario.DesequiparItem(i, personagem);
            item.Durabilidade = itemData.Durabilidade;
            personagem.Inventario.Equipamentos.Add(item.TipoItem, item);
            personagem.Inventario.Itens.Remove(nome);
            if (isItemEquipado)
                await ctx.RespondAsync($"**({i.Nome})** Foi desequipado para equipar ***({item.Nome})!*** {ctx.User.Mention}.");
            else
                await ctx.RespondAsync($"**({item.Nome})** equipado! {ctx.User.Mention}.");
            UsuarioRPG.Salvar(usuario);
        }
    }
}
