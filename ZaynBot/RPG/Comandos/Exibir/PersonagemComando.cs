using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Exibir
{
    public class PersonagemComando : BaseCommandModule
    {
        [Command("personagem")]
        [Description("Exibe os atributos e bonus do seu personagem.")]
        [Cooldown(1, 3, CooldownBucketType.User)]
        public async Task StatusComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.TryGetPersonagemRPG(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;
            RacaRPG raca = personagem.Raca;
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Personagem", ctx);
            embed.WithColor(DiscordColor.Blue);
            StringBuilder sr = new StringBuilder();
            sr.AppendLine($"**Nome raça:** {personagem.Raca.Nome}");
            sr.AppendLine($"**Nível:** {personagem.NivelAtual}");
            sr.AppendLine($"**Experiencia:** {personagem.ExpAtual.Texto2Casas()}/{personagem.ExpMax.Texto2Casas()}");
            sr.AppendLine($"**Pontos disponíveis:** {raca.Pontos}");
            sr.AppendLine($"**Força:** {raca.Forca}");
            sr.AppendLine($"**Força de vontade:** {raca.ForcaDeVontade}");
            sr.AppendLine($"**Resistencia:** {raca.Resistencia}");
            sr.AppendLine($"**Inteligencia:** {raca.Inteligencia}");
            sr.AppendLine($"**Agilidade:** {raca.Agilidade}");
            embed.WithDescription(sr.ToString());
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
