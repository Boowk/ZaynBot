using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot._Gameplay.Mundos.Anker;
using ZaynBot.Entidades;
using ZaynBot.Entidades.EntidadesRpg.EntidadesRpgMapa;

namespace ZaynBot.Comandos.ComandosRpg
{
    public class ComandoViajar
    {
        [Command("viajar")]
        [Aliases("v")]
        [Description("Viaja para outra área disponível")]
        public async Task ComandoPersonagemAb(CommandContext ctx, [Description("norte,sul,oeste,leste")] string direcao = "nenhuma")
        {


            //if (personagem.Inimigos.Count >= 1)
            //{
            //    await ctx.RespondAsync($"{ctx.User.Mention}, você deve matar todos os inimigos antes de ir para outra área.");
            //    return;
            //}

            if (direcao == "nenhuma")
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você precisa informar uma direção para viajar.");
                return;
            }


            switch (direcao.ToLower())
            {
                case "norte":
                case "n":
                    await ExplorarAreaAsync(Saida.Direcoes.Norte, ctx);
                    break;
                case "sul":
                case "s":
                    await ExplorarAreaAsync(Saida.Direcoes.Sul, ctx);
                    break;
                case "oeste":
                case "o":
                    await ExplorarAreaAsync(Saida.Direcoes.Oeste, ctx);
                    break;
                case "leste":
                case "l":
                    await ExplorarAreaAsync(Saida.Direcoes.Leste, ctx);
                    break;

                default:
                    await ctx.RespondAsync($"{ctx.User.Mention}, você informou uma direção certa?");
                    break;
            }    
        }

        public static async Task ExplorarAreaAsync(Saida.Direcoes direcao, CommandContext ctx)
        {
            Usuario usuario = Banco.ConsultarUsuario(ctx.User.Id);
            foreach (var item in usuario.Personagem.LocalAtual.Saidas)
            {
                if (item.Direcao == direcao)
                {
                    usuario.Personagem.LocalAtual = Areas.Regiões[item.RegiaoId];
                    Banco.AlterarUsuario(usuario);
                    await ctx.RespondAsync($"{ctx.User.Mention}, você chegou em: {usuario.Personagem.LocalAtual.RegiaoNome}");
                    return;
                }
            }
            await ctx.RespondAsync($"{ctx.User.Mention}, parece que você não vai chegar a lugar nenhum indo por ai.");
        }              
    }
}
