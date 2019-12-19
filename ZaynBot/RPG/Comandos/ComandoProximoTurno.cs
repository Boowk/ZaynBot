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
    public class ComandoProximoTurno : BaseCommandModule
    {
        [Command("proximo-turno")]
        [Aliases("pt")]
        [Description("Permite iniciar o proximo turno")]
        [ComoUsar("proximo-turno")]
        [Cooldown(1, 2, CooldownBucketType.User)]
        public async Task ProximoTurno(CommandContext ctx)
        {
         //        bool vezJogador = false;
        //        StringBuilder strVezJog = new StringBuilder();
        //        do
        //        {
        //            foreach (var mob in lider.Personagem.Batalha.MobsVivos)
        //            {
        //                mob.Value.EstaminaAtual += Sortear.Valor(1, 10);

        //                #region vez mob


        //                if (mob.Value.EstaminaAtual >= mob.Value.EstaminaMaxima)
        //                {
        //                    //Quando todos morrem
        //                    if (membrosVivos.Count == 0)
        //                    {
        //                        await ctx.RespondAsync($"O grupo {lider.Personagem.Batalha.NomeGrupo} foi dizimado! {DiscordEmoji.FromName(ctx.Client, ":skull_crossbones:")}");
        //                        lider.Personagem.Batalha.MobsVivos = new Dictionary<string, RPGMob>();
        //                        lider.Personagem.Batalha.Turno = 0;
        //                        RPGUsuario.Salvar(lider);
        //                        return;
        //                    }
        //                    mob.Value.EstaminaAtual = 0;

        //                    RPGUsuario alvo = membrosVivos[Sortear.Valor(0, membrosVivos.Count)];

        //                    //Ataque dos mobs
        //                    double danoInimigo = CalcDano(alvo.Personagem.DefesaFisica, mob.Value.AtaqueFisico);
        //                    alvo.Personagem.DanoRecebido += danoInimigo;
        //                    alvo.Personagem.VidaAtual -= danoInimigo;
        //                    if (alvo.Personagem.VidaAtual <= 0)
        //                    {
        //                        alvo.Personagem.EstaminaAtual = 0;
        //                        RPGUsuario.Salvar(alvo);
        //                        membrosVivos.Remove(alvo);
        //                        membrosMortos.Add(alvo);
        //                        try
        //                        {
        //                            DiscordGuild MundoZayn = await ModuloCliente.Client.GetGuildAsync(420044060720627712);
        //                            DiscordChannel CanalRPG = MundoZayn.GetChannel(629281618447564810);
        //                            await CanalRPG.SendMessageAsync($"*Um {mob.Value.Nome.RemoverUltimaLetra()} matou o {ctx.User.Username}#{ctx.User.Discriminator}!*");
        //                        }
        //                        catch { }
        //                    }
        //                    //switch (sorteioAtaque)
        //                    //            {
        //                    //                case 1:
        //                    //                    personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Botas, out armadura);
        //                    //                    break;
        //                    //                case 2:
        //                    //                    personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Couraca, out armadura);
        //                    //                    break;
        //                    //                case 3:
        //                    //                    personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Helmo, out armadura);
        //                    //                    break;
        //                    //                case 4:
        //                    //                    personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Luvas, out armadura);
        //                    //                    break;
        //                    //            }

        //                    //            Se tiver
        //                    //                if (armadura != null)
        //                    //            {
        //                    //                danoInimigo = CalcDano(armadura.DefesaFisica, inimigo.AtaqueFisico);
        //                    //                armadura.DurabilidadeMax--;
        //                    //                if (armadura.DurabilidadeMax == 0)
        //                    //                {
        //                    //                    personagem.Inventario.DesequiparItem(armadura, personagem);
        //                    //                    await ctx.RespondAsync($"**({armadura.Nome})** quebrou! {ctx.User.Mention}!");
        //                    //                }
        //                }
        //                #endregion
        //            }

        //            for (int ind = 0; ind < membrosVivos.Count; ind++)
        //            {
        //                membrosVivos[ind].Personagem.EstaminaAtual += Sortear.Valor(1, 10);


        //                #region vez jogador

        //                if (membrosVivos[ind].Personagem.EstaminaAtual >= membrosVivos[ind].Personagem.EstaminaMaxima)
        //                {
        //                    vezJogador = true;
        //                    strVezJog.AppendLine($"{membrosVivos[ind].DiscordUser.Mention} {DiscordEmoji.FromName(ctx.Client, ":white_check_mark:")}");
        //                }
        //                #endregion
        //            }

        //        } while (vezJogador == false);
        //        foreach (var item in membrosVivos)
        //            RPGUsuario.Salvar(item);

        //        #endregion
        //        //Salvamos e exibimos o relatório da batalha.

        //        DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Proximo turno", ctx);
        //        embed.WithColor(DiscordColor.IndianRed);
        //        embed.WithTitle($"**{lider.Personagem.Batalha.NomeGrupo}**".Titulo());
        //        embed.WithDescription($"**Turno**: {lider.Personagem.Batalha.Turno}");


        //        StringBuilder strVida = new StringBuilder();
        //        foreach (var members in membrosVivos)
        //            strVida.AppendLine($"**{members.DiscordUser.Mention} {DiscordEmoji.FromName(ctx.Client, ":hearts:")} {members.Personagem.VidaAtual.Text()}/{members.Personagem.VidaMaxima.Text()}**");
        //        foreach (var members in membrosMortos)
        //            strVida.AppendLine($"{members.DiscordUser.Mention} {DiscordEmoji.FromName(ctx.Client, ":skull_crossbones:")}");
        //        embed.AddField("Membros".Titulo(), strVida.ToString(), true);


        //        //Colocar bolinhas e trocar por porcentagem
        //        StringBuilder strMob = new StringBuilder();
        //        foreach (var mob in lider.Personagem.Batalha.MobsVivos)
        //            if (mob.Value.PontosDeVida > 0)
        //                strMob.AppendLine($"{mob.Key.PrimeiraLetraMaiuscula()} {DiscordEmoji.FromName(ctx.Client, ":hearts:")} {Extensoes.Text(mob.Value.PontosDeVida)}");
        //            else
        //                strMob.AppendLine($"{mob.Value.Nome} {DiscordEmoji.FromName(ctx.Client, ":skull_crossbones:")}");
        //        embed.AddField("Mobs".Titulo(), strMob.ToString(), true);


        //        StringBuilder strAtaquesRecebidos = new StringBuilder();
        //        foreach (var mVivo in membrosVivos)
        //            if (mVivo.Personagem.DanoRecebido > 0)
        //                strAtaquesRecebidos.AppendLine($"**{mVivo.DiscordUser.Mention} perdeu -{Extensoes.Text(mVivo.Personagem.DanoRecebido)} de vida.**");
        //        if (!string.IsNullOrEmpty(strAtaquesRecebidos.ToString()))
        //            embed.AddField("Danos".Titulo(), strAtaquesRecebidos.ToString());
        //        embed.AddField("Pronto".Titulo(), strVezJog.ToString(), false);


        //        await ctx.RespondAsync(embed: embed);
        //    }
        //}

        //public static double CalcDano(double resistencia, double dano)
        //{
        //    double porcentagemFinal = 100 / (100 + resistencia);
        //    //Dano minimo sempre será o valor total dividido por 2. 
        //    return (Sortear.Valor((dano / 2), dano)) * porcentagemFinal;
        }
    }
}

