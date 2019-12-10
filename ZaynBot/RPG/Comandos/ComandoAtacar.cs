using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
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
        [UsoAtributo("atacar [id|]")]
        [ExemploAtributo("atacar")]
        [ExemploAtributo("atacar 3")]
        [Cooldown(1, 6, CooldownBucketType.User)]
        public async Task AtacarComandoAb(CommandContext ctx, [RemainingText] string inimigoNome = "")
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetPersonagem(ctx, out RPGUsuario usuario);
            RPGPersonagem personagem = usuario.Personagem;
            RPGBatalha batalha = new RPGBatalha();
            RPGUsuario liderUsuario = null;

            //Caso não tenha grupo
            if (personagem.Batalha.LiderGrupo == 0)
            {
                await ctx.RespondAsync($"Você deve criar um Grupo antes! {ctx.User.Mention}.");
                return;
            }

            #region Defini a Party
            //Caso o lider do grupo não seja ele
            if (personagem.Batalha.LiderGrupo != ctx.User.Id)
            {
                liderUsuario = await RPGUsuario.UsuarioGetAsync(personagem.Batalha.LiderGrupo);
                batalha = liderUsuario.Personagem.Batalha;
            }

            //Caso ele seja o lider
            if (personagem.Batalha.LiderGrupo == ctx.User.Id)
                batalha = personagem.Batalha;
            #endregion


            if (batalha.Mobs.Count == 0)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, não tem nenhum inimigo para você atacar.");
                return;
            }

            if (personagem.EstaminaAtual < personagem.EstaminaMaxima)
            {
                await ctx.RespondAsync($"Ainda não está pronto para atacar! {ctx.User.Mention}.");
                return;
            }


            RPGMob mob = null;
            //Procura o inimigo
            if (string.IsNullOrWhiteSpace(inimigoNome))
            {
                var v = batalha.Mobs.First();
                mob = v.Value;
                mob.Nome = v.Key;
            }
            else
            {
                bool achou = batalha.Mobs.TryGetValue(inimigoNome, out mob);
                if (!achou)
                {
                    await ctx.RespondAsync($"Alvo {inimigoNome} não encontrado! {ctx.User.Mention}.");
                    return;
                }
            }

            personagem.EstaminaAtual = 0;
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Combate", ctx);

            //Verifica - se se está com arma equipado
            personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Arma, out RPGItem arma);
            double danoJogador = 0;
            if (arma != null)
            {
                switch (arma.TipoExp)
                {
                    case TipoExpEnum.Perfurante:
                        HabilidadePerfurante perfuranteHab = (HabilidadePerfurante)personagem.Habilidades[HabilidadeEnum.Perfurante];
                        bool evoluiu = perfuranteHab.AdicionarExp();
                        danoJogador = CalcDano(mob.Armadura, perfuranteHab.CalcularDano(arma.AtaqueFisico, arma.DurabilidadeMax, ModuloBanco.ItemGet(arma.Id).DurabilidadeMax));
                        if (evoluiu)
                            await ctx.RespondAsync($"Habilidade **({perfuranteHab.Nome})** evoluiu! {ctx.User.Mention}.");
                        break;
                    case TipoExpEnum.Esmagante:
                        HabilidadeEsmagante esmaganteHab = (HabilidadeEsmagante)personagem.Habilidades[HabilidadeEnum.Esmagante];
                        bool evoluiu2 = esmaganteHab.AdicionarExp();
                        if (evoluiu2)
                            await ctx.RespondAsync($"Habilidade **({esmaganteHab.Nome})** evoluiu! {ctx.User.Mention}.");
                        danoJogador = CalcDano(mob.Armadura, esmaganteHab.CalcularDano(arma.AtaqueFisico, arma.DurabilidadeMax, ModuloBanco.ItemGet(arma.Id).DurabilidadeMax));
                        break;
                }
                arma.DurabilidadeMax -= Convert.ToInt32(0.06 * arma.AtaqueFisico);
                if (arma.DurabilidadeMax <= 0)
                {
                    personagem.Inventario.DesequiparItem(arma, personagem);
                    await ctx.RespondAsync($"**({arma.Nome})** quebrou! {ctx.User.Mention}!");
                }
            }
            else
            {
                HabilidadeDesarmado desarmadoHab = (HabilidadeDesarmado)personagem.Habilidades[HabilidadeEnum.Desarmado];
                bool evoluiu3 = desarmadoHab.AdicionarExp();
                if (evoluiu3)
                    await ctx.RespondAsync($"Habilidade **({desarmadoHab.Nome})** evoluiu! {ctx.User.Mention}.");
                danoJogador = CalcDano(mob.Armadura, desarmadoHab.CalcularDano(personagem.AtaqueFisico));
            }
            string mensagemMortos = "";
            double danoNoinimigo = 0;
            if (mob.PontosDeVida > danoJogador)
                danoNoinimigo = danoJogador;
            else
                danoNoinimigo = mob.PontosDeVida;
            mob.PontosDeVida -= danoNoinimigo;
            embed.WithDescription($"{mob.Nome} perdeu -{danoNoinimigo.Texto2Casas()} de vida.");
            StringBuilder mensagemAtaquesInimigos = new StringBuilder();
            embed.WithColor(DiscordColor.Red);


            //mensagemAtaquesInimigos.Append($"Você perdeu -{danoRecebido.Texto2Casas()} de vida.\n");
            //Enviamos a mensagem armazenada se ela não for vazia
            //    if (danoRecebido != 0)
            //        embed.AddField($"**{"Danos recebidos".Titulo()}**", mensagemAtaquesInimigos.ToString());
            //Adicionamos mais mensagens
            //    string t = personagem.Batalha.Turno + "º Turno";
            //embed.WithTitle($"**{t.Titulo()}**");

            if (mob.PontosDeVida <= 0)
            {
                //Removemos o inimigo
                batalha.Mobs.Remove(mob.Nome);
                mensagemMortos = $"{mob.Nome} morreu.";
                embed.AddField($"**{"Inimigos abatidos".Titulo()}**", mensagemMortos.ToString());


                bool isEvoluiou = personagem.AdicionarExp(mob.Essencia);
                if (isEvoluiou)
                    await ctx.RespondAsync($"{ctx.User.Mention}, você evoluiu para o nível {personagem.NivelAtual}! Seus atributos aumentarão em 20%!");

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
            if (liderUsuario != null) RPGUsuario.Salvar(liderUsuario);

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