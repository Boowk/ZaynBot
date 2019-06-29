using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoLocalizacao
    {
        [Command("localizacao")]
        [Aliases("local")]
        [Description("Mostra a sua localização atual e possíveis regiões para explorar.\n\n" +
            "Uso: z!local")]
        public async Task Localizacao(CommandContext ctx)
        {
            RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioComPersonagemAsync(ctx);
            RPGPersonagem personagem = usuario.Personagem;
            RPGRegiao localAtual = usuario.GetRPGRegiao();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Localização", ctx);
            embed.WithTitle(localAtual.Nome);
            embed.WithDescription(localAtual.Descrição);
            StringBuilder conexoesDisponiveis = new StringBuilder();
            foreach (var reg in localAtual.SaidasRegioes)
                conexoesDisponiveis.Append($"{reg.Direcao.ToString()} - {RPGRegiao.GetRPGRegiao(reg.RegiaoId).Nome}\n");
            embed.AddField("Locais disponíveis", conexoesDisponiveis.ToString());
            embed.WithColor(DiscordColor.Blue);
            if (localAtual.UrlImagem != null)
                embed.WithThumbnailUrl(localAtual.UrlImagem);
            StringBuilder npcsDisponiveis = new StringBuilder();
            foreach (var npc in localAtual.Npcs)
                if (npc.Visivel == true)
                    npcsDisponiveis.Append($"{npc.Nome}\n");
            if (!string.IsNullOrWhiteSpace(npcsDisponiveis.ToString()))
                embed.AddField("Npcs", npcsDisponiveis.ToString());
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
