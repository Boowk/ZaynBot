using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Exibir
{
    public class StatusComando : BaseCommandModule
    {
        [Command("status")]
        [Description("Exibe os atributos derivados do seu personagem.")]
        [UsoAtributo("status")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task StatusComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.TryGetPersonagemRPG(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder()
            {
                Author = new DiscordEmbedBuilder.EmbedAuthor()
                {
                    Name = $"{ctx.User.Username}",
                    IconUrl = ctx.User.AvatarUrl
                },
                Color = DiscordColor.Blue,
                Timestamp = DateTime.Now,
                ThumbnailUrl = ctx.User.AvatarUrl,
            }.AddField("Raça".Titulo(), $"{personagem.Raca.Nome.PrimeiraLetraMaiuscula()}", true)
            .AddField("Nível".Titulo(), $"Nv.{personagem.NivelAtual}", true)
            .AddField("Experiencia".Titulo(), $"{personagem.ExpAtual.Texto2Casas()}/{personagem.ExpMax.Texto2Casas()}", true)
            .AddField("Vida".Titulo(), $"{personagem.VidaAtual.Texto2Casas()}/{personagem.VidaMax.Texto2Casas()}", true)
            .AddField("Mágia".Titulo(), $"{personagem.MagiaAtual.Texto2Casas()}/{personagem.MagiaMax.Texto2Casas()}", true)
            .AddField("Fome".Titulo(), $"{personagem.FomeAtual.Texto2Casas()}/{personagem.FomeMax.Texto2Casas()}", true);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
