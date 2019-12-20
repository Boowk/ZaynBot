using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoStatus : BaseCommandModule
    {
        [Command("status")]
        [Description("Exibe os status do seu personagem.")]
        [ComoUsar("status")]
        public async Task ComandoStatusAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
            RPGPersonagem personagem = usuario.Personagem;

            DiscordEmoji pv = DiscordEmoji.FromGuildEmote(ModuloCliente.Client, 631907691467636736);
            DiscordEmoji pp = DiscordEmoji.FromGuildEmote(ModuloCliente.Client, 631907691425562674);

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao($"Nível {personagem.NivelAtual}| Exp: {personagem.ExpAtual.Text()}/{personagem.ExpMax.Text()}", ctx);
            embed.WithColor(DiscordColor.PhthaloGreen);
            embed.AddField(pv + "Vida".Titulo(), $"{personagem.VidaAtual.Text()}/{personagem.VidaMaxima.Text()}", true);
            embed.AddField(pp + "Magia".Titulo(), $"{personagem.MagiaAtual.Text()}/{personagem.MagiaMaxima.Text()}", true);
            embed.AddField("Ataque físico".Titulo(), $"{personagem.AtaqueFisico.Text()}", true);
            embed.AddField("Ataque mágico".Titulo(), $"{personagem.AtaqueMagico.Text()}", true);
            embed.AddField("Defesa física".Titulo(), $"{personagem.DefesaFisica.Text()}", true);
            embed.AddField("Defesa mágica".Titulo(), $"{personagem.DefesaMagica.Text()}", true);
            embed.AddField("Velocidade".Titulo(), $"{personagem.Velocidade.Text()}", true);
            embed.AddField("Sorte".Titulo(), $"{personagem.Sorte.Text()}", true);
            embed.AddField("Fome".Titulo(), $"{((personagem.FomeAtual / personagem.FomeMaxima) * 100).Text()}%", true);
            embed.AddField("Sede".Titulo(), $"{((personagem.SedeAtual / personagem.SedeMaxima) * 100).Text()}%", true);
            embed.AddField("Rip".Titulo(), $"{usuario.Rip}", true);
            embed.AddField("Rip Mobs".Titulo(), $"{usuario.RipMobs}", true);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}