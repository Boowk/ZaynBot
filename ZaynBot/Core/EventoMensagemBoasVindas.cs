using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ZaynBot.Core.Eventos
{
    public static class EventoMensagemBoasVindas
    {
        //public static async Task EventoBemVindoAsync(GuildMemberAddEventArgs e)
        //{
        //    //if (e.Guild.Id == 508615273515843584)
        //    //{
        //    //    DiscordChannel f = e.Guild.GetChannel(551469878268395530);
        //    //    await f.SendMessageAsync(string.Format(new MensagensEventoBemVindo().Sortear(), e.Member.Mention) + $"\nNão se esqueça de ler as regras!");
        //    //}
        //    if (e.Guild.Id == 420044060720627712)
        //    {
        //        DiscordChannel f = e.Guild.GetChannel(420046160992927744);
        //        DiscordChannel g = e.Guild.GetChannel(592802494577508440);
        //        DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
        //        embed.WithDescription($"Para estar ganhando acesso ao restante do servidor, pedimos que leia todas as regras clicando [aqui](https://discordapp.com/channels/420044060720627712/592802494577508440/594664720389242917) ou indo no {g.Mention}");
        //        embed.WithColor(DiscordColor.Green);
        //        DiscordMessage m = await f.SendMessageAsync($"Seja bem-vindo ao Mundo da Zayn! {e.Member.Mention}!! Divirta-se no nosso servidor!", embed: embed.Build());
        //        await Task.Delay(TimeSpan.FromMinutes(4));
        //        await m.DeleteAsync();
        //    }
        //    await Task.CompletedTask;
        //}
    }

    //public static class MensagensEventoBemVindo
    //{
    //    public static string Sortear()
    //    {
    //        List<string> Mensagens = new List<string>
    //        {
    //            "Batatinha quando nasce espalha rama pelo chão, {0} chegou na área para trazer animação!",
    //            "{0} acabou de entrar. Finjam que estão ocupados!",
    //            "{0} ouviu o zunzunzum sobre a festa de ontem?"   ,
    //            "Estavamos esperando você {0} ( ͡° ͜ʖ ͡°)",
    //            "Bem vindo {0}! Deixe as suas armas na porta.",
    //            "{0} acabou de chegar. Parece OP - nerfa por favor."  ,
    //            "Muhahaha! {0} chegou ativando minha armadilha!!",
    //            "Com calma e jeito, se faz amizade com qualquer sujeito, bem vindo {0}"
    //        };
    //        Random r = new Random();
    //        return Mensagens[r.Next(0, Mensagens.Count)];
    //    }
    //}
}
