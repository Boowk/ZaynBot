using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class RegiaoComando : BaseCommandModule
    {
        [Command("regiao")]
        [Description("Permite visualizar a região atual melhor.")]
        [UsoAtributo("regiao")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task RegiaoComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetPersonagem(ctx, out RPGUsuario usuario);
            RPGRegiao localAtual = usuario.RegiaoGet();

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Região", ctx);
            embed.WithTitle($"**{localAtual.Nome.Titulo()}**");
            embed.WithDescription(localAtual.Descrição);

            StringBuilder conexoesDisponiveis = new StringBuilder();
            foreach (var reg in localAtual.SaidasRegioes)
                conexoesDisponiveis.Append($"**{reg.Direcao.ToString()}** - {RPGRegiao.GetRPGRegiao(reg.RegiaoId).Nome}\n");

            if (!string.IsNullOrWhiteSpace(conexoesDisponiveis.ToString()))
                embed.AddField($"**{"Direções disponíveis".Titulo()}**", conexoesDisponiveis.ToString());
            embed.WithColor(DiscordColor.Azure);

            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
