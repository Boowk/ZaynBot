using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoAtacar
    {
        [Command("atacar")]
        [Aliases("at")]
        [Description("Ataca o inimigo na sua frente.")]
        public async Task Atacar(CommandContext ctx, [Description("Id do alvo")] int id = 0)
        {
            RPGUsuario usuario = Banco.ConsultarUsuario(ctx.User.Id);
            RPGPersonagem personagem = usuario.Personagem;

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();

            if (personagem.PontosDeVida <= 0)
            {
                await ctx.RespondAsync($"**{ctx.User.Mention}, você está morto.**");
                return;
            }

            if (personagem.CampoBatalha.Inimigos.Count == 0)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, não tem nenhum inimigo na sua frente para você atacar.");
                return;
            }

            RPGMob inimigo = null;

            if (id != 0)
            {
                try
                {
                    inimigo = personagem.CampoBatalha.Inimigos[id];
                }
                catch
                {
                    await ctx.RespondAsync($"{ctx.User.Mention}, não foi encontrado nenhum inimigo com o ID {id}.");
                    return;
                }
            }
            else
            {
                inimigo = personagem.CampoBatalha.Inimigos[0];
            }

            //if (Aleatorio.SortearAtaque(chanceAcerto) == true)
            //{
            float danoTotalDoPersonagem = personagem.AtaqueFisico;

            string mensagemMortos = "";
            float danoNoinimigo = 0;

            if (inimigo.PontosDeVida > danoTotalDoPersonagem)
                danoNoinimigo = danoTotalDoPersonagem;
            else
                danoNoinimigo = inimigo.PontosDeVida;

            inimigo.PontosDeVida -= danoTotalDoPersonagem;
            //  StringBuilder mensagemDrops = new StringBuilder();
            if (inimigo.PontosDeVida <= 0)
            {
                personagem.CampoBatalha.Inimigos.Remove(inimigo);
                mensagemMortos = $"{inimigo.Nome} morreu.";
                // MorteInimigo(personagem, inimigo, mensagemDrops);
            }

            embed.AddField("Inimigos atacados", $"{inimigo.Nome}(ID {id}) recebeu {danoNoinimigo.Texto()} de dano.\n{mensagemMortos.ToString()}", true);

            //if (mensagemDrops.ToString() != "")
            //{
            //    embed.AddField("Drops", $"**{mensagemDrops.ToString()}**", true);
            //}

            //}
            //else
            //{
            //    embed.AddField("Inimigos atacados", $"Você errou o ataque no {inimigo.Nome}(ID {inimigo.Id}).", true);

            //}
            StringBuilder mensagemAtacantes = new StringBuilder();
            //   Hit hit;
            for (int i = 0; i < personagem.CampoBatalha.Inimigos.Count; i++)
            {
                //      mensagemAtacantes.Append($"{personagem.Inimigos[i].Nome}(ID {personagem.Inimigos[i].Id}) errou o ataque.\n");
                personagem.PontosDeVida -= personagem.CampoBatalha.Inimigos[i].AtaqueFisico;
                mensagemAtacantes.Append($"{personagem.CampoBatalha.Inimigos[i].Nome}(ID {i}) deu {string.Format("{0:N2}", personagem.CampoBatalha.Inimigos[i].AtaqueFisico)} " +
                    $"de dano.\n");
            }


            embed.WithTitle($"**{ctx.User.Username}**");

            embed.WithDescription($"Vida: {string.Format("{0:N2}", personagem.PontosDeVida)}/{string.Format("{0:N2}", personagem.PontosDeVidaMaxima)}" +
                $"\nInimigos: {personagem.CampoBatalha.Inimigos.Count}");

            if (mensagemAtacantes.ToString() != "")
            {
                embed.AddField("Inimigos que ataracam", mensagemAtacantes.ToString());
            }

            embed.WithColor(DiscordColor.Red);
            Banco.AlterarUsuario(usuario);
            await ctx.RespondAsync(ctx.User.Mention, embed: embed.Build());
        }

        //public static void MorteInimigo(Personagem jogador, Inimigo inimigo, StringBuilder msg)
        //{
        //    List<Item> item = Aleatorio.SortearItem(inimigo.ChanceCairItem);
        //    bool adicionou = false;
        //    foreach (var itens in item)
        //    {
        //        adicionou = false;
        //        adicionou = jogador.Mochila.Adicionar(itens);
        //        if (adicionou == true)
        //        {
        //            msg.Append($"{itens.Quantidade} {itens.Nome}\n");
        //        }
        //    }
        //}
    }
}
