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
        [UsoAtributo("proximo-turno")]
        [Cooldown(1, 2, CooldownBucketType.User)]
        public async Task ProximoTurno(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetPersonagem(ctx, out RPGUsuario lider);


            if (lider.Personagem.Batalha.LiderGrupo == 0)
            {
                await ctx.RespondAsync($"Você deve criar um Grupo antes! {ctx.User.Mention}!");
                return;
            }

            if (lider.Personagem.Batalha.LiderGrupo != ctx.User.Id)
            {
                await ctx.RespondAsync($"Somente o lider do grupo pode usar este comando, {ctx.User.Mention}!");
                return;
            }

            if (lider.Personagem.Batalha.Mobs.Count == 0)
            {
                await ctx.RespondAsync($"Você não tem nenhuma batalha em andamento para iniciar outro turno! {ctx.User.Mention}.");
                return;
            }

            int quantMobsVivos = lider.Personagem.Batalha.Mobs.Count;
            foreach (var mobs in lider.Personagem.Batalha.Mobs)
            {
                if (mobs.Value.Morto)
                    quantMobsVivos--;
            }
            if (quantMobsVivos == 0)
            {
                //Temporario
                await ctx.RespondAsync("Todos os inimigos foram mortos.");
                lider.Personagem.Batalha.Mobs = new Dictionary<string, RPGMob>();
                RPGUsuario.Salvar(lider);
                // Faz a distribuição de recompensas
                return;
            }
            else
            {
                List<RPGUsuario> membros = new List<RPGUsuario>();
                List<DiscordUser> membrosUsers = new List<DiscordUser>();
                foreach (var m in lider.Personagem.Batalha.Membros)
                {
                    var u = ModuloBanco.UsuarioGet(m);
                    var du = await ctx.Client.GetUserAsync(m);
                    if (u.Personagem.EstaminaAtual >= u.Personagem.EstaminaMaxima)
                    {
                        if (u.Personagem.VidaAtual > 0)
                        {
                            await ctx.RespondAsync($"{du.Mention} ainda não fez um movimento, para se iniciar o proximo turno!");
                            return;
                        }
                    }
                    membros.Add(u);
                    membrosUsers.Add(du);
                }
                //Verifica o lider após
                var _u = lider;
                var _du = await ctx.Client.GetUserAsync(_u.Id);
                if (_u.Personagem.EstaminaAtual >= _u.Personagem.EstaminaMaxima)
                {
                    if (_u.Personagem.VidaAtual > 0)
                    {
                        await ctx.RespondAsync($"{_du.Mention} ainda não fez um movimento, para se iniciar o proximo turno!");
                        return;
                    }
                }
                membros.Add(_u);
                membrosUsers.Add(_du);


                //Quando todos morrem
                int quantMembroVivos = membros.Count;
                foreach (var membro in membros)
                {
                    if (membro.Personagem.VidaAtual <= 0)
                        quantMembroVivos--;
                }
                if (quantMembroVivos == 0)
                {
                    await ctx.RespondAsync($"O grupo {lider.Personagem.Batalha.NomeGrupo} foi dizimado!");
                    lider.Personagem.Batalha.Mobs = new Dictionary<string, RPGMob>();
                    lider.Personagem.Batalha.Turno = 0;
                    RPGUsuario.Salvar(lider);
                    return;
                }

                #region inicio turno

                lider.Personagem.Batalha.Turno++;
                bool vezJogador = false;
                StringBuilder strVezJog = new StringBuilder();
                do
                {
                    foreach (var mob in lider.Personagem.Batalha.Mobs)
                    {
                        if (mob.Value.Morto)
                            continue;
                        mob.Value.EstaminaAtual += Sortear.Valor(1, 10);


                        #region vez mob


                        if (mob.Value.EstaminaAtual >= mob.Value.EstaminaMaxima)
                        {
                            RPGUsuario alvo;
                            do
                            {
                                alvo = membros[Sortear.Valor(0, membros.Count)];
                            } while (alvo.Personagem.VidaAtual <= 0);
                            //Ataque dos mobs
                            double danoInimigo = CalcDano(alvo.Personagem.DefesaFisica, mob.Value.AtaqueFisico);
                            alvo.Personagem.DanoRecebido += danoInimigo;
                            alvo.Personagem.VidaAtual -= danoInimigo;

                            if (alvo.Personagem.VidaAtual <= 0)
                            {
                                alvo.Personagem.EstaminaAtual = 0;
                                try
                                {
                                    DiscordGuild MundoZayn = await ModuloCliente.Client.GetGuildAsync(420044060720627712);
                                    DiscordChannel CanalRPG = MundoZayn.GetChannel(629281618447564810);
                                    await CanalRPG.SendMessageAsync($"*Um {mob.Value.Nome} matou o {ctx.User.Username}#{ctx.User.Discriminator}!*");
                                }
                                catch { }
                            }
                            //switch (sorteioAtaque)
                            //            {
                            //                case 1:
                            //                    personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Botas, out armadura);
                            //                    break;
                            //                case 2:
                            //                    personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Couraca, out armadura);
                            //                    break;
                            //                case 3:
                            //                    personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Helmo, out armadura);
                            //                    break;
                            //                case 4:
                            //                    personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Luvas, out armadura);
                            //                    break;
                            //            }

                            //            Se tiver
                            //                if (armadura != null)
                            //            {
                            //                danoInimigo = CalcDano(armadura.DefesaFisica, inimigo.AtaqueFisico);
                            //                armadura.DurabilidadeMax--;
                            //                if (armadura.DurabilidadeMax == 0)
                            //                {
                            //                    personagem.Inventario.DesequiparItem(armadura, personagem);
                            //                    await ctx.RespondAsync($"**({armadura.Nome})** quebrou! {ctx.User.Mention}!");
                            //                }


                            //Reinicia a estamina
                            mob.Value.EstaminaAtual = 0;
                        }
                        #endregion
                    }

                    for (int ind = 0; ind < membros.Count; ind++)
                    {
                        membros[ind].Personagem.EstaminaAtual += Sortear.Valor(1, 10);


                        #region vez jogador

                        if (membros[ind].Personagem.VidaAtual > 0)
                            if (membros[ind].Personagem.EstaminaAtual >= membros[ind].Personagem.EstaminaMaxima)
                            {
                                vezJogador = true;
                                strVezJog.AppendLine($"{membrosUsers[ind].Mention} Pronto");
                            }


                        #endregion
                    }

                } while (vezJogador == false);
                foreach (var item in membros)
                    RPGUsuario.Salvar(item);

                #endregion
                //Salvamos e exibimos o relatório da batalha.

                DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Proximo turno", ctx);
                embed.WithColor(DiscordColor.IndianRed);
                embed.WithTitle($"**{lider.Personagem.Batalha.NomeGrupo}**".Titulo());
                embed.WithDescription($"**Turno**: {lider.Personagem.Batalha.Turno}");


                StringBuilder strVida = new StringBuilder();
                for (int ind = 0; ind < membros.Count; ind++)
                    if (membros[ind].Personagem.VidaAtual <= 0)
                        strVida.AppendLine($"{membrosUsers[ind].Mention} - Morto");
                    else
                        strVida.AppendLine($"{membrosUsers[ind].Mention} - {membros[ind].Personagem.VidaAtual}/{membros[ind].Personagem.VidaMaxima}");
                embed.AddField("Membros".Titulo(), strVida.ToString(), true);


                //Colocar bolinhas e trocar por porcentagem
                StringBuilder strMob = new StringBuilder();
                foreach (var mob in lider.Personagem.Batalha.Mobs)
                    strMob.AppendLine($"{mob.Key.PrimeiraLetraMaiuscula()} - Vida: {mob.Value.PontosDeVida.Texto2Casas()}");
                embed.AddField("Mobs".Titulo(), strMob.ToString(), true);


                StringBuilder strAtaquesRecebidos = new StringBuilder();
                for (int i = 0; i < membros.Count; i++)
                    if (membros[i].Personagem.DanoRecebido > 0)
                        strAtaquesRecebidos.AppendLine($"{membrosUsers[i].Mention} **recebeu {membros[i].Personagem.DanoRecebido.Texto2Casas()} de dano.**");
                if (!string.IsNullOrEmpty(strAtaquesRecebidos.ToString()))
                    embed.AddField("Relatório".Titulo(), strAtaquesRecebidos.ToString());
                embed.AddField("Pronto".Titulo(), strVezJog.ToString(), false);


                await ctx.RespondAsync(embed: embed);
            }
        }

        public static double CalcDano(double resistencia, double dano)
        {
            double porcentagemFinal = 100 / (100 + resistencia);
            //Dano minimo sempre será o valor total dividido por 2. 
            return (Sortear.Valor((dano / 2), dano)) * porcentagemFinal;
        }
    }
}

