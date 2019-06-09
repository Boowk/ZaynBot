using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoViajar
    {
        [Command("viajar")]
        [Aliases("v")]
        [Description("Viaja para outra área disponível")]
        public async Task ComandoPersonagemAb(CommandContext ctx, [Description("norte,sul,oeste,leste")] string direcao = "nenhuma")
        {
            await ctx.RespondAsync("O comando viajar, agora está sendo separado para z!norte, z!sul, z!oeste e z!leste.\n" +
                "Aguarde novas atualizações para poder explorar mais.");
            return;
            RPGUsuario usuario = await Banco.ConsultarUsuarioPersonagemAsync(ctx);
            if (usuario.Personagem == null) return;
            RPGPersonagem personagem = usuario.Personagem;

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
                    await ExplorarAreaAsync(EnumDirecoes.Norte, ctx);
                    break;
                case "sul":
                case "s":
                    await ExplorarAreaAsync(EnumDirecoes.Sul, ctx);
                    break;
                case "oeste":
                case "o":
                    await ExplorarAreaAsync(EnumDirecoes.Oeste, ctx);
                    break;
                case "leste":
                case "l":
                    await ExplorarAreaAsync(EnumDirecoes.Leste, ctx);
                    break;

                default:
                    await ctx.RespondAsync($"{ctx.User.Mention}, você informou uma direção certa?");
                    break;
            }
        }

        public static async Task ExplorarAreaAsync(EnumDirecoes direcao, CommandContext ctx)
        {
            RPGUsuario usuario = Banco.ConsultarUsuario(ctx.User.Id);
            RPGRegião localAtual = Banco.ConsultarRegions(usuario.Personagem.LocalAtualId);

            foreach (var item in localAtual.SaidasRegioes)
            {
                if (item.Direcao == direcao)
                {
                    usuario.Personagem.LocalAtualId = item.RegiaoId;
                    Banco.AlterarUsuario(usuario);
                    localAtual = Banco.ConsultarRegions(item.RegiaoId);
                    await ctx.RespondAsync($"{ctx.User.Mention}, você chegou em: {localAtual.Nome}");
                    return;
                }
            }
            await ctx.RespondAsync($"{ctx.User.Mention}, parece que você não vai chegar a lugar nenhum indo por ai.");
        }
    }
}
