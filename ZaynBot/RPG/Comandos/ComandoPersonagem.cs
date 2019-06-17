using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoPersonagem
    {
        [Command("personagem")]
        public async Task ComandoPersonagemAb(CommandContext ctx, DiscordUser membro = null)
        {
            RPGUsuario usuario = await ModuloBanco.UsuarioConsultarPersonagemAsync(ctx);
            if (usuario.Personagem == null) return;
            RPGPersonagem personagem = usuario.Personagem;
            if (membro == null)
            {
                await ctx.RespondAsync(embed: GerarPersonagem(ctx.Member, usuario).Build());
                return;
            }
            if (membro.IsBot)
            {
                if (membro.Id != 459873132975620134)
                {
                    await ctx.RespondAsync($"{ctx.User.Mention}, não gosto dos outros bots! Porquê você não pergunta sobre mim? :(");
                    return;
                }
                await ctx.RespondAsync($"{ctx.User.Mention}, você só precisa saber que o meu poder é mais de 8 mil! :stuck_out_tongue_closed_eyes:");
                return;
            }
            await ctx.RespondAsync("Atenção - Futuramente será necessario a habilidade inspecionar.", embed: GerarPersonagem(membro, ModuloBanco.UsuarioConsultar(membro.Id)).Build());
        }

        private DiscordEmbedBuilder GerarPersonagem(DiscordUser membro, RPGUsuario usuario)
        {
            RPGPersonagem personagem = usuario.Personagem;
            return new DiscordEmbedBuilder()
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"{membro.Username} ⌈Sem título⌋",
                    IconUrl = membro.AvatarUrl
                },
                Title = "⌈Emprego⌋",
                Description = $"**{personagem.Emprego.Nome}** Nv.{personagem.Emprego.Nivel}\nRecomenda-se encontrar um padre e trocar de emprego o mais rápido póssivel.",
                Color = DiscordColor.Blue,
                Timestamp = DateTime.Now,
                ThumbnailUrl = membro.AvatarUrl,
            }
            .AddField("⌈Raça⌋", $"{personagem.Raca.Nome}", true)
            .AddField("⌈Pontos de vida⌋", $"{personagem.PontosDeVida.Texto()}/{personagem.PontosDeVidaMaxima.Texto()}", true)
            .AddField("⌈Pontos mágicos⌋", $"{personagem.PontosDeMana.Texto()}/{personagem.PontosDeManaMaximo.Texto()}", true)
            .AddField("⌈Ataque físico⌋", $"{personagem.AtaqueFisico.Texto()}", true)
            .AddField("⌈Defesa física⌋", $"{personagem.DefesaFisica.Texto()}", true)
            .AddField("⌈Ataque mágico⌋", $"{personagem.AtaqueMagico.Texto()}", true)
            .AddField("⌈Defesa mágica⌋", $"{personagem.DefesaMagica.Texto()}", true)
            .AddField("⌈Velocidade⌋", $"{personagem.Velocidade}", true)
            .AddField("⌈Sorte⌋", $"{personagem.Raca.Sorte}", true)
            .AddField("⌈Equipamento⌋", $"Em contrução", true)
            .AddField("⌈Habiliades⌋", $"Em contrução", true)
            .AddField("⌈Títulos adquiridos⌋", $"Em contrução", true)
            .AddField("⌈Empregos disponíveis⌋", $"Em contrução", true)
            .AddField("⌈Bêncãos⌋", $"Nenhuma", true);
        }

    }
}
