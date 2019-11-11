using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Comandos.Exibir
{
    public class StatusComando : BaseCommandModule
    {
        [Command("status")]
        [Description("Exibe os status do seu personagem.")]
        [UsoAtributo("status")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task StatusComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.GetPersonagem(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;

            DiscordEmoji pv = DiscordEmoji.FromGuildEmote(ModuloCliente.Client, 631907691467636736);
            DiscordEmoji pp = DiscordEmoji.FromGuildEmote(ModuloCliente.Client, 631907691425562674);

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Status", ctx);
            embed.WithColor(DiscordColor.PhthaloGreen);
            embed.AddField(pv + "**Vida**".Titulo(), $"{personagem.VidaAtual.Texto2Casas()}/{personagem.VidaMaxima.Texto2Casas()}", true);
            embed.AddField(pp + "**Magia**".Titulo(), $"{personagem.MagiaAtual.Texto2Casas()}/{personagem.MagiaMaxima.Texto2Casas()}", true);
            embed.AddField("**Ataque físico**".Titulo(), $"{personagem.AtaqueFisico.Texto2Casas()}", true);
            embed.AddField("**Ataque mágico**".Titulo(), $"{personagem.AtaqueMagico.Texto2Casas()}", true);
            embed.AddField("**Defesa física**".Titulo(), $"{personagem.DefesaFisica.Texto2Casas()}", true);
            embed.AddField("**Defesa mágica**".Titulo(), $"{personagem.DefesaMagica.Texto2Casas()}", true);
            embed.AddField("**Velocidade**".Titulo(), $"{personagem.Velocidade.Texto2Casas()}", true);
            embed.AddField("**Sorte**".Titulo(), $"{personagem.Sorte.Texto2Casas()}", true);
            embed.AddField("**Fome**".Titulo(), $"{(personagem.FomeAtual / personagem.FomeMaxima) * 100}%", true);
            embed.AddField("**Sede**".Titulo(), $"{(personagem.SedeAtual / personagem.SedeMaxima) * 100}%", true);
            embed.AddField("**Estamina**".Titulo(), $"{personagem.EstaminaMaxima.Texto2Casas()}", true);
            embed.AddField("**Peso**".Titulo(), $"{personagem.PesoAtual.Texto2Casas()}/{personagem.PesoMaximo.Texto2Casas()}", true);

            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}