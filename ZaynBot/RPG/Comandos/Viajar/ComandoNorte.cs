using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.RPG.Comandos.Viajar
{
    public class ComandoNorte
    {
        [Command("norte")]
        [Aliases("n")]
        [Description("Explora a área ao Norte.")]
        public async Task Norte(CommandContext ctx)
        {
            RPGUsuario usuario = await Banco.ConsultarUsuarioPersonagemAsync(ctx);
            if (usuario.Personagem == null) return;
            RPGPersonagem personagem = usuario.Personagem;

            RPGRegião localAtual = Banco.ConsultarRegions(usuario.Personagem.LocalAtualId);

            foreach (var item in localAtual.SaidasRegioes)
            {
                if (item.Direcao == EnumDirecoes.Norte)
                {
                    usuario.Personagem.LocalAtualId = item.RegiaoId;
                    Banco.AlterarUsuario(usuario);
                    localAtual = Banco.ConsultarRegions(item.RegiaoId);
                    RPGEmbed embed = new RPGEmbed(ctx, "Viajem do");
                    embed.Embed.WithDescription("Você foi para o norte.");
                    embed.Embed.AddField(localAtual.Nome, localAtual.Descrição);
                    await ctx.RespondAsync(embed: embed.Build());
                    return;
                }
            }
            await ctx.RespondAsync($"{ctx.User.Mention}, não tem caminho nessa direção.");
        }
    }
}
