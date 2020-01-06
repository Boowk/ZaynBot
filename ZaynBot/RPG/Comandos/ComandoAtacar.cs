using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;


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
        [Description("Ataca o npc que você encontrou explorando.")]
        [ComoUsar("atacar")]
        [Cooldown(1, 6, CooldownBucketType.User)]
        public async Task ComandoAtacarAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);

            if (usuario.Personagem.Batalha.Mob.VidaAtual <= 0)
            {
                await ctx.RespondAsync($"{ctx.User.Mention} use `z!explorar` para encontrar novos npcs!".Bold());
                return;
            }

            RPGMob mob = usuario.Personagem.Batalha.Mob;
            StringBuilder strRelatorio = new StringBuilder();
            bool vezJogador = false;
            do
            {
                mob.EstaminaAtual += mob.Velocidade;
                usuario.Personagem.EstaminaAtual += 4;

                //Vez mob
                if (mob.EstaminaAtual >= mob.EstaminaMaxima)
                {
                    mob.EstaminaAtual = 0;

                    usuario.Personagem.Batalha.Turno++;
                    strRelatorio.AppendLine($"Turno {usuario.Personagem.Batalha.Turno}.".Bold());

                    double chanceMobAtaque = mob.Precisao / usuario.Personagem.DefesaFisica;
                    if (Sortear.Sucesso(chanceMobAtaque))
                    {
                        usuario.Personagem.Proficiencias.TryGetValue(EnumProficiencia.Defesa, out RPGProficiencia defesa);
                        ProficienciaDefesa profDefesa = defesa as ProficienciaDefesa;
                        double dano = Sortear.Valor((mob.AtaqueFisico / 2), mob.AtaqueFisico) * profDefesa.CalcDefesa();
                        usuario.RemoverVida(dano);
                        strRelatorio.AppendLine($"{ctx.User.Mention} perdeu -{dano.Text()} vida.".Bold());
                    }
                    else
                        strRelatorio.AppendLine($"Você desviou do ataque.".Bold());
                }

                //Vez jogador
                if (usuario.Personagem.EstaminaAtual >= usuario.Personagem.EstaminaMaxima)
                {
                    vezJogador = true;
                    usuario.Personagem.EstaminaAtual = 0;
                    usuario.Personagem.Batalha.Turno++;
                    strRelatorio.AppendLine($"**Turno {usuario.Personagem.Batalha.Turno}.**");

                    //Perde fome e sede


                    if (usuario.Personagem.FomeAtual <= 0)
                    {
                        double fome = usuario.Personagem.VidaMaxima / 0.02;
                        usuario.RemoverVida(fome);
                        strRelatorio.AppendLine($"Faminto! -{fome} vida.".Bold());
                    }
                    if (usuario.Personagem.SedeAtual <= 0)
                    {
                        double sede = usuario.Personagem.VidaMaxima / 0.08;
                        usuario.RemoverVida(sede);
                        strRelatorio.AppendLine($"Desidratado! -{sede} vida.".Bold());
                    }
                }
            } while (vezJogador == false);

            usuario.Personagem.Proficiencias.TryGetValue(EnumProficiencia.Ataque, out RPGProficiencia ataque);
            ProficienciaAtaque profAtaque = ataque as ProficienciaAtaque;

            double chance = profAtaque.CalcChance(usuario.Personagem.Sorte, mob.DefesaFisica);
            if (Sortear.Sucesso(chance))
            {
                usuario.Personagem.Proficiencias.TryGetValue(EnumProficiencia.Forca, out RPGProficiencia forca);
                ProficienciaForca profForca = forca as ProficienciaForca;


                double dano = Sortear.Valor((usuario.Personagem.AtaqueFisico / 2), usuario.Personagem.AtaqueFisico + profForca.CalcDanoExtra(usuario.Personagem.AtaqueFisico));
                if (mob.VidaAtual < dano)
                    dano = mob.VidaAtual;
                else
                    mob.VidaAtual -= dano;
                strRelatorio.AppendLine($"**< {mob.Nome.Underline()} > perdeu -{dano.Text()} vida.**");
            }
            else
                strRelatorio.AppendLine($"Você errou o ataque.".Bold());

            #region Relatorio

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Ataque", ctx);
            embed.WithColor(DiscordColor.IndianRed);



            embed.AddField(ctx.User.Username.Titulo(), $"{DiscordEmoji.FromName(ctx.Client, ":heart:")} {usuario.Personagem.VidaAtual.Text()}/{usuario.Personagem.VidaMaxima.Text()}", true);

            if (mob.VidaAtual <= 0)
            {
                strRelatorio.AppendLine($"**{DiscordEmoji.FromName(ctx.Client, ":skull_crossbones:")} {mob.Nome} {DiscordEmoji.FromName(ctx.Client, ":skull_crossbones:")}**");
                usuario.RipMobs++;
                //Pega a data do item no Banco de dados
                MobItemDropRPG dropSorteado = mob.SortearDrop();
                var itemData = ModuloBanco.GetItem(dropSorteado.ItemId);

                int quantidade = Sortear.Valor(1, dropSorteado.QuantMax);
                usuario.Personagem.Mochila.AdicionarItem(itemData, quantidade);
                // Enviamos uma mensagem
                strRelatorio.AppendLine($"{DiscordEmoji.FromName(ctx.Client, ":inbox_tray:")} +{quantidade} [{itemData.Nome.FirstUpper()}]!".Bold());
                if (usuario.Personagem.AdicionarExp(mob.Essencia))
                    strRelatorio.Append($"Subiu para o nível {usuario.Personagem.NivelAtual}! +2% {DiscordEmoji.FromName(ctx.Client, ":muscle:")} +1 PP!".Bold());
            }
            else
            {
                double porcentagem = mob.VidaAtual / mob.VidaMax;
                string porcentagemText = (porcentagem * 100).Text() + "%";
                string vidaMob = "";
                if (porcentagem > 0.7)
                    vidaMob = $"{DiscordEmoji.FromName(ctx.Client, ":green_heart:")} {porcentagemText}";
                else if (porcentagem > 0.4)
                    vidaMob = $"{DiscordEmoji.FromName(ctx.Client, ":yellow_heart:")} {porcentagemText}";
                else if (porcentagem > 0)
                    vidaMob = $"{DiscordEmoji.FromName(ctx.Client, ":heart:")} {porcentagemText}";
                embed.AddField("< " + mob.Nome.Underline().Bold() + " >", vidaMob, true);
            }
            embed.WithDescription(strRelatorio.ToString());
            #endregion

            RPGUsuario.Salvar(usuario);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
