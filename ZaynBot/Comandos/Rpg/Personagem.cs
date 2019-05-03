using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Entidades;
using ZaynBot.Funções;

namespace ZaynBot.Comandos.Rpg
{
    public class Personagem
    {
        private Usuario _userDep;
        public Personagem(Usuario userDep)
        {
            _userDep = userDep;
        }


        [Command("personagem")]                    
        public async Task PersonagemAb(CommandContext ctx, DiscordUser membro = null)
        {
            if (membro == null)
            {
                await ctx.RespondAsync(embed: GerarPersonagem(ctx.Member, _userDep).Build());
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
            await ctx.RespondAsync("Atenção - Futuramente será necessario a habilidade inspecionar.", embed: GerarPersonagem(membro, Banco.ConsultarUsuario(membro)).Build());
        }

        private DiscordEmbedBuilder GerarPersonagem(DiscordUser membro, Usuario usuario)
        {
            ZaynBot.Entidades.Rpg.Personagem personagem = usuario.Personagem;
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
            .AddField("⌈Raça⌋", $"{personagem.Raça}", true)
            .AddField("⌈Pontos de vida⌋", $"{personagem.PontosDeVida}/{personagem.PontosDeVidaMaxima}", true)
            .AddField("⌈Pontos mágicos⌋", $"{personagem.PontosDeMana}/{personagem.PontosDeManaMaximo}", true)
            .AddField("⌈Ataque físico⌋", $"{personagem.AtaqueFisico}", true)
            .AddField("⌈Defesa física⌋", $"{personagem.DefesaFisica}", true)
            .AddField("⌈Ataque mágico⌋", $"{personagem.AtaqueMagico}", true)
            .AddField("⌈Defesa mágica⌋", $"{personagem.DefesaMagica}", true)
            .AddField("⌈Velocidade⌋", $"{personagem.Velocidade}", true)
            .AddField("⌈Sorte⌋", $"{personagem.Sorte}", true)
            .AddField("⌈Equipamento⌋", $"Em contrução", true)
            .AddField("⌈Habiliades⌋", $"Em contrução", true)
            .AddField("⌈Títulos adquiridos⌋", $"Em contrução", true)
            .AddField("⌈Empregos disponíveis⌋", $"Em contrução", true)
            .AddField("⌈Bêncãos⌋", $"Nenhuma", true);
        }
    }
}
