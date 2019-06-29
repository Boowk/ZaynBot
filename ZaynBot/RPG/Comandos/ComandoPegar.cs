using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoPegar
    {
        [Command("pegar")]
        [Description("Permite pegar um objeto do local atual.\n\n" +
            "Uso: z!pegar [nome]\n\n" +
            "Exemplo: z!pegar espada")]
        public async Task VerInimigos(CommandContext ctx, [RemainingText]string nome)
        {
            RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioComPersonagemAsync(ctx);
            if (string.IsNullOrWhiteSpace(nome))
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você não ira conseguir pegar o vento.");
                return;
            }
            RPGPersonagem personagem = usuario.Personagem;
            RPGRegiao localAtual = usuario.GetRPGRegiao();
            localAtual.Itens.TryGetValue(nome.ToLower(), out RPGItem item);
            if (item == null)
            {
                await ctx.RespondAsync($"Utilize z!local, se encontrar algum *item* tente, usar esse comando, {ctx.User.Mention}.");
                return;
            }

            if (item.PegarSomenteComMissaoEmAndamento)
            {
                if (personagem.MissaoEmAndamento != null)
                {
                    if (personagem.MissaoEmAndamento.Id == item.PegarSomenteComMissaoEmAndamentoId)
                    {
                        await ctx.RespondAsync($"{ctx.User.Mention}, você pegou o `{item.Nome.PrimeiraLetraMaiuscula()}`.");
                        if (item.DesapareceAoPegar)
                        {
                            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Evento", ctx);
                            embed.WithDescription(item.DesapareceAoPegarMensagem);
                            await ctx.RespondAsync(embed: embed.Build());
                        }
                        if (item.CompletaMissaoAoPegar)
                        {
                            await ctx.RespondAsync($"{ctx.User.Mention}, você completou a missão `{personagem.MissaoEmAndamento.Nome}`.");
                            personagem.MissoesConcluidasId.Add(personagem.MissaoEmAndamento.Id);
                            personagem.MissaoEmAndamento = null;
                            ModuloBanco.UpdateUsuario(usuario);
                            return;
                        }
                        return;
                    }
                }
                await ctx.RespondAsync($"{ctx.User.Mention} - {item.PegarSomenteComMissaoEmAndamentoMensagem}");
                return;
            }
        }
    }
}
