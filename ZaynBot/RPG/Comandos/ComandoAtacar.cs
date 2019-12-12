using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;
using ZaynBot.RPG.Habilidades;
using ZaynBot.Utilidades;


namespace ZaynBot.RPG.Comandos
{

    public class ComandoAtacar : BaseCommandModule
    {
        [Command("atacar")]
        [Aliases("at")]
        [Description("Ataca o primeiro inimigo que está na sua frente.")]
        [UsoAtributo("atacar [nome id|]")]
        [ExemploAtributo("atacar")]
        [ExemploAtributo("atacar coelho 1")]
        [Cooldown(1, 6, CooldownBucketType.User)]
        public async Task AtacarComandoAb(CommandContext ctx, [RemainingText] string inimigoNome = "")
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);

            if (usuario.Personagem.VidaAtual <= 0)
            {
                await ctx.RespondAsync($"Você está morto {ctx.User.Mention}!");
                return;
            }


            if (usuario.Personagem.Batalha.LiderGrupo == 0)
            {
                await ctx.RespondAsync($"Você deve criar um Grupo antes! {ctx.User.Mention}.");
                return;
            }


            RPGBatalha batalha = new RPGBatalha();
            RPGUsuario liderBatalha = null;

            #region Defini a Party
            //Caso o lider do grupo não seja ele
            if (usuario.Personagem.Batalha.LiderGrupo != ctx.User.Id)
            {
                liderBatalha = ModuloBanco.GetUsuario(usuario.Personagem.Batalha.LiderGrupo);
                batalha = liderBatalha.Personagem.Batalha;
            }
            else
                batalha = usuario.Personagem.Batalha;

            #endregion


            if (batalha.MobsVivos.Count == 0 && batalha.MobsMortos.Count != 0)
            {
                await ctx.RespondAsync($"Todos os inimigos já estão mortos! O lider do grupo deve usar `saquear` para finalizar a batalha, {ctx.User.Mention}!");
                return;
            }
            else if (batalha.MobsVivos.Count == 0)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, o seu grupo não tem nenhum inimigo para atacar!");
                return;
            }

            if (usuario.Personagem.EstaminaAtual < usuario.Personagem.EstaminaMaxima)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você não tem estamina o suficiente para atacar!");
                return;
            }


            RPGMob mob = null;
            //Procura o inimigo
            if (string.IsNullOrWhiteSpace(inimigoNome))
            {
                var v = batalha.MobsVivos.First();
                mob = v.Value;
                mob.Nome = v.Key;

            }
            else
            {
                bool achou = batalha.MobsVivos.TryGetValue(inimigoNome, out mob);
                if (!achou)
                {
                    await ctx.RespondAsync($"Alvo **{inimigoNome}** não encontrado! {ctx.User.Mention}.");
                    return;
                }
                mob.Nome = inimigoNome.ToLower().PrimeiraLetraMaiuscula();
            }

            usuario.Personagem.EstaminaAtual = 0;
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Combate", ctx);

            //Verifica - se se está com arma equipado
            usuario.Personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Arma, out RPGItem arma);
            double danoJogador = 0;
            if (arma != null)
            {
                switch (arma.TipoExp)
                {
                    case TipoExpEnum.Perfurante:
                        HabilidadePerfurante perfuranteHab = (HabilidadePerfurante)usuario.Personagem.Habilidades[HabilidadeEnum.Perfurante];
                        bool evoluiu = perfuranteHab.AdicionarExp();
                        danoJogador = CalcDano(mob.Armadura, perfuranteHab.CalcularDano(arma.AtaqueFisico, arma.DurabilidadeMax, ModuloBanco.ItemGet(arma.Id).DurabilidadeMax));
                        if (evoluiu)
                            await ctx.RespondAsync($"Habilidade **({perfuranteHab.Nome})** evoluiu! {ctx.User.Mention}.");
                        break;
                    case TipoExpEnum.Esmagante:
                        HabilidadeEsmagante esmaganteHab = (HabilidadeEsmagante)usuario.Personagem.Habilidades[HabilidadeEnum.Esmagante];
                        bool evoluiu2 = esmaganteHab.AdicionarExp();
                        if (evoluiu2)
                            await ctx.RespondAsync($"Habilidade **({esmaganteHab.Nome})** evoluiu! {ctx.User.Mention}.");
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
                HabilidadeDesarmado desarmadoHab = (HabilidadeDesarmado)usuario.Personagem.Habilidades[HabilidadeEnum.Desarmado];
                bool evoluiu3 = desarmadoHab.AdicionarExp();
                if (evoluiu3)
                    await ctx.RespondAsync($"Habilidade **({desarmadoHab.Nome})** evoluiu! {ctx.User.Mention}.");
                danoJogador = CalcDano(mob.Armadura, desarmadoHab.CalcularDano(usuario.Personagem.AtaqueFisico));
            }
            string mensagemMortos = "";
            double danoNoinimigo = 0;
            if (mob.PontosDeVida > danoJogador)
                danoNoinimigo = danoJogador;
            else
                danoNoinimigo = mob.PontosDeVida;
            mob.PontosDeVida -= danoNoinimigo;
            embed.WithTitle(batalha.NomeGrupo.Titulo());
            embed.WithDescription($"**{mob.Nome.PrimeiraLetraMaiuscula()} perdeu -{danoNoinimigo.Texto2Casas()} de vida.**");
            StringBuilder mensagemAtaquesInimigos = new StringBuilder();
            embed.WithColor(DiscordColor.Red);


            if (mob.PontosDeVida <= 0)
            {
                //Removemos o inimigo
                mensagemMortos = $"**{mob.Nome.PrimeiraLetraMaiuscula()} morreu.**";
                embed.AddField($"**{"Inimigos abatidos".Titulo()}**", mensagemMortos.ToString());
                batalha.MobsVivos.Remove(mob.Nome);
                batalha.MobsMortos.Add(mob.Nome, mob);

                //List<RPGUsuario> l = new List<RPGUsuario>();
                //l.Add(await RPGUsuario.UsuarioGetAsync(batalha.LiderGrupo));
                //foreach (var item in batalha.Jogadores)
                //    l.Add(await RPGUsuario.UsuarioGetAsync(item));


                //foreach (var item in l)
                //{
                //    bool isEvoluiou = personagem.AdicionarExp(mob.Essencia / l.Count);
                //    if (isEvoluiou)
                //    {
                //        DiscordUser g = await ctx.Client.GetUserAsync(item.Id);
                //        await ctx.RespondAsync($"{g.Mention}, evoluiu para o nível {item.Personagem.NivelAtual}! Seus atributos aumentarão em 2%!");
                //    }
                //}


                //foreach (var i in mob.ChanceItemUnico)
                //{
                //    if (Sortear.Sucesso(i.ChanceDeCair))
                //    {
                //        //Pega a data do item no Banco de dados
                //        ItemRPG itemData = ModuloBanco.ItemGet(i.ItemId);
                //        // Sorteia - se a quantidade
                //        int quantidade = Sortear.Valor(1, i.QuantidadeMaxima);
                //        // Adiciona no inventario o item conforme a quantidade sorteada
                //        personagem.Inventario.AdicionarItem(itemData, quantidade);
                //        // Enviamos uma mensagem avisando a quantidade sorteada e o item que foi sorteado
                //        mensagemDrops.Append($"{quantidade} {itemData.Nome.PrimeiraLetraMaiuscula()}.\n");
                //        // Finalizamos o sorteio de itens.
                //        break;
                //    }
                //}
                ////Lista onde pode sortear todos os itens
                //// Para cada item da lista
                //foreach (var i in mobAlvo.ChanceItemTodos)
                //{
                //    // Sorteia - se a chance de cair do item
                //    // Se foi sorteado
                //    if (Sortear.Sucesso(i.ChanceDeCair))
                //    {
                //        //   Pega a data do item no Banco de dados
                //        ItemRPG itemData = ModuloBanco.ItemGet(i.ItemId);
                //        //   Sorteia - se a quantidade
                //        int quantidade = Sortear.Valor(1, i.QuantidadeMaxima);
                //        //   Adiciona no inventario o item conforme a quantidade sorteada
                //        personagem.Inventario.AdicionarItem(itemData, quantidade);
                //        //  Enviamos uma mensagem avisando a quantidade sorteada e o item que foi sorteado
                //        mensagemDrops.Append($"{quantidade} {itemData.Nome.PrimeiraLetraMaiuscula()}.\n");
                //    }
                //}
                //// Enviamos a mensagem dos itens que cai - o se não for nulo
                //if (mensagemDrops.ToString() != "")
                //    embed.AddField($"**{"Recompensas".Titulo()}**", $"**{mensagemDrops.ToString()}**");
            }

            // Salvamos o usuario
            RPGUsuario.Salvar(usuario);
            if (liderBatalha != null) RPGUsuario.Salvar(liderBatalha);

            // Retornamos toda a mensagem preparada para ser enviada
            await ctx.RespondAsync(embed: embed.Build());
        }


        public static double CalcDano(double resistencia, double dano)
        {
            double porcentagemFinal = 100 / (100 + resistencia);
            //Dano minimo sempre será o valor total dividido por 2.
            return (Sortear.Valor((dano / 2), dano)) * porcentagemFinal;
        }


    }
}