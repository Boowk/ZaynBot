using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoProficiencia : BaseCommandModule
    {
        [Command("proficiencia")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        [Description("Exibe todas as proficiencia de um personagem.")]
        [ComoUsar("proficiencia")]
        public async Task HabilidadeComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Proficiencia", ctx);
            embed.WithDescription($"Pontos disponíveis: {usuario.Personagem.ProficienciaPontos}".Bold());
            foreach (var item in usuario.Personagem.Proficiencias)
                embed.AddField(item.Value.Nome.ToString().Titulo().Bold(), $"{item.Value.Pontos}".Bold(), true);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}

