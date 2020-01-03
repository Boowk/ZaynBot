using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoOlhar : BaseCommandModule
    {
        [Command("olhar")]
        [Description("Permite visualizar a região atual.")]
        [ComoUsar("olhar")]
        [Cooldown(1, 8, CooldownBucketType.User)]
        public async Task ComandoLocalAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
            RPGRegiao localAtual = RPGRegiao.GetRegiao(usuario.Personagem.RegiaoAtualId);

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Região", ctx);
            embed.WithTitle($"{localAtual.Nome.Titulo()} - {localAtual.Id}");
            embed.WithDescription(localAtual.Descrição);

            StringBuilder conexoesDisponiveis = new StringBuilder();
            foreach (var reg in localAtual.SaidasRegioes)
                conexoesDisponiveis.Append($"{reg.Direcao.ToString().Bold()}, ");

            if (!string.IsNullOrWhiteSpace(conexoesDisponiveis.ToString()))
                embed.AddField("Direções obvias".Titulo(), conexoesDisponiveis.ToString());

            embed.WithColor(DiscordColor.Azure);

            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
