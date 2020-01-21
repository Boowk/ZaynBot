using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoLocal : BaseCommandModule
    {
        [Command("localizacao")]
        [Aliases("local")]
        [Description("Permite exibir a localização atual.")]
        [ComoUsar("local")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoLocalAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var jogador = ModuloBanco.GetJogador(ctx);
            if (ModuloBanco.TryGetRegiao(jogador, out var localAtual))
            {

                DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Área", ctx);

                StringBuilder saidas = new StringBuilder();
                foreach (var reg in localAtual.Saidas)
                    saidas.AppendLine(reg.FirstUpper());
                embed.AddField("Conectado á".Titulo(), saidas.ToString(), true);
                embed.WithTitle(localAtual.Nome.FirstUpper());
                embed.WithDescription(string.IsNullOrEmpty(localAtual.Descricao) == true ? "Sem descrição" : localAtual.Descricao);

                await ctx.RespondAsync(embed: embed.Build());
            }
            else
                await ctx.RespondAsync($"Área ainda não adicionado no banco de dados, será adicionado em breve {ctx.User.Mention}!");
        }
    }
}
