using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Comandos.Exibir
{
    public class EquipamentosComando : BaseCommandModule
    {
        [Command("equipamentos")]
        [Aliases("eqs")]
        [Description("Permite ver os equipamentos.")]
        [UsoAtributo("equipamentos")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task ComandoEquipamentosAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.TryGetPersonagemRPG(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Personagem", ctx);
            embed.WithTitle($"**Itens equipados**".Titulo());
            StringBuilder str = new StringBuilder();
            foreach (var item in personagem.Inventario.Equipamentos)
            {
                ItemRPG itemData = ModuloBanco.ItemGet(item.Value.Id);
                str.Append($"**{item.Value.Nome}**({itemData.Id})");
                str.Append($" - *Durab. {item.Value.Durabilidade}/{itemData.Durabilidade}*");
                str.Append("\n");
            }
            embed.WithDescription(str.ToString());
            embed.WithColor(DiscordColor.Blue);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
