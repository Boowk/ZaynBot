using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Exceptions;
using DSharpPlus.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZaynBot.Core.Entidades;
using ZaynBot.Core.Eventos;
using ZaynBot.RPG;

namespace ZaynBot
{
    public class ModuloCliente
    {
        public static DiscordClient Client { get; private set; }
        public ModuloCliente(DiscordConfiguration discordConfiguration)
        {
            Client = new DiscordClient(discordConfiguration);
            Client.Ready += Client_Ready;
            Client.GuildAvailable += Client_GuildAvailable;
            Client.ClientErrored += Client_ClientError;
            Client.MessageCreated += Client_MessageCreated;
            Client.GuildMemberAdded += Client_GuildMemberAdded;
            Client.MessageReactionAdded += Client_MessageReactionAdded;
            Client.MessageReactionRemoved += Client_MessageReactionRemoved;
            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(1)
            });
        }

        private async Task Client_MessageReactionRemoved(MessageReactionRemoveEventArgs e)
        {
            if (e.Message.Id == 594667356622684191)
            {
                if (e.Emoji.GetDiscordName() == ":white_check_mark:")
                {
                    DiscordGuild guildaZayn = await Client.GetGuildAsync(420044060720627712);
                    var verificado = guildaZayn.Roles.FirstOrDefault(x => x.Id == 592837208247894046);
                    DiscordMember membro = await guildaZayn.GetMemberAsync(e.User.Id);
                    await membro.RevokeRoleAsync(verificado);

                }
            }
            await Task.CompletedTask;
        }

        private async Task Client_MessageReactionAdded(MessageReactionAddEventArgs e)
        {
            if (e.Message.Id == 594667356622684191)
            {
                if (e.Emoji.GetDiscordName() == ":white_check_mark:")
                {
                    DiscordGuild guildaZayn = await Client.GetGuildAsync(420044060720627712);
                    var verificado = guildaZayn.Roles.FirstOrDefault(x => x.Id == 592837208247894046);
                    DiscordMember membro = await guildaZayn.GetMemberAsync(e.User.Id);
                    await membro.GrantRoleAsync(verificado);
                }
            }
            await Task.CompletedTask;
        }

        private async Task Client_GuildMemberAdded(GuildMemberAddEventArgs e)
        {
            await EventoMensagemBoasVindas.EventoBemVindoAsync(e);
        }

        //private async Task DoWorkAsyncInfiniteLoop()
        //{
        //    while (true)
        //    {
        //        FazerSemParar();
        //        await Task.Delay(TimeSpan.FromMinutes(5));

        //    }
        //}

        //private async Task FazerSemParar()
        //{

        //    Console.WriteLine($"Cache com {BancoDeDados.CacheUsuarios.Count} usuarios!");
        //    Console.WriteLine($"Cache limpo: {BancoDeDados.SalvarUsuarios()} usuarios salvos!");

        //DiscordGuild f = await Client.GetGuildAsync(508615273515843584);
        //DiscordChannel g = f.GetChannel(551469878268395530);
        //Random r = new Random();
        //string imagem = "";
        //string resposta = "";
        //switch (r.Next(0, 5))
        //{
        //    case 0:
        //        imagem = "https://cdn.discordapp.com/attachments/519347764547813388/552232509996531713/1546396750586.png";
        //        resposta = "Lucas";
        //        break;
        //    case 1:
        //        imagem = "https://cdn.discordapp.com/attachments/519347764547813388/552232585145745409/leogamer4.PNG";
        //        resposta = "Leo gamer";
        //        break;
        //    case 2:
        //        imagem = "https://cdn.discordapp.com/attachments/519347764547813388/552232619899748393/z.jpg";
        //        resposta = "Fusion";
        //        break;
        //    case 3:
        //        imagem = "https://cdn.discordapp.com/attachments/551469878268395530/552248135326367774/Screenshot_15.png";
        //        resposta = "José italo";
        //        break;
        //    case 4:
        //        imagem = "https://cdn.discordapp.com/attachments/552242642373836811/552242661877219348/JPEG_20181117_000919.jpg";
        //        resposta = "Fusion 2";
        //        break;
        //}
        //DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
        //embed.WithImageUrl(imagem);
        //embed.WithTitle($"**{resposta} apareceu!**");
        //embed.WithColor(DiscordColor.Red);
        //await g.SendMessageAsync(embed: embed.Build());
        //var i = Client.GetInteractivityModule();
        //var m = await i.WaitForMessageAsync(
        //    x => x.Channel == g
        //    && x.Content == "bang!", TimeSpan.FromSeconds(20));

        //if (m == null)
        //{
        //    await g.SendMessageAsync($"**O {resposta} fugiu!**");
        //}
        //else
        //if (m.Message.Content == "bang!")
        //{
        //    DiscordEmbedBuilder emb = new DiscordEmbedBuilder();
        //    emb.WithImageUrl("https://media.tenor.com/images/a6405b266a920c98b482b17defa3c44f/tenor.gif");
        //    await g.SendMessageAsync($"**{m.User.Mention} matou o {resposta}!**", embed: emb.Build());
        //}

        //    await Task.CompletedTask;
        //}

        private async Task Client_MessageCreated(MessageCreateEventArgs e)
        {
            if (e.Message.Author.IsBot) return;
            if (e.Message.MessageType == MessageType.GuildMemberJoin) return;
            try
            {
                await EvoluirNivelPorMensagem.ReceberXPNivelMensagens(e);
            }
            catch (Exception ex)
            {
                if (ex is UnauthorizedException || ex is AggregateException)
                {
                    e.Client.DebugLogger.LogMessage(LogLevel.Info, e.Guild.Name, $"Sem permissão para falar no canal {e.Channel.Name}.", DateTime.Now);
                }
            }
            await Task.CompletedTask;
        }

        private Task Client_Ready(ReadyEventArgs e)
        {
            e.Client.DebugLogger.LogMessage(LogLevel.Info, "ZAYN", "Cliente está pronto para processar eventos.", DateTime.Now);
            return Task.CompletedTask;
        }

        private Task Client_GuildAvailable(GuildCreateEventArgs e)
        {
            e.Client.DebugLogger.LogMessage(LogLevel.Info, "ZAYN", $"Guilda disponível: {e.Guild.Name}", DateTime.Now);
            CoreBot.QuantidadeServidores++;
            CoreBot.QuantidadeMembros += e.Guild.MemberCount;
            CoreBot.QuantidadeCanais += e.Guild.Channels.Count;
            return Task.CompletedTask;
        }

        private Task Client_ClientError(ClientErrorEventArgs e)
        {
            e.Client.DebugLogger.LogMessage(LogLevel.Error, "", $"Um erro aconteceu: {e.Exception.GetType()}: {e.Exception.Message}", DateTime.Now);
            return Task.CompletedTask;
        }
    }
}
