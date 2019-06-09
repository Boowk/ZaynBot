using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.RPG.Comandos.Viajar
{
    public class ComandoLeste
    {
        [Command("leste")]
        [Aliases("l")]
        [Description("Explora a área ao leste.")]
        public async Task Leste(CommandContext ctx)
        {
            RPGUsuario usuario = await Banco.ConsultarUsuarioPersonagemAsync(ctx);
            if (usuario.Personagem == null) return;
            RPGPersonagem personagem = usuario.Personagem;

            RPGRegião localAtual = Banco.ConsultarRegions(usuario.Personagem.LocalAtualId);

            foreach (var item in localAtual.SaidasRegioes)
            {
                if (item.Direcao == EnumDirecoes.Leste)
                {
                    bool podeIr = true;
                    if (item.Travado == true)
                    {
                        podeIr = false;
                        if (item.DestravaMissao)
                        {
                            foreach (var missao in personagem.MissoesConcluidasId)
                            {
                                if (missao == item.IdMissaoDestravarPorta)
                                {
                                    podeIr = true;
                                }
                            }
                        }
                    }

                    if (podeIr == true)
                    {
                        usuario.Personagem.LocalAtualId = item.RegiaoId;
                        Banco.AlterarUsuario(usuario);
                        localAtual = Banco.ConsultarRegions(item.RegiaoId);
                        RPGEmbed embed = new RPGEmbed(ctx, "Viajem do");
                        embed.Embed.WithDescription("Você foi para o leste.");
                        embed.Embed.AddField(localAtual.Nome, localAtual.Descrição);
                        await ctx.RespondAsync(embed: embed.Build());
                        return;
                    }
                    else
                    {
                        await ctx.RespondAsync($"{ctx.User.Mention}, parece que está saída está bloqueada.");
                        if (item.DesencadeiaMensagem == true)
                        {
                            RPGEmbed embed = new RPGEmbed(ctx, "Historia do");
                            embed.Embed.WithDescription(item.Mensagem);
                            await ctx.RespondAsync(embed: embed.Build());
                        }
                        return;
                    }
                }
            }
            await ctx.RespondAsync($"{ctx.User.Mention}, não tem caminho nessa direção.");
        }
    }
}
