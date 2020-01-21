using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;

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

            var usuario = ModuloBanco.GetJogador(ctx);
            nome = nome.ToLower();

            if (usuario.Mochila.TryGetValue(nome, out var itemData))
            {
                if (ModuloBanco.TryGetItem(nome, out var item))
                {
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
                        case EnumTipo.Elmo:
                            if (await EquiparItemAsync(ctx, usuario, item))
                                await ctx.RespondAsync($"O elmo [{item.Nome}] foi equipado {ctx.User.Mention}!".Bold());
                            break;
                        case EnumTipo.Luvas:
                            if (await EquiparItemAsync(ctx, usuario, item))
                                await ctx.RespondAsync($"As luvas [{item.Nome}] foram esquipadas {ctx.User.Mention}!".Bold());
                            break;
                        case EnumTipo.Picareta:
                            if (await EquiparItemAsync(ctx, usuario, item))
                                await ctx.RespondAsync($"A picareta [{item.Nome}] foi equipada {ctx.User.Mention}!".Bold());
                            break;
                        default:
                            await ctx.RespondAsync($"Este [item] não é equipavel {ctx.User.Mention}!".Bold());
                            return;
                    }
                    usuario.Salvar();
                }
                else
                {
                    await ctx.RespondAsync($"Item ainda não adicionado no banco de dados, será adicionado em breve {ctx.User.Mention}!".Bold());
                    return;
                }
            }
            else
                await ctx.RespondAsync($"Você não tem este [item] {ctx.User.Mention}!".Bold());
        }

        private async Task<bool> EquiparItemAsync(CommandContext ctx, RPGJogador usuario, RPGItem item)
        {
            if (item.ProficienciaNivelRequisito != 1)
            {
                usuario.Proficiencias.TryGetValue(item.Proficiencia, out RPGProficiencia proff);
                if (proff.Pontos >= item.ProficienciaNivelRequisito)
                {
                    usuario.EquiparItem(item);
                    return true;
                }
                else
                {
                    await ctx.RespondAsync($"Você precisa de {item.ProficienciaNivelRequisito} pontos em {item.Proficiencia.ToString()} para usar [{item.Nome}] {ctx.User.Mention}!".Bold());
                    return false;
                }
            }
            usuario.EquiparItem(item);
            return true;
        }
    }
}
