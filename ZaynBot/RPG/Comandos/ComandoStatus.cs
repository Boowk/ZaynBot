using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoStatus : BaseCommandModule
    {
        [Command("status")]
        [Description("Permite exibir os status do personagem.")]
        [ComoUsar("status [@usuario]")]
        [Exemplo("status @Talion Oak")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoStatusAb(CommandContext ctx, DiscordUser discordUser)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync(embed: GerarStatus(discordUser).Build());
        }

        [Command("status")]
        [ComoUsar("status")]
        [Exemplo("status")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoStatusAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync(embed: GerarStatus(ctx.User).Build());
        }

        public DiscordEmbedBuilder GerarStatus(DiscordUser user)
        {
            var usuario = ModuloBanco.GetJogador(user);

            RPGJogador personagem = usuario;

            DiscordEmoji pv = DiscordEmoji.FromGuildEmote(ModuloCliente.Client, 631907691467636736);
            DiscordEmoji pp = DiscordEmoji.FromGuildEmote(ModuloCliente.Client, 631907691425562674);
            DiscordEmoji pf = DiscordEmoji.FromName(ModuloCliente.Client, ":fork_and_knife:");

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithAuthor($"{user.Username} - Nível {personagem.NivelAtual}", iconUrl: user.AvatarUrl);
            int combate = 0, coleta = 0, total = 0;
            foreach (var proff in usuario.Proficiencias)
            {
                total += proff.Value.Pontos;
                switch (proff.Key)
                {
                    case EnumProficiencia.Ataque:
                        combate += proff.Value.Pontos;
                        break;
                    case EnumProficiencia.Defesa:
                        combate += proff.Value.Pontos;
                        break;
                    case EnumProficiencia.Forca:
                        combate += proff.Value.Pontos;
                        break;
                    case EnumProficiencia.Minerar:
                        coleta += proff.Value.Pontos;
                        break;
                    case EnumProficiencia.Cortar:
                        coleta += proff.Value.Pontos;
                        break;
                }
            }

            embed.WithDescription($"Tem {personagem.ExpAtual.Text().Bold()} pontos de experiencia e precisa de {(personagem.ExpMax - personagem.ExpAtual).Text().Bold()} para avançar.\n" +
                $"Matou **{usuario.MobsMortos}** e foi morto **{usuario.MortoPorMobs}** vezes por criaturas.\n" +
                $"Matou **{usuario.JogadoresMortos}** e foi morto **{usuario.MortoPorJogadores}** vezes por jogadores.\n" +
                $"Está carregando **{personagem.Mochila.Count}** itens.\n");

            embed.AddField("Info".Titulo(), $"{pv}**Vida:** {personagem.VidaAtual.Text()}/{personagem.VidaMaxima.Text()}\n" +
                $"{pp}**Magia:** {personagem.MagiaAtual.Text()}/{personagem.MagiaMaxima.Text()}\n" +
                $"{pf}**Fome:** {((personagem.FomeAtual / personagem.FomeMaxima) * 100).Text()}%\n", true);

            embed.AddField("Info".Titulo(), $"**Ataque físico:** {(personagem.AtaqueFisicoBase + personagem.AtaqueFisicoExtra).Text()}\n" +
                $"**Defesa física:** {(personagem.DefesaFisicaBase + personagem.DefesaFisicaExtra).Text()}\n" +
                $"**Defesa mágica:** {(personagem.DefesaMagicaBase + personagem.DefesaMagicaExtra).Text()}", true);

            embed.AddField("Proficiências distribuídas(PD)".Titulo(), $"**Combate:** {(((double)combate / (double)total) * 100.00).Text()}% |" +
                $" **Coleta:** {(((double)coleta / (double)total) * 100.00).Text()}%");

            string armaP = "Nehuma";
            if (usuario.Equipamentos.TryGetValue(EnumTipo.ArmaPrimaria, out var item))
                armaP = item.Nome.FirstUpper();
            embed.AddField("Arma primária".Titulo(), armaP, true);

            string armaS = "Nenhum";
            if (usuario.Equipamentos.TryGetValue(EnumTipo.ArmaSecundaria, out item))
                armaS = item.Nome.FirstUpper();
            embed.AddField("Arma secundária".Titulo(), armaS, true);

            string elmo = "Nenhum";
            if (usuario.Equipamentos.TryGetValue(EnumTipo.Elmo, out item))
                elmo = item.Nome.FirstUpper();
            embed.AddField("Elmo".Titulo(), elmo, true);

            string peitoral = "Nenhum";
            if (usuario.Equipamentos.TryGetValue(EnumTipo.Peitoral, out item))
                peitoral = item.Nome.FirstUpper();
            embed.AddField("Peitoral".Titulo(), peitoral, true);

            string pernas = "Nenhum";
            if (usuario.Equipamentos.TryGetValue(EnumTipo.Pernas, out item))
                pernas = item.Nome.FirstUpper();
            embed.AddField("Pernas".Titulo(), pernas, true);

            string luvas = "Nenhum";
            if (usuario.Equipamentos.TryGetValue(EnumTipo.Luvas, out item))
                luvas = item.Nome.FirstUpper();
            embed.AddField("Luvas".Titulo(), luvas, true);

            string botas = "Nenhum";
            if (usuario.Equipamentos.TryGetValue(EnumTipo.Botas, out item))
                botas = item.Nome.FirstUpper();
            embed.AddField("Botas".Titulo(), botas, true);

            string picareta = "Nenhum";
            if (usuario.Equipamentos.TryGetValue(EnumTipo.Picareta, out item))
                picareta = item.Nome.FirstUpper();
            embed.AddField("Picareta".Titulo(), picareta, true);

            string machado = "Nenhum";
            if (usuario.Equipamentos.TryGetValue(EnumTipo.Machado, out item))
                machado = item.Nome.FirstUpper();
            embed.AddField("Machado".Titulo(), machado, true);

            return embed;
        }
    }
}