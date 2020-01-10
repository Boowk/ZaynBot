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
                        if (await EquiparItemAsync(ctx, usuario, item))
                            await ctx.RespondAsync($"A arma primária [{item.Nome}] foi equipada {ctx.User.Mention}!".Bold());
                        break;
                    case EnumTipo.ArmaSecundaria:
                        if (await EquiparItemAsync(ctx, usuario, item))
                            await ctx.RespondAsync($"A arma segundaria [{item.Nome}] foi equipada {ctx.User.Mention}!".Bold());
                        break;
                    case EnumTipo.Botas:
                        if (await EquiparItemAsync(ctx, usuario, item))
                            await ctx.RespondAsync($"As botas [{item.Nome}] foram equipadas {ctx.User.Mention}!".Bold());
                        break;
                    case EnumTipo.Peitoral:
                        if (await EquiparItemAsync(ctx, usuario, item))
                            await ctx.RespondAsync($"O peitoral [{item.Nome}] foi equipado {ctx.User.Mention}!".Bold());
                        break;
                    case EnumTipo.Helmo:
                        if (await EquiparItemAsync(ctx, usuario, item))
                            await ctx.RespondAsync($"O helmo [{item.Nome}] foi equipado {ctx.User.Mention}!".Bold());
                        break;
                    case EnumTipo.Luvas:
                        if (await EquiparItemAsync(ctx, usuario, item))
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

        private async Task<bool> EquiparItemAsync(CommandContext ctx, RPGUsuario usuario, RPGItem item)
        {
            if (item.ProficienciaNivelRequisito != 1)
            {
                usuario.Personagem.Proficiencias.TryGetValue(item.Proficiencia, out RPGProficiencia proff);
                if (proff.Pontos >= item.ProficienciaNivelRequisito)
                {
                    usuario.Personagem.EquiparItem(item);
                    return true;
                }
                else
                {
                    await ctx.RespondAsync($"Você precisa de {item.ProficienciaNivelRequisito} pontos em {item.Proficiencia.ToString()} para usar [{item.Nome}] {ctx.User.Mention}!".Bold());
                    return false;
                }
            }
            usuario.Personagem.EquiparItem(item);
            return true;
        }
    }
}
