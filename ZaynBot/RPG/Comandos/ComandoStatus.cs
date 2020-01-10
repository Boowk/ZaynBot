using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoStatus : BaseCommandModule
    {
        [Command("status")]
        [Description("Exibe os status do personagem.")]
        [ComoUsar("status")]
        [Exemplo("status")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoStatusAb(CommandContext ctx, DiscordUser discordUser)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync(embed: GerarStatus(discordUser).Build());
        }

        [Command("status")]
        [ComoUsar("status [usuario]")]
        [Exemplo("status @Usuario")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoStatusAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync(embed: GerarStatus(ctx.User).Build());
        }

        public DiscordEmbedBuilder GerarStatus(DiscordUser user)
        {
            RPGUsuario.GetUsuario(user, out RPGUsuario usuario);
            RPGPersonagem personagem = usuario.Personagem;

            DiscordEmoji pv = DiscordEmoji.FromGuildEmote(ModuloCliente.Client, 631907691467636736);
            DiscordEmoji pp = DiscordEmoji.FromGuildEmote(ModuloCliente.Client, 631907691425562674);
            DiscordEmoji pf = DiscordEmoji.FromName(ModuloCliente.Client, ":fork_and_knife:");
            DiscordEmoji pg = DiscordEmoji.FromName(ModuloCliente.Client, ":tumbler_glass:");

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithAuthor($"{user.Username} - Nível {personagem.NivelAtual}", iconUrl: user.AvatarUrl);

            int combate = 0, total = 0;
            foreach (var proff in usuario.Personagem.Proficiencias)
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
                }
            }

            embed.WithDescription($"Tem {personagem.ExpAtual.Text().Bold()} pontos de experiencia e precisa de {(personagem.ExpMax - personagem.ExpAtual).Text().Bold()} para avançar.\n" +
                $"Matou **{usuario.RipMobs}** e foi morto **{usuario.RipPorMobs}** vezes por criaturas.\n" +
                $"Matou **0** e foi morto **0** vezes por jogadores.\n" +
                $"Está carregando **{personagem.Mochila.Itens.Count}** itens.\n");

            embed.AddField("Info".Titulo(), $"{pv}**Vida:** {personagem.VidaAtual.Text()}/{personagem.VidaMaxima.Text()}\n" +
                $"{pp}**Magia:** {personagem.MagiaAtual.Text()}/{personagem.MagiaMaxima.Text()}\n" +
                $"{pf}**Fome:** {((personagem.FomeAtual / personagem.FomeMaxima) * 100).Text()}%\n" +
                $"{pg}**Sede:** {((personagem.SedeAtual / personagem.SedeMaxima) * 100).Text()}%\n", true);

            embed.AddField("Info".Titulo(), $"**Ataque físico:** {(personagem.AtaqueFisicoBase + personagem.AtaqueFisicoExtra).Text()}\n" +
                $"**Ataque mágico:** {(personagem.AtaqueMagicoBase + personagem.AtaqueMagicoExtra).Text()}\n" +
                $"**Defesa física:** {(personagem.DefesaFisicaBase + personagem.DefesaFisicaExtra).Text()}\n" +
                $"**Defesa mágica:** {(personagem.DefesaMagicaBase + personagem.DefesaMagicaExtra).Text()}", true);

            embed.AddField("Proficiências distribuídas(PD)".Titulo(), $"**Combate:** {(combate / total) * 100}%");

            RPGMochila mochila = personagem.Mochila;
            if (mochila.Equipamentos.TryGetValue(EnumTipo.ArmaPrimaria, out var armaP))
                embed.AddField("Arma primária".Titulo(), armaP.Nome, true);
            else
                embed.AddField("Arma primária".Titulo(), "Nenhuma", true);


            if (mochila.Equipamentos.TryGetValue(EnumTipo.ArmaSecundaria, out var armaS))
                embed.AddField("Arma segundaria".Titulo(), armaS.Nome, true);
            else
                embed.AddField("Arma segundaria".Titulo(), "Nenhuma", true);


            if (mochila.Equipamentos.TryGetValue(EnumTipo.Helmo, out var helmo))
                embed.AddField("Helmo".Titulo(), helmo.Nome, true);
            else
                embed.AddField("Helmo".Titulo(), "Nenhuma", true);


            if (mochila.Equipamentos.TryGetValue(EnumTipo.Peitoral, out var peitoral))
                embed.AddField("Peitoral".Titulo(), peitoral.Nome, true);
            else
                embed.AddField("Peitoral".Titulo(), "Nenhuma", true);


            if (mochila.Equipamentos.TryGetValue(EnumTipo.Pernas, out var pernas))
                embed.AddField("Pernas".Titulo(), pernas.Nome, true);
            else
                embed.AddField("Pernas".Titulo(), "Nenhuma", true);


            if (mochila.Equipamentos.TryGetValue(EnumTipo.Luvas, out var luvas))
                embed.AddField("Luvas".Titulo(), luvas.Nome, true);
            else
                embed.AddField("Luvas".Titulo(), "Nenhuma", true);


            if (mochila.Equipamentos.TryGetValue(EnumTipo.Botas, out var botas))
                embed.AddField("Botas".Titulo(), botas.Nome, true);
            else
                embed.AddField("Botas".Titulo(), "Nenhuma", true);

            return embed;
        }
    }
}