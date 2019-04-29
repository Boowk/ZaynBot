using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using System;
using System.Threading.Tasks;
using ZaynBot.Entidades;
using ZaynBot.Eventos;
using ZaynBot.Funções;

namespace ZaynBot
{
    public class Cliente
    {
        public static DiscordClient Client { get; private set; }
        Usuario user;
        public Cliente(DiscordConfiguration discordConfiguration, Usuario user)
        {
            Client = new DiscordClient(discordConfiguration);
            Client.Ready += Client_Ready;
            Client.GuildAvailable += Client_GuildAvailable;
            Client.ClientErrored += Client_ClientError;
            Client.MessageCreated += Client_MessageCreated;
            Client.GuildMemberAdded += Client_GuildMemberAdded;
            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(1)
            });
            this.user = user;
        }

        private async Task Client_GuildMemberAdded(GuildMemberAddEventArgs e)
        {
            await UsuarioNovoEntrouGuilda.EventoBemVindoAsync(e);
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
            user.Copiar(Banco.ConsultarUsuario(e.Author.Id));
            await MensagemNovaEnviada.XpUsuario(e, user);
            //Console.WriteLine($"[{DateTime.Now}] [{e.Guild.Name}] [{e.Message.Author}] {e.Message.Content}");
            // await Task.CompletedTask;
        }

        private Task Client_Ready(ReadyEventArgs e)
        {
            e.Client.DebugLogger.LogMessage(LogLevel.Info, "RPG", "Cliente está pronto para processar eventos.", DateTime.Now);

            return Task.CompletedTask;

        }

        private Task Client_GuildAvailable(GuildCreateEventArgs e)
        {
            e.Client.DebugLogger.LogMessage(LogLevel.Info, "RPG", $"Guilda disponível: {e.Guild.Name}", DateTime.Now);
            return Task.CompletedTask;
        }

        private Task Client_ClientError(ClientErrorEventArgs e)
        {
            e.Client.DebugLogger.LogMessage(LogLevel.Error, "ERRO", $"Um erro aconteceu: {e.Exception.GetType()}: {e.Exception.Message}", DateTime.Now);

            return Task.CompletedTask;
        }
    }
}
