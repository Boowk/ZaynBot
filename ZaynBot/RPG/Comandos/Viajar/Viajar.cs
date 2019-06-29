using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.RPG.Comandos.Viajar
{
    public class Viajar
    {
        public async Task ViajarAbAsync(CommandContext ctx, EnumDirecoes enumDirecao, string direcao)
        {
            RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioComPersonagemAsync(ctx);
            RPGPersonagem personagem = usuario.Personagem;
            RPGRegiao localAtual = usuario.GetRPGRegiao();
            foreach (var item in localAtual.SaidasRegioes)
            {
                if (item.Direcao == enumDirecao)
                {
                    bool podeIr = true;
                    if (item.Travado == true)
                    {
                        podeIr = false;
                        if (item.DestravaComMissaoConcluida)
                            foreach (var missao in personagem.MissoesConcluidasId)
                                if (missao == item.DestravaComMissaoConcluidaId)
                                    podeIr = true;
                        if (item.DestravaComMissaoEmAndamento)
                            if (personagem.MissaoEmAndamento != null)
                                if (personagem.MissaoEmAndamento.Id == item.DestravaComMissaoEmAndamentoId)
                                    podeIr = true;
                    }
                    else if (item.TravadoSemItemInventario == true)
                        podeIr = false;
                    if (podeIr == true)
                    {
                        usuario.Personagem.LocalAtualId = item.RegiaoId;
                        ModuloBanco.UpdateUsuario(usuario);
                        localAtual = usuario.GetRPGRegiao();
                        DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Viajem", ctx);
                        embed.WithDescription($"Você foi para o {direcao}.");
                        embed.AddField(localAtual.Nome, localAtual.Descrição);
                        if (localAtual.UrlImagem != null)
                        {
                            embed.WithThumbnailUrl(localAtual.UrlImagem);
                        }
                        await ctx.RespondAsync(embed: embed.Build());
                        return;
                    }
                    else
                    {
                        DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Historia", ctx);
                        embed.WithDescription(item.TravadoMensagem);
                        await ctx.RespondAsync(embed: embed.Build());
                        return;
                    }
                }
            }
            await ctx.RespondAsync($"{ctx.User.Mention}, não tem caminho nessa direção.");
        }
    }
}
