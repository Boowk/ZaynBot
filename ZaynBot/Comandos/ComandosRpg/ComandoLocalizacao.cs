using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.Comandos.ComandosRpg
{
    public class ComandoLocalizacao
    {
        [Command("localizacao")]
        [Aliases("local")]
        [Description("Mostra a sua localização atual e possíveis regiões para explorar.")]
        public async Task Localizacao(CommandContext ctx)
        {
            RPGUsuario usuario = Banco.ConsultarUsuario(ctx.User.Id);
            RPGPersonagem personagem = usuario.Personagem;
            RPGRegião localAtual = Banco.ConsultarRegions(personagem.LocalAtualId);

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();

            embed.WithTitle($"Localização do {ctx.User.Username}");
            embed.WithDescription($"{localAtual.RegiaoNome} - {localAtual.Descrição}");

            StringBuilder conexoesDisponiveis = new StringBuilder();
            StringBuilder npcsDisponiveis = new StringBuilder();
            foreach (var reg in localAtual.Saidas)
            {
                conexoesDisponiveis.Append($"{reg.Direcao.ToString()} - {Banco.ConsultarRegions(reg.RegiaoId).RegiaoNome}\n");
            }

            //foreach (var npc in personagem.RegiaoAtual.Npcs)
            //{
            //    npcsDisponiveis.Append($"{npc.Nome}\n");
            //}

            embed.AddField("Conexões disponíveis", conexoesDisponiveis.ToString());
            //if (npcsDisponiveis.ToString() != "")
            //{
            //    embed.AddField("Npcs", npcsDisponiveis.ToString());
            //}
            embed.WithColor(DiscordColor.Blue);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
