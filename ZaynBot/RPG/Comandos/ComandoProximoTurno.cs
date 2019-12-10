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
            RPGUsuario.GetPersonagem(ctx, out RPGUsuario usuario);
            RPGPersonagem personagem = usuario.Personagem;
            RPGBatalha batalha = personagem.Batalha;


            //Caso não tenha grupo
            if (batalha.LiderGrupo == 0)
            {
                await ctx.RespondAsync($"Você deve criar um Grupo antes! {ctx.User.Mention}.");
                return;
            }

            if (batalha.LiderGrupo != ctx.User.Id)
            {
                await ctx.RespondAsync($"Somente o lider do grupo pode usar este comando, {ctx.User.Mention}!");
                return;
            }

            List<RPGUsuario> jogadores = new List<RPGUsuario>();
            jogadores.Add(usuario);
            foreach (var item in batalha.Jogadores)
                jogadores.Add(ModuloBanco.UsuarioGet(item));
            foreach (var item in jogadores)
            {
                if (item.Personagem.EstaminaAtual >= item.Personagem.EstaminaMaxima)
                {
                    await ctx.RespondAsync($"{ctx.User.Mention}, todos devem efetuar uma ação para se iniciar o proximo turno!");
                    return;
                }
            }


            bool vezJogador = false;
            do
            {
                foreach (var item in batalha.Mobs)
                {
                    var m = item.Value;
                    if (m.PontosDeVida > 0)
                        m.EstaminaAtual += Sortear.Valor(1, 10);
                    #region turno mob
                    if (m.EstaminaAtual >= m.EstaminaMaxima)
                    {
                        Random r = new Random();
                        int v = r.Next(0, jogadores.Count);
                        double danoInimigo = CalcDano(jogadores[v].Personagem.DefesaFisica, m.AtaqueFisico);
                        jogadores[v].Personagem.DanoRecebido += danoInimigo;
                        jogadores[v].Personagem.VidaAtual -= danoInimigo;
                        m.EstaminaAtual = 0;
                    }
                    #endregion
                }

                int f = 0;
                foreach (var item in jogadores)
                {
                    RPGPersonagem p = item.Personagem;
                    p.EstaminaAtual += Sortear.Valor(1, 10);

                    #region Turno Jogador
                    if (p.EstaminaAtual >= p.EstaminaMaxima)
                    {
                        vezJogador = true;
                    }
                    #endregion
                    f++;
                }

            } while (vezJogador == false);

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


            List<DiscordUser> users = new List<DiscordUser>();
            StringBuilder str = new StringBuilder();
            foreach (var item in jogadores)
            {
                var jog = await ctx.Client.GetUserAsync(item.Id);
                users.Add(jog);
                str.AppendLine($"{jog.Mention} - {CalcularVez(item.Personagem.EstaminaAtual, item.Personagem.EstaminaMaxima)}");
            }
            embed.WithDescription($"**Turno**: {batalha.Turno.ToString()}");
            embed.AddField("Membros".Titulo(), str.ToString(), true);


            StringBuilder stf = new StringBuilder();
            foreach (var item in batalha.Mobs)
            {
                stf.AppendLine($"{item.Key.PrimeiraLetraMaiuscula()} - Vida: {item.Value.PontosDeVida.Texto2Casas()}");
            }
            if (!string.IsNullOrEmpty(stf.ToString()))
                embed.AddField("Mobs".Titulo(), stf.ToString(), true);


            StringBuilder mensagemAtaque = new StringBuilder();
            int i = 0;
            foreach (var item in jogadores)
            {
                if (item.Personagem.DanoRecebido > 0)
                    mensagemAtaque.AppendLine($"{users[i].Mention} perdeu -{item.Personagem.DanoRecebido.Texto2Casas()} de vida.");
                i++;
            }
            if (!string.IsNullOrEmpty(mensagemAtaque.ToString()))
                embed.AddField("Danos recebidos".Titulo(), mensagemAtaque.ToString(), false);
            foreach (var item in jogadores)
            {
                RPGUsuario.Salvar(item);
            }
            await ctx.RespondAsync(embed: embed);
        }

        public string CalcularVez(double estaminaAtual, double estaminaMaxima)
        {
            if (estaminaAtual >= estaminaMaxima)
                return "Pronto";
            return "Se preparando";
        }

        public static double CalcDano(double resistencia, double dano)
        {
            double porcentagemFinal = 100 / (100 + resistencia);
            //Dano minimo sempre será o valor total dividido por 2. 
            return (Sortear.Valor((dano / 2), dano)) * porcentagemFinal;
        }
    }
}
