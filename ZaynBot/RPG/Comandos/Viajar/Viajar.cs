using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;
using ZaynBot.RPG.Entidades.Mapa;
using ZaynBot.Utilidades;

namespace ZaynBot.RPG.Comandos.Viajar
{
    public class Viajar
    {
        public async Task ViajarAbAsync(CommandContext ctx, EnumDirecoes enumDirecao, string direcao)
        {
            RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioBaseAsync(ctx);
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


                        if (personagem.PontosDeVida <= 0)
                        {
                            await ctx.RespondAsync($"**{ctx.User.Mention}, você está sem vida.**");
                            return;
                        }

                        if (personagem.Batalha.Inimigos.Count != 0)
                        {
                            StringBuilder mensagemAtacantes = new StringBuilder();
                            if (personagem.Batalha.Turno == 0)
                            {
                                float velocidadeInimigo = 0;
                                foreach (var vel in personagem.Batalha.Inimigos)
                                    velocidadeInimigo += vel.Velocidade;
                                personagem.Batalha.PontosDeAcaoBase = personagem.Velocidade + velocidadeInimigo;
                            }
                            while (personagem.PontosDeAcao < personagem.Batalha.PontosDeAcaoBase)
                            {
                                // Verifica quem começa atacando com base na velocidade maior

                                personagem.PontosDeAcao += personagem.Velocidade / 4;
                                foreach (var inimigos in personagem.Batalha.Inimigos)
                                {
                                    inimigos.PontosDeAcao += inimigos.Velocidade / 4;
                                    if (inimigos.PontosDeAcao >= personagem.Batalha.PontosDeAcaoBase)
                                    {
                                        personagem.Batalha.Turno++;
                                        inimigos.PontosDeAcao = 0;
                                        float danorecebido = inimigos.AtaqueFisico - personagem.DefesaFisica;
                                        if (danorecebido > 0)
                                            personagem.PontosDeVida -= danorecebido;
                                        else
                                            danorecebido = 0;
                                        mensagemAtacantes.Append($"{inimigos.Nome} deu {string.Format("{0:N2}", danorecebido)} " +
                                            $"de dano.\n");
                                    }
                                }
                            }
                            personagem.Batalha.Turno++;
                            personagem.PontosDeAcao = 0;
                            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();


                            embed.WithTitle($"**{ctx.User.Username}**");
                            StringBuilder strDesc = new StringBuilder();
                            strDesc.Append($"Vida: {string.Format("{0:N2}", personagem.PontosDeVida)}/{string.Format("{0:N2}", personagem.PontosDeVidaMaxima)}" +
                                $"\nInimigos: {personagem.Batalha.Inimigos.Count}\nTurno {personagem.Batalha.Turno}");
                            if (personagem.Batalha.Inimigos.Count == 0)
                                personagem.Batalha.Turno = 0;



                            embed.WithColor(DiscordColor.Red);
                            RPGUsuario.UpdateRPGUsuario(usuario);
                            Sortear sortear = new Sortear();
                            if (sortear.Sucesso(0.5))
                            {
                                usuario.Personagem.LocalAtualId = item.RegiaoId;
                                usuario.Personagem.ItensNoChao = new Dictionary<string, RPGItem>();
                                usuario.Personagem.Batalha.Inimigos = new List<RPGMob>();
                                usuario.Personagem.Batalha.Turno = 0;
                                usuario.Personagem.Batalha.PontosDeAcaoBase = 0;
                                RPGUsuario.UpdateRPGUsuario(usuario);
                                localAtual = usuario.GetRPGRegiao();
                                DiscordEmbedBuilder embedViajou = new DiscordEmbedBuilder().Padrao("Viajem", ctx);
                                embedViajou.WithDescription($"Você foi para o {direcao}.");
                                embedViajou.AddField(localAtual.Nome, localAtual.Descrição);
                                if (mensagemAtacantes.ToString() != "")
                                {
                                    embedViajou.AddField("Você foi atacado tentando fugir", mensagemAtacantes.ToString());
                                }
                                if (localAtual.UrlImagem != null)
                                {
                                    embedViajou.WithThumbnailUrl(localAtual.UrlImagem);
                                }
                                await ctx.RespondAsync(embed: embedViajou.Build());
                            }
                            else
                            {
                                if (mensagemAtacantes.ToString() != "")
                                {
                                    embed.AddField("Inimigos que ataracam", mensagemAtacantes.ToString());
                                }
                                strDesc.Append("\nVocê não conseguiu fugir.");
                                embed.WithDescription(strDesc.ToString());
                                await ctx.RespondAsync(embed: embed.Build());
                            }
                            return;
                        }

                        usuario.Personagem.ItensNoChao = new Dictionary<string, RPGItem>();
                        usuario.Personagem.LocalAtualId = item.RegiaoId;
                        RPGUsuario.UpdateRPGUsuario(usuario);
                        localAtual = usuario.GetRPGRegiao();
                        DiscordEmbedBuilder embedViajeNormal = new DiscordEmbedBuilder().Padrao("Viajem", ctx);
                        embedViajeNormal.WithDescription($"Você foi para o {direcao}.");
                        embedViajeNormal.AddField(localAtual.Nome, localAtual.Descrição);
                        if (localAtual.UrlImagem != null)
                        {
                            embedViajeNormal.WithThumbnailUrl(localAtual.UrlImagem);
                        }
                        await ctx.RespondAsync(embed: embedViajeNormal.Build());
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
