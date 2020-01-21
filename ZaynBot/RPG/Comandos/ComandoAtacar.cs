using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoAtacar : BaseCommandModule
    {
        public double ReduzirDano(double armadura, double dano, double danoExtra = 0)
        {
            double danoSorteado = Sortear.Valor((dano / 2), dano + danoExtra);
            double reducao = (armadura * danoSorteado) / (armadura + 10 * danoSorteado);
            danoSorteado -= reducao;
            if (danoSorteado < 0)
                return 0;
            return danoSorteado;
        }

        [Command("atacar")]
        [Aliases("at")]
        [Description("Permite atacar a criatura que você encontrou explorando.")]
        [ComoUsar("atacar")]
        [Cooldown(1, 1, CooldownBucketType.User)]
        public async Task ComandoAtacarAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var jogador = ModuloBanco.GetJogador(ctx);

            if (jogador.Batalha.Mob.VidaAtual <= 0)
            {
                await ctx.RespondAsync($"Use `z!explorar` para encontrar novas criaturas {ctx.User.Mention}!".Bold());
                return;
            }

            var criatura = jogador.Batalha.Mob;
            StringBuilder strRelatorio = new StringBuilder();


            // Vez criatura

            double dano = ReduzirDano(jogador.DefesaFisicaBase + jogador.DefesaFisicaExtra, criatura.AtaqueFisico);
            jogador.RemoverVida(dano);
            strRelatorio.AppendLine($"< {criatura.Nome.Underline()} > atacou causando {dano.Text()} de dano!".Bold());


            // Vez jogador

            if (jogador.FomeAtual <= 0)
            {
                double fome = jogador.VidaMaxima / 0.02;
                jogador.RemoverVida(fome);
                strRelatorio.AppendLine($"Faminto! -{fome} vida.".Bold());
            }

            jogador.Proficiencias.TryGetValue(EnumProficiencia.Forca, out RPGProficiencia forca);
            ProficienciaForca profForca = forca as ProficienciaForca;

            double danoNoMob = ReduzirDano(0, jogador.AtaqueFisicoBase + jogador.AtaqueFisicoExtra, profForca.CalcDanoExtra(jogador.AtaqueFisicoBase));
            if (criatura.VidaAtual < danoNoMob)
            {
                danoNoMob = criatura.VidaAtual;
                criatura.VidaAtual = 0;
            }
            else
                criatura.VidaAtual -= danoNoMob;
            strRelatorio.AppendLine($"{ctx.User.Mention} atacou causando {danoNoMob.Text()} de dano!".Bold());

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Ataque", ctx);
            embed.WithColor(DiscordColor.IndianRed);

            embed.AddField(ctx.User.Username.Titulo(), $"{DiscordEmoji.FromName(ctx.Client, ":heart:")} {jogador.VidaAtual.Text()}/{jogador.VidaMaxima.Text()}", true);

            if (criatura.VidaAtual <= 0)
            {
                strRelatorio.AppendLine($"{DiscordEmoji.FromName(ctx.Client, ":skull_crossbones:")} {criatura.Nome} {DiscordEmoji.FromName(ctx.Client, ":skull_crossbones:")}".Bold());
                strRelatorio.AppendLine($"{DiscordEmoji.FromName(ctx.Client, ":inbox_tray:")} +12 [Zayn]!".Bold());
                strRelatorio.AppendLine($"{DiscordEmoji.FromName(ctx.Client, ":inbox_tray:")} +1 [Ossos]!".Bold());
                strRelatorio.AppendLine($"{DiscordEmoji.FromName(ctx.Client, ":inbox_tray:")} +1 [Frasco vermelho]!".Bold());
                strRelatorio.AppendLine($"{DiscordEmoji.FromName(ctx.Client, ":inbox_tray:")} +8 XP!".Bold());
                jogador.Mochila.AdicionarItem("zayn", 12);
                jogador.Mochila.AdicionarItem("frasco vermelho", 1);
                jogador.Mochila.AdicionarItem("ossos", 1);
                jogador.MobsMortos++;
                if (jogador.AdicionarExp(8))
                    strRelatorio.Append($"Subiu para o nível {jogador.NivelAtual}! +2% {DiscordEmoji.FromName(ctx.Client, ":muscle:")} +1 PP!".Bold());
            }
            else
            {
                double porcentagem = criatura.VidaAtual / criatura.VidaMax;
                string porcentagemText = (porcentagem * 100).Text() + "%";
                string vidaMob = "";
                if (porcentagem > 0.7)
                    vidaMob = $"{DiscordEmoji.FromName(ctx.Client, ":green_heart:")} {porcentagemText}";
                else if (porcentagem > 0.4)
                    vidaMob = $"{DiscordEmoji.FromName(ctx.Client, ":yellow_heart:")} {porcentagemText}";
                else if (porcentagem > 0)
                    vidaMob = $"{DiscordEmoji.FromName(ctx.Client, ":heart:")} {porcentagemText}";
                embed.AddField("< " + criatura.Nome.Underline().Bold() + " >", vidaMob, true);
            }
            embed.WithDescription(strRelatorio.ToString());

            jogador.Salvar();
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
