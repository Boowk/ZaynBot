using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    [Group("personagem", CanInvokeWithoutSubcommand = true)]
    [Description("Exibe os atributos do seu personagem.\n\n" +
        "Uso: z!personagem")]
    [Aliases("p")]
    public class ComandosPersonagem
    {
        public async Task ExecuteGroupAsync(CommandContext ctx)
        {
            RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioBaseAsync(ctx);
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
            .AddField("⌈Pontos de vida⌋", $"{personagem.PontosDeVida.Texto2Casas()}/{personagem.PontosDeVidaMaxima.Texto2Casas()}", true)
            .AddField("⌈Pontos mágicos⌋", $"{personagem.PontosDeMana.Texto2Casas()}/{personagem.PontosDeManaMaximo.Texto2Casas()}", true)
            .AddField("⌈Ataque físico⌋", $"{personagem.AtaqueFisico.Texto2Casas()}", true)
            .AddField("⌈Defesa física⌋", $"{personagem.DefesaFisica.Texto2Casas()}", true)
            .AddField("⌈Ataque mágico⌋", $"{personagem.AtaqueMagico.Texto2Casas()}", true)
            .AddField("⌈Defesa mágica⌋", $"{personagem.DefesaMagica.Texto2Casas()}", true)
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
        [Description("Exibe os atributos da raça do seu personagem.\n\n" +
        "Uso: z!personagem raca")]
        public async Task ComandoPersonagemRaca(CommandContext ctx)
        {
            RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioBaseAsync(ctx);
            RPGPersonagem personagem = usuario.Personagem;
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Raça do", ctx);
            embed.WithDescription($"**{personagem.Raca.Nome}** Nv.1 [Exp 123/451]");
            embed.AddField("Força".Titulo(), personagem.Raca.Forca.ToString(), true);
            embed.AddField("Inteligência".Titulo(), personagem.Raca.Inteligencia.ToString(), true);
            embed.AddField("Percepção".Titulo(), personagem.Raca.Percepcao.ToString(), true);
            embed.AddField("Destreza".Titulo(), personagem.Raca.Destreza.ToString(), true);
            embed.AddField("Constituição".Titulo(), personagem.Raca.Constituicao.ToString(), true);
            embed.AddField("Sorte".Titulo(), personagem.Raca.Sorte.ToString(), true);
            embed.AddField("Pontos disponíveis".Titulo(), "0");
            embed.WithColor(DiscordColor.Goldenrod);
            await ctx.RespondAsync(embed: embed.Build());
        }

        [Command("missao")]
        [Aliases("m")]
        [Description("Exibe missões concluídas.\n\n" +
       "Uso: z!personagem missao")]
        public async Task ComandoPersonagemMissao(CommandContext ctx)
        {
            RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioBaseAsync(ctx);
            RPGPersonagem personagem = usuario.Personagem;
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Missãoes concluídas", ctx);
            StringBuilder sb = new StringBuilder();
            if (personagem.MissoesConcluidasId.Count != 0)
                foreach (var item in personagem.MissoesConcluidasId)
                    sb.Append($"{ModuloBanco.MissaoConsultar(item).Nome}\n");
            else
                sb.Append("Nenhuma missão concluída.");
            embed.WithDescription(sb.ToString());
            embed.AddField("Pontos de missão".Titulo(), personagem.MissoesConcluidasId.Count.ToString());
            embed.WithColor(DiscordColor.Goldenrod);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
