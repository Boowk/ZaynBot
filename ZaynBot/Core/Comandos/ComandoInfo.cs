using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Entidades;

namespace ZaynBot.Core.Comandos
{
    public class ComandoInfo
    {
        [Command("informacao")]
        [Aliases("info")]
        [Description("Exibe uma breve descrição do bot.\n\n" +
          "Uso: z!informacao")]
        public async Task ComandoInfoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordUser criador = await ModuloCliente.Client.GetUserAsync(87604980344721408);
            DiscordUser imain = await ModuloCliente.Client.GetUserAsync(383711472221421589);
            DiscordUser ink = await ModuloCliente.Client.GetUserAsync(477203165641441292);
            DiscordUser yuki = await ModuloCliente.Client.GetUserAsync(459410223480832010);
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithColor(DiscordColor.Blue);
            embed.Description = "[Servidor oficial](https://discord.gg/GGRnMQu)\n" +
                "[Vote no bot](https://discordbots.org/bot/459873132975620134/vote)\n" +
                "[Código fonte no Github](https://github.com/ZaynBot/ZaynBot)\n";
            embed.WithThumbnailUrl("https://blog.jonygames.com.br/wp-content/uploads/2017/07/RPG-a-sigla-que-mudou-o-mundo-dos-jogos.jpg")
                .AddField("⌈Servidores⌋", $"{CoreBot.QuantidadeServidores}", true)
                .AddField("⌈Jogadores⌋", $"{CoreBot.QuantidadeMembros}", true)
                .AddField("⌈Tempo ativo⌋", $"**{(DateTime.Now - CoreBot.TempoAtivo).Days} dias, " +
                $"{(DateTime.Now - CoreBot.TempoAtivo).Hours} horas e {(DateTime.Now - CoreBot.TempoAtivo).Minutes} minutos.**", true)
                .AddField("⌈Criador⌋", $"{criador.Username}#{criador.Discriminator}", true)
                .AddField("⌈Testador Beta⌋", $"{imain.Username}#{imain.Discriminator}, {ink.Username}#{ink.Discriminator}, " +
                $"{yuki.Username}#{yuki.Discriminator}", true);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
