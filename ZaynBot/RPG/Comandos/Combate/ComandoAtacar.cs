using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;
using ZaynBot.Utilidades;

namespace ZaynBot.RPG.Comandos.Combate
{
    public class ComandoAtacar
    {
        [Command("atacar")]
        [Aliases("at")]
        [Description("Ataca o inimigo na sua frente.\n\n" +
            "Exemplo: z!atacar [id]\n\n" +
            "Uso: z!atacar 1")]
        public async Task Atacar(CommandContext ctx, int id = 0)
        {
            RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioBaseAsync(ctx);
            RPGPersonagem personagem = usuario.Personagem;

            if (personagem.PontosDeVida <= 0)
            {
                await ctx.RespondAsync($"**{ctx.User.Mention}, você está sem vida para executar ações!**");
                return;
            }

            if (personagem.Batalha.Inimigos.Count == 0)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, não tem nenhum inimigo para você atacar.");
                return;
            }

            RPGMob inimigo = null;
            // Procura o inimigo com base no id passado pelo usuario.
            if (id != 0)
            {
                try
                {
                    inimigo = personagem.Batalha.Inimigos[id];
                }
                catch
                {
                    await ctx.RespondAsync($"{ctx.User.Mention}, não foi encontrado nenhum inimigo com o ID {id}.");
                    return;
                }
            }
            else
                inimigo = personagem.Batalha.Inimigos[0];
            DiscordEmbedBuilder embed = CalcBatalha(usuario, inimigo);
            await ctx.RespondAsync(ctx.User.Mention, embed: embed.Build());

        }

        public static DiscordEmbedBuilder CalcBatalha(RPGUsuario usuario, RPGMob inimigoAtacado = null)
        {
            RPGPersonagem personagem = usuario.Personagem;

            if (personagem.Batalha.Turno == 0)
            {
                int velocidadeBatalha = 0;
                foreach (var inimigos in personagem.Batalha.Inimigos)
                {
                    velocidadeBatalha += inimigos.Velocidade;
                }
                personagem.Batalha.PontosDeAcaoBase = personagem.Velocidade + velocidadeBatalha;
            }
            // Definição do round - Aqui é feito o ataque do inimigo e 
            // é onde é definido quando será o turno do jogador.
            StringBuilder mensagemAtaquesInimigos = new StringBuilder();
            Sortear s = new Sortear();
            while (personagem.PontosDeAcao < personagem.Batalha.PontosDeAcaoBase)
            {
                personagem.PontosDeAcao += personagem.Velocidade / 4 + s.Valor(1, personagem.Raca.Sorte);
                foreach (var inimigosAtacando in personagem.Batalha.Inimigos)
                {
                    inimigosAtacando.PontosDeAcao += (inimigosAtacando.Velocidade / 4) + s.Valor(1, 10);
                    if (inimigosAtacando.PontosDeAcao >= personagem.Batalha.PontosDeAcaoBase)
                    {
                        // Ataque do inimigo
                        personagem.Batalha.Turno++;
                        inimigosAtacando.PontosDeAcao = 0;
                        float danoInimigo = CalcDano(personagem.DefesaFisica, inimigosAtacando.AtaqueFisico);
                        personagem.PontosDeVida -= danoInimigo;
                        inimigosAtacando.DanoFeito += danoInimigo;
                    }
                }
            }
            personagem.Batalha.Turno++;
            personagem.PontosDeAcao = 0;
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();

            if (inimigoAtacado != null)
            {
                float danoPersonagem = CalcDano(inimigoAtacado.DefesaFisica, personagem.AtaqueFisico);
                string mensagemMortos = "";
                float danoNoinimigo = 0;
                if (inimigoAtacado.PontosDeVida > danoPersonagem)
                    danoNoinimigo = danoPersonagem;
                else
                    danoNoinimigo = inimigoAtacado.PontosDeVida;

                inimigoAtacado.PontosDeVida -= danoNoinimigo;

                embed.AddField("Inimigos atacados".Titulo(), $"{inimigoAtacado.Nome} recebeu **{danoNoinimigo.Texto2Casas()}** de dano.", true);
                foreach (var inimigo in personagem.Batalha.Inimigos)
                {
                    if (inimigo.DanoFeito != 0)
                    {
                        mensagemAtaquesInimigos.Append($"{inimigo.Nome} deu {inimigo.DanoFeito.Texto2Casas()} de dano.\n");
                        inimigo.DanoFeito = 0;
                    }
                }
                if (mensagemAtaquesInimigos.ToString() != "")
                    embed.AddField("Inimigos que ataracam".Titulo(), mensagemAtaquesInimigos.ToString());

                StringBuilder strVida = new StringBuilder();
                strVida.Append($"Vida: {personagem.PontosDeVida.Texto2Casas()}/{personagem.PontosDeVidaMaxima.Texto2Casas()}\n");
                strVida.Append($"Mana: {personagem.PontosDeMana.Texto2Casas()}/{personagem.PontosDeManaMaximo.Texto2Casas()}\n");
                strVida.Append($"Inimigos: {personagem.Batalha.Inimigos.Count}\n");
                strVida.Append($"Turno {personagem.Batalha.Turno}");
                embed.WithDescription(strVida.ToString());
                embed.WithColor(DiscordColor.Red);


                if (inimigoAtacado.PontosDeVida <= 0)
                {

                    personagem.Batalha.Inimigos.Remove(inimigoAtacado);
                    mensagemMortos = $"{inimigoAtacado.Nome} morreu.";
                    embed.AddField("Inimigos mortos".Titulo(), mensagemMortos.ToString());

                    //Inimigo morreu
                    //List<RPGItem> item = Sortear.SortearItem(inimigo.ChanceCairItem);
                    //bool adicionou = false;
                    //foreach (var itens in item)
                    //{
                    //    adicionou = false;
                    //    adicionou = jogador.Mochila.Adicionar(itens);
                    //    if (adicionou == true)
                    //    {
                    //        msg.Append($"{itens.Quantidade} {itens.Nome}\n");
                    //    }
                    //}




                    StringBuilder mensagemDrops = new StringBuilder();
                    Sortear sortear = new Sortear();
                    foreach (var item in inimigoAtacado.ChanceItemUnico)
                    {
                        if (sortear.Sucesso(item.ChanceDeCair))
                        {
                            if (personagem.ItensNoChao == null)
                                personagem.ItensNoChao = new Dictionary<string, RPGItem>();
                            personagem.ItensNoChao.TryGetValue(item.Item.Nome, out RPGItem itemSorteado);
                            int quantidade = sortear.Valor(1, item.QuantidadeMaxima);
                            item.Item.Quantidade = quantidade;
                            if (itemSorteado == null)
                                personagem.ItensNoChao.Add(item.Item.Nome, item.Item);
                            else
                                itemSorteado.Quantidade += item.Item.Quantidade;
                            mensagemDrops.Append($"{item.Item.Quantidade} {item.Item.Nome.PrimeiraLetraMaiuscula()}\n");
                            break;
                        }
                    }

                    foreach (var item in inimigoAtacado.ChanceItemTodos)
                    {
                        if (sortear.Sucesso(item.ChanceDeCair))
                        {
                            personagem.ItensNoChao.TryGetValue(item.Item.Nome, out RPGItem itemSorteado);
                            int quantidade = sortear.Valor(1, item.QuantidadeMaxima);
                            item.Item.Quantidade = quantidade;
                            if (itemSorteado == null)
                                personagem.ItensNoChao.Add(item.Item.Nome, item.Item);
                            else
                                itemSorteado.Quantidade += item.Item.Quantidade;
                            mensagemDrops.Append($"{item.Item.Quantidade} {item.Item.Nome.PrimeiraLetraMaiuscula()}\n");
                        }
                    }

                    if (mensagemDrops.ToString() != "")
                        embed.AddField("Drops", $"**{mensagemDrops.ToString()}**");

                    if (personagem.Batalha.Inimigos.Count == 0)
                        personagem.Batalha.Turno = 0;
                }
            }
            RPGUsuario.UpdateRPGUsuario(usuario);
            return embed;
        }

        public static float CalcDano(float resistencia, float dano)
        {
            float porcentagemFinal = 100 / (100 + resistencia);
            Sortear s = new Sortear();
            return (s.Valor((dano / 2), dano)) * porcentagemFinal;
        }
    }
}
