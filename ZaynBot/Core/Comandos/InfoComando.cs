using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.Core.Entidades;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.Core.Comandos
{

    [BsonIgnoreExtraElements]
    class TamanhoResultado
    {
        [BsonElement("dataSize")]
        public double Tamanho { get; set; }
    }

    public class InfoComando : BaseCommandModule
    {
        [Command("info")]
        [Description("Exibe uma breve descrição do bot.")]
        [ComoUsar("info")]
        public async Task InformacaoComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordUser imain = await ModuloCliente.Client.GetUserAsync(383711472221421589);
            DiscordUser yuki = await ModuloCliente.Client.GetUserAsync(459410223480832010);
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            StringBuilder srb = new StringBuilder();
            srb.AppendLine("ZaynBot V. Vinland 1.4")
                .AppendLine("[Servidor oficial](https://discord.gg/GGRnMQu)")
                .AppendLine("[Vote no bot](https://discordbots.org/bot/459873132975620134/vote)")
                .AppendLine("[Código fonte](https://github.com/ZaynBot/ZaynBot)");
            embed.WithDescription(srb.ToString());
            StringBuilder srd = new StringBuilder();
            srd.AppendLine($"Online por: **{(DateTime.Now - BotCore.TempoAtivo).Days} dias, {(DateTime.Now - BotCore.TempoAtivo).Hours} horas e {(DateTime.Now - BotCore.TempoAtivo).Minutes} minutos.**")
                .AppendLine($"Contas criadas: {ModuloBanco.UsuarioColecao.CountDocuments(FilterDefinition<RPGUsuario>.Empty)}")
                .AppendLine($"Ping: {ctx.Client.Ping} pong")
                .AppendLine($"Memoria: {(GC.GetTotalMemory(false) / 1024) / 1024} Mb")
                .AppendLine($"Banco: {Extensoes.Text(((ModuloBanco.Database.RunCommand<TamanhoResultado>("{dbstats: 1}").Tamanho / 1024) / 1024))} Mb");
            embed.AddField("Bot".Titulo(), srd.ToString(), true);
            StringBuilder srf = new StringBuilder();
            srf.AppendLine($"Guildas: {BotCore.QuantidadeServidores}");
            srf.AppendLine($"Canais: {BotCore.QuantidadeCanais}");
            srf.AppendLine($"Usuarios: {BotCore.QuantidadeMembros}");
            embed.AddField("Discord".Titulo(), srf.ToString(), true);
            embed.AddField("Testadores".Titulo(), $"{imain.Username}#{imain.Discriminator}\n{yuki.Username}#{yuki.Discriminator}", true);
            embed.WithColor(DiscordColor.Blue);
            embed.WithThumbnailUrl("https://blog.jonygames.com.br/wp-content/uploads/2017/07/RPG-a-sigla-que-mudou-o-mundo-dos-jogos.jpg");
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
