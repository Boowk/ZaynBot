using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoItemEquipar : BaseCommandModule
    {
        [Command("equipar")]
        [Aliases("eq")]
        [Description("Permite equipar itens.")]
        [ComoUsar("equipar [nome do item]")]
        [Exemplo("equipar espada de bronze")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoEquiparAb(CommandContext ctx, [RemainingText] string nome = "")
        {
            await ctx.TriggerTypingAsync();
            if (string.IsNullOrWhiteSpace(nome))
            {
                await ctx.ExecutarComandoAsync("ajuda equipar");
                return;
            }

            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
            nome = nome.ToLower();

            if (usuario.Personagem.Mochila.Itens.TryGetValue(nome, out RPGMochilaItemData itemData))
            {
                RPGItem item = ModuloBanco.GetItem(itemData.Id);
                switch (item.Tipo)
                {
                    case EnumTipo.ArmaPrimaria:
                        usuario.Personagem.EquiparItem(item);
                        await ctx.RespondAsync($"A arma primária [{item.Nome}] foi equipada {ctx.User.Mention}!".Bold());
                        break;
                    case EnumTipo.ArmaSecundaria:
                        usuario.Personagem.EquiparItem(item);
                        await ctx.RespondAsync($"A arma segundaria [{item.Nome}] foi equipada {ctx.User.Mention}!".Bold());
                        break;
                    case EnumTipo.Botas:
                        usuario.Personagem.EquiparItem(item);
                        await ctx.RespondAsync($"As botas [{item.Nome}] foram equipadas {ctx.User.Mention}!".Bold());
                        break;
                    case EnumTipo.Peitoral:
                        usuario.Personagem.EquiparItem(item);
                        await ctx.RespondAsync($"O peitoral [{item.Nome}] foi equipado {ctx.User.Mention}!".Bold());
                        break;
                    case EnumTipo.Helmo:
                        usuario.Personagem.EquiparItem(item);
                        await ctx.RespondAsync($"O helmo [{item.Nome}] foi equipado {ctx.User.Mention}!".Bold());
                        break;
                    case EnumTipo.Luvas:
                        usuario.Personagem.EquiparItem(item);
                        await ctx.RespondAsync($"As luvas [{item.Nome}] foram esquipadas {ctx.User.Mention}!".Bold());
                        break;
                    default:
                        await ctx.RespondAsync($"Este [item] não é equipavel {ctx.User.Mention}!".Bold());
                        return;
                }
                usuario.Salvar();
            }
            else
                await ctx.RespondAsync($"Você não tem este [item] {ctx.User.Mention}!".Bold());
        }
    }
}
