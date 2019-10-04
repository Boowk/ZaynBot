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
                    await ViajandoAbAsync(usuario, enumDirecao, ctx);
                }
                else
                {
                    DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Combate", ctx);
                    double danoRecebido = await AtacarComando.CalcBatalhaMobsAsync(personagem, ctx);
                    // Enviamos a mensagem armazenada se ela não for vazia
                    if (danoRecebido != 0)
                        embed.AddField($"**{"Danos recebidos".Titulo()}**", $"Você perdeu -{danoRecebido.Texto2Casas()} de vida.");
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
                            MobRPG mobSorteado = ExplorarComando.MobProcurar(localAtual, personagem, ctx);

                            await ctx.RespondAsync($"**<{mobSorteado.Nome}>** lhe abordou no caminho! {ctx.User.Mention}.");
                        }
                    return;
                }
            }
            await ctx.RespondAsync($"Algo bloqueia a sua passagem! {ctx.User.Mention}.");
        }
    }
}
