using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoBatalha : BaseCommandModule
    {
        [Command("batalha")]
        [Description("Exibe o status da batalha atual.")]
        [ComoUsar("batalha")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task BatalhaComandoAb(CommandContext ctx)
        {
            //await ctx.TriggerTypingAsync();
            //RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
            //RPGBatalha batalha = new RPGBatalha();

            ////Caso não tenha grupo
            //if (usuario.Personagem.Batalha.LiderGrupo == 0)
            //{
            //    await ctx.RespondAsync($"Você deve criar um Grupo antes! {ctx.User.Mention}.");
            //    return;
            //}

            ////Caso o lider do grupo não seja ele
            //if (usuario.Personagem.Batalha.LiderGrupo != ctx.User.Id)
            //{
            //    RPGUsuario liderUsuario = await RPGUsuario.UsuarioGetAsync(usuario.Personagem.Batalha.LiderGrupo);
            //    batalha = liderUsuario.Personagem.Batalha;
            //}

            ////Caso ele seja o lider
            //if (usuario.Personagem.Batalha.LiderGrupo == ctx.User.Id)
            //    batalha = usuario.Personagem.Batalha;

            //if (batalha.MobsVivos.Count == 0 && batalha.MobsMortos.Count == 0)
            //{
            //    await ctx.RespondAsync($"{ctx.User.Mention}, seu grupo não tem nenhuma batalha em andamento!");
            //    return;
            //}

            //DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Batalha", ctx);
            //embed.WithColor(DiscordColor.Green);
            //embed.WithTitle(batalha.NomeGrupo.Titulo());
            //embed.WithDescription($"**Turno: {batalha.Turno.ToString()}**");


            //StringBuilder strPronto = new StringBuilder();
            //foreach (var users in batalha.Membros)
            //{
            //    var u = ModuloBanco.GetUsuario(users);
            //    u.DiscordUser = await ctx.Client.GetUserAsync(users);

            //    if (u.Personagem.VidaAtual > 0)
            //        if (u.Personagem.EstaminaAtual >= u.Personagem.EstaminaMaxima)
            //            strPronto.AppendLine($"**{u.DiscordUser.Mention} {DiscordEmoji.FromName(ctx.Client, ":white_check_mark:")} {DiscordEmoji.FromName(ctx.Client, ":hearts:")} {Extensoes.Text(u.Personagem.VidaAtual)}/{Extensoes.Text(u.Personagem.VidaMaxima)}**");
            //        else
            //            strPronto.AppendLine($"**{u.DiscordUser.Mention} {DiscordEmoji.FromName(ctx.Client, ":stopwatch:")}  {DiscordEmoji.FromName(ctx.Client, ":hearts:")} {Extensoes.Text(u.Personagem.VidaAtual)}/{Extensoes.Text(u.Personagem.VidaMaxima)}**");
            //    else
            //        strPronto.AppendLine($"**{DiscordEmoji.FromName(ctx.Client, ":skull_crossbones:")} {u.DiscordUser.Mention} {DiscordEmoji.FromName(ctx.Client, ":skull_crossbones:")}**");
            //}

            //var uf = ModuloBanco.GetUsuario(batalha.LiderGrupo);
            //uf.DiscordUser = await ctx.Client.GetUserAsync(batalha.LiderGrupo);
            //if (uf.Personagem.VidaAtual > 0)
            //    if (uf.Personagem.EstaminaAtual >= uf.Personagem.EstaminaMaxima)
            //        strPronto.AppendLine($"**{uf.DiscordUser.Mention} {DiscordEmoji.FromName(ctx.Client, ":white_check_mark:")} {DiscordEmoji.FromName(ctx.Client, ":hearts:")} {Extensoes.Text(uf.Personagem.VidaAtual)}/{Extensoes.Text(uf.Personagem.VidaMaxima)}**");
            //    else
            //        strPronto.AppendLine($"**{uf.DiscordUser.Mention} {DiscordEmoji.FromName(ctx.Client, ":stopwatch:")}  {DiscordEmoji.FromName(ctx.Client, ":hearts:")} {Extensoes.Text(uf.Personagem.VidaAtual)}/{Extensoes.Text(uf.Personagem.VidaMaxima)}**");
            //else
            //    strPronto.AppendLine($"**{DiscordEmoji.FromName(ctx.Client, ":skull_crossbones:")} {uf.DiscordUser.Mention} {DiscordEmoji.FromName(ctx.Client, ":skull_crossbones:")}**");
            //embed.AddField("Membros".Titulo(), strPronto.ToString(), true);


            ////Colocar bolinhas e trocar por porcentagem
            //StringBuilder strMobs = new StringBuilder();
            //foreach (var mob in batalha.MobsVivos)
            //    strMobs.AppendLine($"{mob.Key.PrimeiraLetraMaiuscula()} {DiscordEmoji.FromName(ctx.Client, ":hearts:")} {Extensoes.Text(mob.Value.PontosDeVida)}");


            //foreach (var mob in batalha.MobsMortos)
            //    strMobs.AppendLine($"{DiscordEmoji.FromName(ctx.Client, ":skull_crossbones:")} **{mob.Value.Nome.PrimeiraLetraMaiuscula()}** {DiscordEmoji.FromName(ctx.Client, ":skull_crossbones:")}");
            //embed.AddField("Mobs".Titulo(), strMobs.ToString(), true);


            //await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
