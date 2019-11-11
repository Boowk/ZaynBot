//using DSharpPlus.CommandsNext;
//using DSharpPlus.CommandsNext.Attributes;
//using DSharpPlus.Entities;
//using System;
//using System.Text;
//using System.Threading.Tasks;
//using ZaynBot.Core.Atributos;
//using ZaynBot.RPG.Entidades;
//using ZaynBot.RPG.Entidades.Enuns;
//using ZaynBot.RPG.Habilidades;
//using ZaynBot.Utilidades;


//namespace ZaynBot.RPG.Comandos.Ativavel
//{

//    public class AtacarComando : BaseCommandModule
//    {
//        [Command("atacar")]
//        [Aliases("at")]
//        [Description("Ataca o primeiro inimigo que está na sua frente.")]
//        [UsoAtributo("atacar [id|]")]
//        [ExemploAtributo("atacar")]
//        [ExemploAtributo("atacar 3")]
//        [Cooldown(1, 6, CooldownBucketType.User)]
//        public async Task AtacarComandoAb(CommandContext ctx, int id = 0)
//        {
//            await ctx.TriggerTypingAsync();
//            UsuarioRPG.GetPersonagem(ctx, out UsuarioRPG usuario);
//            PersonagemRPG personagem = usuario.Personagem;
//            if (personagem.Batalha.Inimigos.Count == 0)
//            {
//                await ctx.RespondAsync($"{ctx.User.Mention}, não tem nenhum inimigo para você atacar.");
//                return;
//            }

//            MobRPG mobAlvo = null;
//            try
//            {
//                mobAlvo = personagem.Batalha.Inimigos[id];
//            }
//            catch
//            {
//                mobAlvo = personagem.Batalha.Inimigos[0];
//                id = 0;
//            }

//            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Combate", ctx);

//            double danoRecebido = await CalcBatalhaMobsAsync(personagem, ctx);

//            Verifica - se se está com arma equipado
//            personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Arma, out ItemRPG arma);
//            double danoJogador = 0;
//            if (arma != null)
//            {
//                switch (arma.TipoExp)
//                {
//                    case TipoExpEnum.Perfurante:
//                        PerfuranteHabilidade perfuranteHab = (PerfuranteHabilidade)personagem.Habilidades[HabilidadeEnum.Perfurante];
//                        bool evoluiu = perfuranteHab.AdicionarExp();
//                        danoJogador = CalcDano(mobAlvo.Armadura, perfuranteHab.CalcularDano(arma.AtaqueFisico, arma.Durabilidade, ModuloBanco.ItemGet(arma.Id).Durabilidade, personagem.Raca.Forca));
//                        if (evoluiu)
//                            await ctx.RespondAsync($"Habilidade **({perfuranteHab.Nome})** evoluiu! {ctx.User.Mention}.");
//                        break;
//                    case TipoExpEnum.Esmagante:
//                        EsmaganteHabilidade esmaganteHab = (EsmaganteHabilidade)personagem.Habilidades[HabilidadeEnum.Esmagante];
//                        bool evoluiu2 = esmaganteHab.AdicionarExp();
//                        if (evoluiu2)
//                            await ctx.RespondAsync($"Habilidade **({esmaganteHab.Nome})** evoluiu! {ctx.User.Mention}.");
//                        danoJogador = CalcDano(mobAlvo.Armadura, esmaganteHab.CalcularDano(arma.AtaqueFisico, arma.Durabilidade, ModuloBanco.ItemGet(arma.Id).Durabilidade, personagem.Raca.Forca));
//                        break;
//                }
//                arma.Durabilidade -= Convert.ToInt32(0.06 * arma.AtaqueFisico);
//                if (arma.Durabilidade <= 0)
//                {
//                    personagem.Inventario.DesequiparItem(arma, personagem);
//                    await ctx.RespondAsync($"**({arma.Nome})** quebrou! {ctx.User.Mention}!");
//                }
//            }
//            else
//            {
//                DesarmadoHabilidade desarmadoHab = (DesarmadoHabilidade)personagem.Habilidades[HabilidadeEnum.Desarmado];
//                bool evoluiu3 = desarmadoHab.AdicionarExp();
//                if (evoluiu3)
//                    await ctx.RespondAsync($"Habilidade **({desarmadoHab.Nome})** evoluiu! {ctx.User.Mention}.");
//                danoJogador = CalcDano(mobAlvo.Armadura, desarmadoHab.CalcularDano(personagem.Raca.Forca));
//            }
//            string mensagemMortos = "";
//            double danoNoinimigo = 0;
//            if (mobAlvo.PontosDeVida > danoJogador)
//                danoNoinimigo = danoJogador;
//            else
//                danoNoinimigo = mobAlvo.PontosDeVida;
//            mobAlvo.PontosDeVida -= danoNoinimigo;
//            embed.WithDescription($"{mobAlvo.Nome}(ID {id}) perdeu -{danoNoinimigo.Texto2Casas()} de vida.");
//            StringBuilder mensagemAtaquesInimigos = new StringBuilder();
//            mensagemAtaquesInimigos.Append($"Você perdeu -{danoRecebido.Texto2Casas()} de vida.\n");
//            Enviamos a mensagem armazenada se ela não for vazia
//            if (danoRecebido != 0)
//                    embed.AddField($"**{"Danos recebidos".Titulo()}**", mensagemAtaquesInimigos.ToString());
//            Adicionamos mais mensagens
//            string t = personagem.Batalha.Turno + "º Turno";
//            embed.WithTitle($"**{t.Titulo()}**");
//            embed.WithColor(DiscordColor.Red);
//            if (mobAlvo.PontosDeVida <= 0)
//            {
//                Removemos o inimigo
//                personagem.Batalha.Inimigos.Remove(mobAlvo);
//                Enviamos uma mensagem avisando que ele morreu
//               mensagemMortos = $"{mobAlvo.Nome} morreu.";
//                embed.AddField($"**{"Inimigos abatidos".Titulo()}**", mensagemMortos.ToString());
//                Preparamos uma variavel para guardar a mensagem dos itens que caiu do inimgo
//               StringBuilder mensagemDrops = new StringBuilder();

//                Adicionamos a essencia para ser usado quando todos os inimigos forem mortos
//                bool isEvoluiou = personagem.AdicionarExp(mobAlvo.Essencia);
//                if (isEvoluiou)
//                    await ctx.RespondAsync($"{ctx.User.Mention}, você evoluiu para o nível {personagem.NivelAtual}! Você tem{personagem.Raca.Pontos} pontos disponíveis.");

//                foreach (var i in mobAlvo.ChanceItemUnico)
//                {
//                    Sorteia - se a chance de cair do item
//                      Se foi sorteado
//                    if (Sortear.Sucesso(i.ChanceDeCair))
//                    {
//                        Pega a data do item no Banco de dados
//                        ItemRPG itemData = ModuloBanco.ItemGet(i.ItemId);
//                        Sorteia - se a quantidade
//                        int quantidade = Sortear.Valor(1, i.QuantidadeMaxima);
//                        Adiciona no inventario o item conforme a quantidade sorteada
//                        personagem.Inventario.AdicionarItem(itemData, quantidade);
//                        Enviamos uma mensagem avisando a quantidade sorteada e o item que foi sorteado
//                        mensagemDrops.Append($"{quantidade} {itemData.Nome.PrimeiraLetraMaiuscula()}.\n");
//                        Finalizamos o sorteio de itens.
//                        break;
//                    }
//                }
//                Lista onde pode sortear todos os itens
//                Para cada item da lista
//                foreach (var i in mobAlvo.ChanceItemTodos)
//                {
//                    Sorteia - se a chance de cair do item
//                      Se foi sorteado
//                    if (Sortear.Sucesso(i.ChanceDeCair))
//                    {
//                        Pega a data do item no Banco de dados
//                        ItemRPG itemData = ModuloBanco.ItemGet(i.ItemId);
//                        Sorteia - se a quantidade
//                        int quantidade = Sortear.Valor(1, i.QuantidadeMaxima);
//                        Adiciona no inventario o item conforme a quantidade sorteada
//                        personagem.Inventario.AdicionarItem(itemData, quantidade);
//                        Enviamos uma mensagem avisando a quantidade sorteada e o item que foi sorteado
//                        mensagemDrops.Append($"{quantidade} {itemData.Nome.PrimeiraLetraMaiuscula()}.\n");
//                    }
//                }
//                Enviamos a mensagem dos itens que cai - o se não for nulo
//                 if (mensagemDrops.ToString() != "")
//                        embed.AddField($"**{"Recompensas".Titulo()}**", $"**{mensagemDrops.ToString()}**");
//            }

//            Salvamos o usuario
//            UsuarioRPG.Salvar(usuario);
//            Retornamos toda a mensagem preparada para ser enviada
//            await ctx.RespondAsync(embed: embed.Build());
//        }


//        public static double CalcDano(double resistencia, double dano)
//        {
//            double porcentagemFinal = 100 / (100 + resistencia);
//            Dano minimo sempre será o valor total dividido por 2.
//            return (Sortear.Valor((dano / 2), dano)) * porcentagemFinal;
//        }

//        public static async Task<double> CalcBatalhaMobsAsync(PersonagemRPG personagem, CommandContext ctx)
//        {
//            double danoRecebido = 0;
//            while (personagem.Batalha.PontosDeAcao < personagem.Batalha.PontosDeAcaoTotal)
//            {
//                personagem.Batalha.PontosDeAcao += personagem.Raca.Agilidade / 4 + Sortear.Valor(1, 10);
//                foreach (var inimigo in personagem.Batalha.Inimigos)
//                {
//                    inimigo.PontosDeAcao += (inimigo.Velocidade / 4) + Sortear.Valor(1, 10);

//                    if (inimigo.PontosDeAcao >= personagem.Batalha.PontosDeAcaoTotal)
//                    {
//                        personagem.Batalha.Turno++;
//                        inimigo.PontosDeAcao = 0;

//                        double danoInimigo = 0;
//                        Random r = new Random();
//                        int sorteioAtaque = r.Next(0, 5);
//                        ItemRPG armadura = null;
//                        switch (sorteioAtaque)
//                        {
//                            case 1:
//                                personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Botas, out armadura);
//                                break;
//                            case 2:
//                                personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Couraca, out armadura);
//                                break;
//                            case 3:
//                                personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Helmo, out armadura);
//                                break;
//                            case 4:
//                                personagem.Inventario.Equipamentos.TryGetValue(TipoItemEnum.Luvas, out armadura);
//                                break;
//                        }

//                        Se tiver
//                        if (armadura != null)
//                        {
//                            danoInimigo = CalcDano(armadura.DefesaFisica, inimigo.AtaqueFisico);
//                            armadura.Durabilidade--;
//                            if (armadura.Durabilidade == 0)
//                            {
//                                personagem.Inventario.DesequiparItem(armadura, personagem);
//                                await ctx.RespondAsync($"**({armadura.Nome})** quebrou! {ctx.User.Mention}!");
//                            }
//                        }
//                        else
//                            danoInimigo = CalcDano(0, inimigo.AtaqueFisico);
//                        personagem.VidaAtual -= danoInimigo;
//                        if (personagem.VidaAtual <= 0)
//                            try
//                            {
//                                DiscordGuild MundoZayn = await ModuloCliente.Client.GetGuildAsync(420044060720627712);
//                                DiscordChannel CanalRPG = MundoZayn.GetChannel(629281618447564810);
//                                await CanalRPG.SendMessageAsync($"*Um {inimigo.Nome} matou o {ctx.User.Username}!*");
//                            }
//                            catch { }
//                        danoRecebido += danoInimigo;
//                    }
//                }
//            }
//            personagem.Batalha.Turno++;
//            personagem.Batalha.PontosDeAcao = 0;
//            return danoRecebido;
//        }
//    }
//}