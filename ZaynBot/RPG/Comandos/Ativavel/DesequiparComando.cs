using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Ativavel
{
    public class DesequiparComando : BaseCommandModule
    {
        [Command("desequipar")]
        [Description("Permite desequipar algum item.")]
        [UsoAtributo("desequipar [id]")]
        [ExemploAtributo("desequipar 23")]
        [Cooldown(1, 3, CooldownBucketType.User)]
        public async Task ComandoDesequiparAb(CommandContext ctx, [RemainingText] string nome)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.TryGetPersonagemRPG(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;

            if (string.IsNullOrWhiteSpace(nome))
            {
                await ctx.RespondAsync($"Informe o id do item equipado! {ctx.User.Mention}.");
                return;
            }
            ItemRPG item = null;
            foreach (var i in personagem.Inventario.Equipamentos)
            {
                if (i.Value.Id.ToString() == nome)
                {
                    item = i.Value;
                    break;
                }
            }
            if (item != null)
            {
                personagem.Inventario.DesequiparItem(item, personagem);
                UsuarioRPG.Salvar(usuario);
                await ctx.RespondAsync($"**({item.Nome})** foi desequipado! {ctx.User.Mention}.");
            }
            else
                await ctx.RespondAsync($"Este item não está equipado! {ctx.User.Mention}.");
        }
    }
}
