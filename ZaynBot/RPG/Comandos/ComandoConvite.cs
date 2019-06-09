using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Threading.Tasks;
using ZaynBot.Core.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoConvite
    {
        [Command("convite")]
        [Description("Envia uma serie de link sobre o bot.")]
        public async Task ComandoConviteAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordUser criador = await ModuloCliente.Client.GetUserAsync(87604980344721408);
            DiscordUser imain = await ModuloCliente.Client.GetUserAsync(383711472221421589);
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithColor(DiscordColor.Blue);
            embed.Description = "[Servidor oficial - Mundo da Zayn](https://discord.gg/GGRnMQu)\n" +
                "[Adicionar bot no seu servidor](https://discordapp.com/api/oauth2/authorize?client_id=459873132975620134&permissions=469887175&scope=bot)\n" +
                "[Vote no bot e ganhe uma recompensa](https://discordbots.org/bot/459873132975620134/vote)\n" +
                "[Código fonte no Github](https://github.com/ZaynBot/ZaynBot)\n" +
                "[Nossa wiki](https://github.com/ZaynBot/wiki/wiki)";
            embed.WithThumbnailUrl("https://blog.jonygames.com.br/wp-content/uploads/2017/07/RPG-a-sigla-que-mudou-o-mundo-dos-jogos.jpg")
                .AddField("Servidores totais", $"{CoreBot.QuantidadeServidores}", true)
                .AddField("Membros totais", $"{CoreBot.QuantidadeMembros}", true)
                .AddField("Canais totais", $"{CoreBot.QuantidadeCanais}", true)
                .AddField("Tempo ativo sem reiniciar!", $"**{(DateTime.Now - CoreBot.TempoAtivo).Days} dias, {(DateTime.Now - CoreBot.TempoAtivo).Hours} horas e {(DateTime.Now - CoreBot.TempoAtivo).Minutes} minutos.**", true)
                .AddField("Créditos", $"Criador - {criador.Mention}\n" +
                $"Tester - {imain.Mention}");

            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
