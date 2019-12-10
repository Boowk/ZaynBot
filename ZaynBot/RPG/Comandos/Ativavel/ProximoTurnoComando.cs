using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Ativavel
{
    public class ProximoTurnoComando : BaseCommandModule
    {
        [Command("proximo-turno")]
        [Aliases("pt")]
        [Description("Permite iniciar o proximo turno")]
        [UsoAtributo("proximo-turno")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task ProximoTurnoComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.GetPersonagem(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;
            BatalhaRPG batalha = null;


            //Caso não tenha grupo
            if (personagem.Batalha.LiderGrupo == 0)
            {
                await ctx.RespondAsync($"Você deve criar um Grupo antes! {ctx.User.Mention}.");
                return;
            }


            List<UsuarioRPG> jogadores = new List<UsuarioRPG>();
            #region Defini a Party
            //Caso o lider do grupo não seja ele
            if (personagem.Batalha.LiderGrupo != ctx.User.Id)
            {
                UsuarioRPG liderUsuario = await UsuarioRPG.UsuarioGetAsync(personagem.Batalha.LiderGrupo);
                jogadores.Add(liderUsuario);
                batalha = liderUsuario.Personagem.Batalha;
            }

            //Caso ele seja o lider
            if (personagem.Batalha.LiderGrupo == ctx.User.Id)
            {

                jogadores.Add(usuario);
                batalha = personagem.Batalha;
            }
            #endregion


            foreach (var item in batalha.Jogadores)
            {
                jogadores.Add(await UsuarioRPG.UsuarioGetAsync(item));
            }

            bool vezJogador = false;
            UsuarioRPG user = null;
            StringBuilder mensagemAtaque = new StringBuilder();
            do
            {
                foreach (var item in batalha.Mobs)
                {
                    var m = item.Value;
                    //if (m.Atacou)
                    //    continue;
                    m.EstaminaAtual += Sortear.Valor(1, 10);
                    if (m.EstaminaAtual >= m.EstaminaMaxima)
                    {
                        Random r = new Random();
                        int v = r.Next(1, jogadores.Count + 1);
                        double danoInimigo = CalcDano(jogadores[v].Personagem.DefesaFisica, m.AtaqueFisico);

                        jogadores[v].Personagem.VidaAtual -= danoInimigo;
                        if (jogadores[v].Personagem.VidaAtual <= jogadores[v].Personagem.VidaMaxima)
                        {
                            var u = await ModuloCliente.Client.GetUserAsync(jogadores[v].Id);
                            await ctx.RespondAsync($"{u.Mention} morreu!");
                        }
                        UsuarioRPG.Salvar(jogadores[v]);
                    }
                }


                foreach (var item in jogadores)
                {
                    PersonagemRPG p = item.Personagem;
                    if (p.Batalha.Atacou)
                        continue;
                    p.EstaminaAtual += Sortear.Valor(1, 10);
                    if (p.EstaminaAtual >= p.EstaminaMaxima)
                    {
                        vezJogador = true;
                        user = item;
                    }

                }

            } while (vezJogador);

            //double danoRecebido = 0;
            //while (personagem.Batalha.PontosDeAcao < personagem.Batalha.PontosDeAcaoTotal)
            //{
            //    personagem.Batalha.PontosDeAcao += personagem.Raca.Agilidade / 4 + Sortear.Valor(1, 10);
            //    foreach (var inimigo in personagem.Batalha.Inimigos)
            //    {
            //        inimigo.PontosDeAcao += (inimigo.Velocidade / 4) + Sortear.Valor(1, 10);

            //        if (inimigo.PontosDeAcao >= personagem.Batalha.PontosDeAcaoTotal)
            //        {
            //            personagem.Batalha.Turno++;
            //            inimigo.PontosDeAcao = 0;

            //            double danoInimigo = 0;
            //            Random r = new Random();
            //            int sorteioAtaque = r.Next(0, 5);
            //            ItemRPG armadura = null;
            //            switch (sorteioAtaque)
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
            //            }
            //            else
            //                danoInimigo = CalcDano(0, inimigo.AtaqueFisico);
            //            personagem.VidaAtual -= danoInimigo;
            //            if (personagem.VidaAtual <= 0)
            //                try
            //                {
            //                    DiscordGuild MundoZayn = await ModuloCliente.Client.GetGuildAsync(420044060720627712);
            //                    DiscordChannel CanalRPG = MundoZayn.GetChannel(629281618447564810);
            //                    await CanalRPG.SendMessageAsync($"*Um {inimigo.Nome} matou o {ctx.User.Username}!*");
            //                }
            //                catch { }
            //            danoRecebido += danoInimigo;
            //        }
            //    }
            //}
            //personagem.Batalha.Turno++;
            //personagem.Batalha.PontosDeAcao = 0;
            //return danoRecebido;



            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Proximo turno", ctx);
            embed.WithColor(DiscordColor.PhthaloGreen);
            embed.WithTitle($"**{batalha.NomeGrupo}**".Titulo());
            if (batalha.LiderGrupo == ctx.User.Id)
                embed.WithDescription($"**Lider:** {ctx.User.Mention} - {CalcularVez(personagem.EstaminaAtual, personagem.EstaminaMaxima)}\n" +
                    $"**Turno**: {batalha.Turno.ToString()}\n");
            if (batalha.LiderGrupo != ctx.User.Id)
            {
                DiscordUser liderUser = await ctx.Client.GetUserAsync(batalha.LiderGrupo);
                var liderJogador = await UsuarioRPG.UsuarioGetAsync(batalha.LiderGrupo);
                embed.WithDescription($"**Lider:** {liderUser.Mention} - {CalcularVez(liderJogador.Personagem.EstaminaAtual, liderJogador.Personagem.EstaminaMaxima)}\n" +
                    $"**Turno**: {batalha.Turno.ToString()}\n");
            }
        }

        public string CalcularVez(double estaminaAtual, double estaminaMaxima)
        {
            if (estaminaAtual >= estaminaMaxima)
                return "Pronto";
            if (estaminaAtual != 0)
                return "Se preparando";
            return "Já atacou";
        }

        public static double CalcDano(double resistencia, double dano)
        {
            double porcentagemFinal = 100 / (100 + resistencia);
            //Dano minimo sempre será o valor total dividido por 2.
            return (Sortear.Valor((dano / 2), dano)) * porcentagemFinal;
        }
    }
}
