using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Combate
{
    public class ComandoInimigos
    {
        [Command("inimigos")]
        [Aliases("ins")]
        [Description("Veja todos os inimigos a sua volta.")]
        public async Task VerInimigos(CommandContext ctx)
        {
            RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioBaseAsync(ctx);
            RPGPersonagem personagem = usuario.Personagem;
            if (personagem.Batalha.Inimigos.Count == 0)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, parece que você não tem nenhum inimigo.");
                return;
            }
            StringBuilder inimigos = new StringBuilder();
            int quant = 0;
            foreach (var item in personagem.Batalha.Inimigos)
            {
                inimigos.Append($"{item.Nome}(ID {quant}) | Vida: {string.Format("{0:N2}", item.PontosDeVida)}\n");
                quant++;
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithTitle($"Inimigos do {ctx.User.Username}");
            embed.WithDescription($"{quant} ainda vivos.");
            embed.AddField("Inimigos", inimigos.ToString());
            embed.WithColor(DiscordColor.Red);
            await ctx.RespondAsync(ctx.User.Mention, embed: embed.Build());
        }
    }
}
