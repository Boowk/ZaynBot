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
        [Description("Permite olhar para muitas coisas.")]
        [ComoUsar("olhar [|nome]")]
        [Exemplo("olhar rato")]
        [Exemplo("olhar")]
        [Cooldown(1, 8, CooldownBucketType.User)]
        public async Task ComandoLocalAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);

            RPGRegiao localAtual = RPGRegiao.GetRegiao(usuario.Personagem.RegiaoAtualId);

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Região", ctx);

            RPGMapa mapa = new RPGMapa();
            foreach (var reg in localAtual.SaidasRegioes)
                mapa.AdicionarRegiao(reg);

            embed.WithDescription($"{mapa.Criar()}" +
                  $"\n\n{localAtual.Nome.Titulo()} - {localAtual.Id}" +
                  $"\n{localAtual.Descrição}");

            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
