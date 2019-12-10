using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.RPG.Comandos.Exibir
{
    public class RegiaoComando : BaseCommandModule
    {
<<<<<<< HEAD:ZaynBot/RPG/Comandos/Exibir/RegiaoComando.cs
        [Command("regiao")]
        [Description("Mostra a descrição da região atual")]
        [UsoAtributo("regiao")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task RegiaoComandoAb(CommandContext ctx)
=======
        [Command("localizacao")]
        [Aliases("local")]
        [Description("Mostra a sua localização atual e possíveis regiões para explorar.")]
        [UsoAtributo("localizacao")]
        [Cooldown(1, 3, CooldownBucketType.User)]
        public async Task Localizacao(CommandContext ctx)
>>>>>>> master:ZaynBot/RPG/Comandos/Exibir/LocalComando.cs
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.GetPersonagem(ctx, out UsuarioRPG usuario);
            RegiaoRPG localAtual = usuario.RegiaoGet();

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Localização", ctx);
            embed.WithTitle($"**{localAtual.Nome.Titulo()}**");
            embed.WithDescription(localAtual.Descrição);

            StringBuilder conexoesDisponiveis = new StringBuilder();
            foreach (var reg in localAtual.SaidasRegioes)
                conexoesDisponiveis.Append($"**{reg.Direcao.ToString()}** - {RegiaoRPG.GetRPGRegiao(reg.RegiaoId).Nome}\n");

            if (!string.IsNullOrWhiteSpace(conexoesDisponiveis.ToString()))
                embed.AddField($"**{"Direções disponíveis".Titulo()}**", conexoesDisponiveis.ToString());
            embed.WithColor(DiscordColor.Azure);

            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
