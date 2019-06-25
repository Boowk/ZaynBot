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
            RPGUsuario usuario = await ModuloBanco.UsuarioConsultarPersonagemAsync(ctx);
            if (usuario.Personagem == null) return;
            RPGPersonagem personagem = usuario.Personagem;
            RPGRegião localAtual = ModuloBanco.RegiaoConsultar(personagem.LocalAtualId);

            RPGEmbed embed = new RPGEmbed(ctx, "Localização do");
            embed.Embed.WithTitle(localAtual.Nome);
            embed.Embed.WithDescription(localAtual.Descrição);
            StringBuilder conexoesDisponiveis = new StringBuilder();
            foreach (var reg in localAtual.SaidasRegioes)
            {
                conexoesDisponiveis.Append($"{reg.Direcao.ToString()} - {ModuloBanco.RegiaoConsultar(reg.RegiaoId).Nome}\n");
            }
            embed.Embed.AddField("Locais disponíveis", conexoesDisponiveis.ToString());
            embed.Embed.WithColor(DiscordColor.Blue);
            if (localAtual.UrlImagem != null)
            {
                embed.Embed.WithThumbnailUrl(localAtual.UrlImagem);
            }
            StringBuilder npcsDisponiveis = new StringBuilder();
            foreach (var npc in localAtual.Npcs)
            {
                if (npc.Visivel == true)
                {
                    npcsDisponiveis.Append($"{npc.Nome}\n");
                }
            }
            if (!string.IsNullOrWhiteSpace(npcsDisponiveis.ToString()))
            {
                embed.Embed.AddField("Npcs", npcsDisponiveis.ToString());
            }
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
