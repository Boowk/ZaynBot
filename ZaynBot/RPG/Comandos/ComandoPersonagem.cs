using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    [Group("personagem", CanInvokeWithoutSubcommand = true)]
    [Description("Exibe os atributos do personagem.")]
    [Aliases("p")]
    public class ComandoPersonagem
    {
        public async Task ExecuteGroupAsync(CommandContext ctx)
        {
            RPGUsuario usuario = await ModuloBanco.UsuarioConsultarPersonagemAsync(ctx);
            if (usuario.Personagem == null) return;
            RPGPersonagem personagem = usuario.Personagem;
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"{ctx.User.Username} ⌈Sem título⌋",
                    IconUrl = ctx.User.AvatarUrl
                },
                Title = "⌈Emprego⌋",
                Description = $"**{personagem.Emprego.Nome}** Nv.{personagem.Emprego.Nivel}\nRecomenda-se encontrar um padre e trocar de emprego o mais rápido póssivel.",
                Color = DiscordColor.Blue,
                Timestamp = DateTime.Now,
                ThumbnailUrl = ctx.User.AvatarUrl,
            }.AddField("⌈Raça⌋", $"{personagem.Raca.Nome}", true)
            .AddField("⌈Pontos de vida⌋", $"{personagem.PontosDeVida.Texto()}/{personagem.PontosDeVidaMaxima.Texto()}", true)
            .AddField("⌈Pontos mágicos⌋", $"{personagem.PontosDeMana.Texto()}/{personagem.PontosDeManaMaximo.Texto()}", true)
            .AddField("⌈Ataque físico⌋", $"{personagem.AtaqueFisico.Texto()}", true)
            .AddField("⌈Defesa física⌋", $"{personagem.DefesaFisica.Texto()}", true)
            .AddField("⌈Ataque mágico⌋", $"{personagem.AtaqueMagico.Texto()}", true)
            .AddField("⌈Defesa mágica⌋", $"{personagem.DefesaMagica.Texto()}", true)
            .AddField("⌈Velocidade⌋", $"{personagem.Velocidade}", true)
            .AddField("⌈Sorte⌋", $"{personagem.Raca.Sorte}", true)
            //.AddField("⌈Equipamento⌋", $"Em contrução", true)
            //.AddField("⌈Habiliades⌋", $"Em contrução", true)
            //.AddField("⌈Títulos adquiridos⌋", $"Em contrução", true)
            //.AddField("⌈Empregos disponíveis⌋", $"Em contrução", true)
            .AddField("⌈Bêncãos⌋", $"Nenhuma", true);
            await ctx.RespondAsync(embed: embed.Build());
        }

        [Command("raca")]
        [Aliases("r")]
        public async Task ComandoPersonagemRaca(CommandContext ctx)
        {
            RPGUsuario usuario = await ModuloBanco.UsuarioConsultarPersonagemAsync(ctx);
            if (usuario.Personagem == null) return;
            RPGPersonagem personagem = usuario.Personagem;
            RPGEmbed embed = new RPGEmbed(ctx, "Raça do");
            embed.Embed.WithDescription($"**{personagem.Raca.Nome}** Nv.1 [Exp 123/451]");
            embed.Embed.AddField("Força".Titulo(), personagem.Raca.Forca.ToString(), true);
            embed.Embed.AddField("Inteligência".Titulo(), personagem.Raca.Inteligencia.ToString(), true);
            embed.Embed.AddField("Percepção".Titulo(), personagem.Raca.Percepcao.ToString(), true);
            embed.Embed.AddField("Destreza".Titulo(), personagem.Raca.Destreza.ToString(), true);
            embed.Embed.AddField("Constituição".Titulo(), personagem.Raca.Constituicao.ToString(), true);
            embed.Embed.AddField("Sorte".Titulo(), personagem.Raca.Sorte.ToString(), true);
            embed.Embed.AddField("Pontos disponíveis".Titulo(), "0");
            embed.Embed.WithColor(DiscordColor.Goldenrod);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
