using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.RPG.Comandos.Viajar
{
    public class Viajar : BaseCommandModule
    {
        public async Task ViajarAbAsync(CommandContext ctx, DirecaoEnum enumDirecao)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.TryGetPersonagemRPG(ctx, out UsuarioRPG usuario);
            RegiaoRPG localAtual = usuario.RegiaoGet();
            foreach (var regiao in localAtual.SaidasRegioes)
            {
                if (regiao.Direcao == enumDirecao)
                {
                    PersonagemRPG personagem = usuario.Personagem;
                    personagem.LocalAtualId = regiao.RegiaoId;
                    UsuarioRPG.Salvar(usuario);
                    localAtual = usuario.RegiaoGet();
                    DiscordEmbedBuilder embedViajeNormal = new DiscordEmbedBuilder().Padrao("Viajem", ctx);
                    embedViajeNormal.WithDescription($"Você foi para o {enumDirecao.ToString()}.");
                    embedViajeNormal.AddField(localAtual.Nome, localAtual.Descrição);
                    if (localAtual.UrlImagem != null)
                        embedViajeNormal.WithThumbnailUrl(localAtual.UrlImagem);
                    await ctx.RespondAsync(embed: embedViajeNormal.Build());
                    return;
                }
            }
            await ctx.RespondAsync($"Este caminho não está disponível! {ctx.User.Mention}.");
        }
    }
}
