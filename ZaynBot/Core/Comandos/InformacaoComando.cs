using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.Core.Entidades;

namespace ZaynBot.Core.Comandos
{
    public class InformacaoComando : BaseCommandModule
    {
        [Command("informacao")]
        [Aliases("info")]
        [Description("Exibe uma breve descrição do bot.")]
        [UsoAtributo("informacao")]
        public async Task InformacaoComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordUser criador = await ModuloCliente.Client.GetUserAsync(87604980344721408);
            DiscordUser imain = await ModuloCliente.Client.GetUserAsync(383711472221421589);
            DiscordUser ink = await ModuloCliente.Client.GetUserAsync(477203165641441292);
            DiscordUser yuki = await ModuloCliente.Client.GetUserAsync(459410223480832010);
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();

            float memoria = 0;
            using (Process proc = Process.GetCurrentProcess())
            {
                memoria = (proc.PrivateMemorySize64 / 1024) / 1024;
            }

            embed.WithColor(DiscordColor.Blue);
            embed.Description = "[Servidor oficial](https://discord.gg/GGRnMQu)\n" +
                "[Vote no bot](https://discordbots.org/bot/459873132975620134/vote)\n" +
                "[Código fonte no Github](https://github.com/ZaynBot/ZaynBot)\n";
            embed.WithThumbnailUrl("https://blog.jonygames.com.br/wp-content/uploads/2017/07/RPG-a-sigla-que-mudou-o-mundo-dos-jogos.jpg")
                .AddField("⌈Servidores⌋", $"{BotCore.QuantidadeServidores}", true)
                .AddField("⌈Jogadores⌋", $"{BotCore.QuantidadeMembros}", true)
                .AddField("⌈Tempo ativo⌋", $"**{(DateTime.Now - BotCore.TempoAtivo).Days} dias, " +
                $"{(DateTime.Now - BotCore.TempoAtivo).Hours} horas e {(DateTime.Now - BotCore.TempoAtivo).Minutes} minutos.**", true)
                .AddField("⌈Criador⌋", $"{criador.Username}#{criador.Discriminator}", true)
                .AddField("⌈Testador Beta⌋", $"{imain.Username}#{imain.Discriminator}, {ink.Username}#{ink.Discriminator}", true)
                .AddField("Memoria:", $"{memoria} Mb", true);

            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
