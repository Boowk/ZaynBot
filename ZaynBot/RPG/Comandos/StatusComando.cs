using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Comandos
{
    public class StatusComando : BaseCommandModule
    {
        [Command("status")]
        [Description("Exibe os status do seu personagem.")]
        [ComoUsar("status")]
        public async Task StatusComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
            RPGPersonagem personagem = usuario.Personagem;

            DiscordEmoji pv = DiscordEmoji.FromGuildEmote(ModuloCliente.Client, 631907691467636736);
            DiscordEmoji pp = DiscordEmoji.FromGuildEmote(ModuloCliente.Client, 631907691425562674);

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao($"Nível {personagem.NivelAtual}| Exp: {personagem.ExpAtual}/{personagem.ExpMax}", ctx);
            embed.WithColor(DiscordColor.PhthaloGreen);
            embed.AddField(pv + "Vida".Titulo(), $"{Extensoes.Text(personagem.VidaAtual)}/{Extensoes.Text(personagem.VidaMaxima)}", true);
            embed.AddField(pp + "Magia".Titulo(), $"{Extensoes.Text(personagem.MagiaAtual)}/{Extensoes.Text(personagem.MagiaMaxima)}", true);
            embed.AddField("Ataque físico".Titulo(), $"{Extensoes.Text(personagem.AtaqueFisico)}", true);
            embed.AddField("Ataque mágico".Titulo(), $"{Extensoes.Text(personagem.AtaqueMagico)}", true);
            embed.AddField("Defesa física".Titulo(), $"{Extensoes.Text(personagem.DefesaFisica)}", true);
            embed.AddField("Defesa mágica".Titulo(), $"{Extensoes.Text(personagem.DefesaMagica)}", true);
            embed.AddField("Velocidade".Titulo(), $"{Extensoes.Text(personagem.Velocidade)}", true);
            embed.AddField("Sorte".Titulo(), $"{Extensoes.Text(personagem.Sorte)}", true);
            embed.AddField("Fome".Titulo(), $"{Extensoes.Text(((personagem.FomeAtual / personagem.FomeMaxima) * 100))}%", true);
            embed.AddField("Sede".Titulo(), $"{Extensoes.Text(((personagem.SedeAtual / personagem.SedeMaxima) * 100))}%", true);
            embed.AddField("Estamina".Titulo(), $"{Extensoes.Text(personagem.EstaminaAtual)}/{Extensoes.Text(personagem.EstaminaMaxima)}", true);
            embed.AddField("Peso".Titulo(), $"{Extensoes.Text(personagem.Inventario.PesoAtual)}/{Extensoes.Text(personagem.Inventario.PesoMaximo)}", true);

            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}