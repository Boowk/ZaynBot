using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.Data.Itens;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;
using ZaynBot.RPG.Habilidades;
using ZaynBot.Utilidades;


namespace ZaynBot.RPG.Comandos
{
    public class ComandoAtacar : BaseCommandModule
    {
        public double CalcDano(double resistencia, double dano)
        {
            double porcentagemFinal = 100 / (100 + resistencia);
            //Dano minimo sempre será o valor total dividido por 2.
            return (Sortear.Valor((dano / 2), dano)) * porcentagemFinal;
        }

        [Command("atacar")]
        [Aliases("at")]
        [Description("Ataca o mob que você encontrou explorando")]
        [ComoUsar("atacar")]
        [Cooldown(1, 6, CooldownBucketType.User)]
        public async Task ComandoAtacarAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);

            if (usuario.Personagem.Batalha.Mob.VidaAtual <= 0)
            {
                await ctx.RespondAsync($"**{usuario.Personagem.Batalha.Mob.Nome}** já está morto! {ctx.User.Mention} use `z!explorar` para encontrar novos mobs!");
                return;
            }

            RPGMob mob = usuario.Personagem.Batalha.Mob;
            StringBuilder strRelatorio = new StringBuilder();
            bool vezJogador = false;
            do
            {
                //Vez mob
                mob.EstaminaAtual += Sortear.Valor(1, 10);

                //Ataque
                if (mob.EstaminaAtual >= mob.EstaminaMaxima)
                {

                    mob.EstaminaAtual = 0;
                    double danoInimigo = CalcDano(usuario.Personagem.DefesaFisica, mob.AtaqueFisico);
                    usuario.Personagem.VidaAtual -= danoInimigo;
                    if (usuario.Personagem.VidaAtual <= 0)
                    {
                        await ctx.RespondAsync("https://cdn.discordapp.com/attachments/651848690033754113/657218098033721365/RIP.png\n" +
                            $"{ctx.User.Mention}"); 
                        usuario.Personagem.VidaAtual = usuario.Personagem.VidaMaxima / 3;
                        RPGUsuario.Salvar(usuario);
                        try
                        {
                            DiscordGuild MundoZayn = await ModuloCliente.Client.GetGuildAsync(420044060720627712);
                            DiscordChannel CanalRPG = MundoZayn.GetChannel(629281618447564810);
                            await CanalRPG.SendMessageAsync($"*Um {mob.Nome} matou o {ctx.User.Username}#{ctx.User.Discriminator}!*");
                        }
                        catch { }
                        return;
                    }
                    usuario.Personagem.Batalha.Turno++;
                    strRelatorio.AppendLine($"**Turno {usuario.Personagem.Batalha.Turno}.**");
                    strRelatorio.AppendLine($"**{ctx.User.Mention} perdeu -{danoInimigo.Text()} vida.**");

                    //switch (sorteioAtaque)
                    //{
                    //    case 1:
                    //        personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Botas, out armadura);
                    //        break;
                    //    case 2:
                    //        personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Couraca, out armadura);
                    //        break;
                    //    case 3:
                    //        personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Helmo, out armadura);
                    //        break;
                    //    case 4:
                    //        personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Luvas, out armadura);
                    //        break;
                    //}
                }
                ////Se tiver
                //                if (armadura != null)
                //{
                //    danoInimigo = CalcDano(armadura.DefesaFisica, inimigo.AtaqueFisico);
                //    armadura.DurabilidadeMax--;
                //    if (armadura.DurabilidadeMax == 0)
                //    {
                //        personagem.Inventario.DesequiparItem(armadura, personagem);
                //        await ctx.RespondAsync($"**({armadura.Nome})** quebrou! {ctx.User.Mention}!");
                //    }
                //}

                //Vez jogador
                usuario.Personagem.EstaminaAtual += Sortear.Valor(1, 10);

                //Ataque
                if (usuario.Personagem.EstaminaAtual >= usuario.Personagem.EstaminaMaxima)
                {
                    vezJogador = true;
                    usuario.Personagem.Batalha.Turno++;
                    strRelatorio.AppendLine($"**Turno {usuario.Personagem.Batalha.Turno}.**");
                    usuario.Personagem.EstaminaAtual = 0;
                }
            } while (vezJogador == false);


            //Pega a arma que ele esta usando e já fz o calculo de proficiencia
            usuario.Personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Arma, out RPGItem arma);
            double danoJogador = 0;
            if (arma != null)
            {
                switch (arma.TipoExp)
                {
                    case TipoExpEnum.Perfurante:
                        ProficienciaPerfurante perfuranteHab = (ProficienciaPerfurante)usuario.Personagem.Proficiencias[ProficienciaEnum.Perfurante];
                        bool evoluiu = perfuranteHab.AdicionarExp();
                        danoJogador = CalcDano(mob.Armadura, perfuranteHab.CalcularDano(arma.AtaqueFisico, arma.DurabilidadeMax, ModuloBanco.ItemGet(arma.Id).DurabilidadeMax));
                        break;
                    case TipoExpEnum.Esmagante:
                        ProficienciaEsmagante esmaganteHab = (ProficienciaEsmagante)usuario.Personagem.Proficiencias[ProficienciaEnum.Esmagante];
                        bool evoluiu2 = esmaganteHab.AdicionarExp();
                        //if (evoluiu2)
                        //    await ctx.RespondAsync($"Habilidade **({esmaganteHab.Nome})** evoluiu! {ctx.User.Mention}.");
                        danoJogador = CalcDano(mob.Armadura, esmaganteHab.CalcularDano(arma.AtaqueFisico, arma.DurabilidadeMax, ModuloBanco.ItemGet(arma.Id).DurabilidadeMax));
                        break;
                }
                arma.DurabilidadeMax -= Convert.ToInt32(0.06 * arma.AtaqueFisico);
                if (arma.DurabilidadeMax <= 0)
                {
                    usuario.Personagem.Inventario.DesequiparItem(arma, usuario.Personagem);
                    await ctx.RespondAsync($"**({arma.Nome})** quebrou! {ctx.User.Mention}!");
                }
            }
            else
            {
                ProficienciaDesarmado desarmadoHab = (ProficienciaDesarmado)usuario.Personagem.Proficiencias[ProficienciaEnum.Desarmado];
                bool evoluiu3 = desarmadoHab.AdicionarExp();
                danoJogador = CalcDano(mob.Armadura, desarmadoHab.CalcularDano(usuario.Personagem.AtaqueFisico));
            }

            if (mob.VidaAtual < danoJogador)
                danoJogador = mob.VidaAtual;
            mob.VidaAtual -= danoJogador;


            #region Relatorio

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Ataque", ctx);
            embed.WithColor(DiscordColor.IndianRed);
            strRelatorio.AppendLine($"**`{mob.Nome.PrimeiraLetraMaiuscula()}` perdeu -{danoJogador.Text()} vida.**");


            embed.AddField(ctx.User.Username.Titulo(), $"{DiscordEmoji.FromName(ctx.Client, ":heart:")} {usuario.Personagem.VidaAtual.Text()}/{usuario.Personagem.VidaMaxima.Text()}", true);

            if (mob.VidaAtual <= 0)
            {
                strRelatorio.AppendLine($"**{DiscordEmoji.FromName(ctx.Client, ":skull_crossbones:")} {mob.Nome} {DiscordEmoji.FromName(ctx.Client, ":skull_crossbones:")}**");
                //if (mensagemDrops.ToString() != "")
                //    embed.AddField($"**{"Recompensas".Titulo()}**", $"**{mensagemDrops.ToString()}**");
                ////Pega a data do item no Banco de dados
                //ItensRPG itemData = ModuloBanco.ItemGet(i.ItemId);
                //// Sorteia - se a quantidade
                //int quantidade = Sortear.Valor(1, i.QuantidadeMaxima);
                //// Adiciona no inventario o item conforme a quantidade sorteada
                //usuario.Personagem.Inventario.AdicionarItem(itemData, quantidade);
                //// Enviamos uma mensagem avisando a quantidade sorteada e o item que foi sorteado
                //mensagemDrops.Append($"{quantidade} {itemData.Nome.PrimeiraLetraMaiuscula()}.\n");
                //// Finalizamos o sorteio de itens.
                //break;

                bool isEvoluiou = usuario.Personagem.AdicionarExp(mob.Essencia);
                if (isEvoluiou)
                {
                    await ctx.RespondAsync($"{ctx.User.Mention}, evoluiu para o nível {usuario.Personagem.NivelAtual}! Seus atributos aumentarão em 2%!");
                }
            }
            else
            {
                double porcentagem = mob.VidaAtual / mob.VidaMax;
                string vidaMob = "";
                if (porcentagem > 0.7)
                    vidaMob = $"{mob.Nome} {DiscordEmoji.FromName(ctx.Client, ":green_heart:")}";
                else if (porcentagem > 0.4)
                    vidaMob = $"{mob.Nome} {DiscordEmoji.FromName(ctx.Client, ":yellow_heart:")}";
                else if (porcentagem > 0)
                    vidaMob = $"{mob.Nome} {DiscordEmoji.FromName(ctx.Client, ":heart:")}";
                embed.AddField("mob".Titulo(), vidaMob, true);
            }
            embed.WithDescription(strRelatorio.ToString());
            #endregion

            RPGUsuario.Salvar(usuario);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
