using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Comandos.Ativavel;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;
using ZaynBot.RPG.Entidades.Mapa;
using ZaynBot.Utilidades;

namespace ZaynBot.RPG.Comandos.Viajar
{
    public class Viajar : BaseCommandModule
    {
        public async Task ViajarAbAsync(CommandContext ctx, DirecaoEnum enumDirecao)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.TryGetPersonagemRPG(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;

            if (personagem.Batalha.Inimigos.Count > 0)
                if (Sortear.Sucesso(0.5))
                {
                    personagem.Batalha.Inimigos.Clear();
                    personagem.Batalha.Turno = 0;
                    await ViajandoAbAsync(usuario, enumDirecao, ctx);
                }
                else
                {


                    DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Combate", ctx);

                    if (personagem.Batalha.Turno == 0)
                    {
                        int velocidadeInimigos = 0;
                        foreach (var inimigo in personagem.Batalha.Inimigos)
                            velocidadeInimigos += inimigo.Velocidade;
                        personagem.Batalha.PontosDeAcaoTotal = personagem.Raca.Agilidade + velocidadeInimigos;
                    }
                    double danoRecebido = 0;
                    while (personagem.Batalha.PontosDeAcao < personagem.Batalha.PontosDeAcaoTotal)
                    {
                        personagem.Batalha.PontosDeAcao += personagem.Raca.Agilidade / 4 + Sortear.Valor(1, 10);
                        foreach (var inimigo in personagem.Batalha.Inimigos)
                        {
                            inimigo.PontosDeAcao += (inimigo.Velocidade / 4) + Sortear.Valor(1, 10);

                            if (inimigo.PontosDeAcao >= personagem.Batalha.PontosDeAcaoTotal)
                            {
                                personagem.Batalha.Turno++;
                                inimigo.PontosDeAcao = 0;

                                double danoInimigo = 0;
                                Random r = new Random();
                                int sorteioAtaque = r.Next(0, 5);
                                ItemRPG armadura = null;
                                switch (sorteioAtaque)
                                {
                                    case 1:
                                        personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Botas, out armadura);
                                        break;
                                    case 2:
                                        personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Couraca, out armadura);
                                        break;
                                    case 3:
                                        personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Helmo, out armadura);
                                        break;
                                    case 4:
                                        personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Luvas, out armadura);
                                        break;
                                }

                                // Se tiver
                                if (armadura != null)
                                {
                                    danoInimigo = AtacarComando.CalcDano(armadura.DefesaFisica, inimigo.AtaqueFisico);
                                    armadura.Durabilidade--;
                                    if (armadura.Durabilidade == 0)
                                    {
                                        personagem.Inventario.DesequiparItem(armadura, personagem);
                                        await ctx.RespondAsync($"**({armadura.Nome})** quebrou! {ctx.User.Mention}!");
                                    }
                                }
                                else
                                    danoInimigo = AtacarComando.CalcDano(0, inimigo.AtaqueFisico);
                                personagem.VidaAtual -= danoInimigo;
                                danoRecebido += danoInimigo;
                            }
                        }
                    }
                    personagem.Batalha.Turno++;
                    personagem.Batalha.PontosDeAcao = 0;

                    StringBuilder mensagemAtaquesInimigos = new StringBuilder();
                    mensagemAtaquesInimigos.Append($"Você perdeu -{danoRecebido.Texto2Casas()} de vida.\n");
                    // Enviamos a mensagem armazenada se ela não for vazia
                    if (danoRecebido != 0)
                        embed.AddField($"**{"Danos recebidos".Titulo()}**", mensagemAtaquesInimigos.ToString());
                    // Adicionamos mais mensagens
                    string t = personagem.Batalha.Turno + "º Turno";
                    embed.WithTitle($"**{t.Titulo()}**");
                    embed.WithDescription($"Você tentou fugir do inimigo mas, não teve sucesso!");
                    embed.WithColor(DiscordColor.Red);
                    await ctx.RespondAsync(embed: embed.Build());
                }
            else
                await ViajandoAbAsync(usuario, enumDirecao, ctx);
            UsuarioRPG.Salvar(usuario);
        }

        public async Task ViajandoAbAsync(UsuarioRPG usuario, DirecaoEnum enumDirecao, CommandContext ctx)
        {
            RegiaoRPG localAtual = usuario.RegiaoGet();
            PersonagemRPG personagem = usuario.Personagem;
            foreach (var regiao in localAtual.SaidasRegioes)
            {
                if (regiao.Direcao == enumDirecao)
                {
                    personagem.LocalAtualId = regiao.RegiaoId;
                    localAtual = usuario.RegiaoGet();
                    DiscordEmbedBuilder embedViajeNormal = new DiscordEmbedBuilder().Padrao("Viajem", ctx);
                    embedViajeNormal.WithDescription($"Você foi para o {enumDirecao.ToString()}.");
                    embedViajeNormal.AddField("----", localAtual.Descrição);
                    if (localAtual.UrlImagem != null)
                        embedViajeNormal.WithThumbnailUrl(localAtual.UrlImagem);
                    await ctx.RespondAsync(embed: embedViajeNormal.Build());
                    if (localAtual.Mobs.Count > 0)
                        if (Sortear.Sucesso(0.5))
                        {
                            List<MobRPG> pesos = localAtual.Mobs;

                            int somaPeso = 0;
                            for (int i = 0; i < pesos.Count; i++)
                            {
                                somaPeso += pesos[i].ChanceDeAparecer;
                            }

                            Random r = new Random();
                            int sorteio = r.Next(0, somaPeso);
                            int posicaoEscolhida = -1;
                            do
                            {
                                posicaoEscolhida++;
                                sorteio -= pesos[posicaoEscolhida].ChanceDeAparecer;
                            } while (sorteio > 0);
                            MobRPG inimigo = pesos[posicaoEscolhida];

                            personagem.Batalha.Inimigos.Add(inimigo);

                            await ctx.RespondAsync($"**<{inimigo.Nome}>** lhe abordou no caminho! {ctx.User.Mention}.");
                        }
                    return;
                }
            }
            await ctx.RespondAsync($"Algo bloqueia a sua passagem! {ctx.User.Mention}.");
        }
    }
}
