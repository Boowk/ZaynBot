using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Combate
{
    public class ComandoHabilidade
    {
        [Command("habilidade")]
        [Cooldown(1, 2, CooldownBucketType.User)]
        [Description("Exibe as informações de uma habilidade.\n\n" +
       "Exemplo: z!habilidade [nome]\n\n" +
           "Uso: z!habilidade regeneração")]
        public async Task ComandoHabilidadeAb(CommandContext ctx, [RemainingText] string nome)
        {
            RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioBaseAsync(ctx);
            RPGPersonagem personagem = usuario.Personagem;
            if (string.IsNullOrWhiteSpace(nome))
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, qual é o nome da habilidade que deseja examinar?");
                return;
            }

            personagem.Habilidades.TryGetValue(nome.ToLower(), out RPGHabilidade habilidade);
            if (habilidade == null)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, habilidade não encontrada.");
                return;
            }

            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao();
            embed.WithTitle(habilidade.Nome.PrimeiraLetraMaiuscula().Titulo());
            embed.WithDescription(habilidade.Descricao);
            if (habilidade.Cura == true)
            {
                float quantidadeCura = usuario.Personagem.DefesaMagica * habilidade.CuraQuantidadePorcentagem;
                embed.AddField("Quantidade de cura".Titulo(), quantidadeCura.Texto2Casas());
            }

            embed.AddField("Experiência".Titulo(), $"{habilidade.ExperienciaAtual.Texto2Casas()}/{habilidade.ExperienciaProximoNivel.Texto2Casas()}");
            embed.AddField("Nível".Titulo(), habilidade.Nivel.ToString());
            if (habilidade.Passiva == true)
                embed.AddField("Custo de mana".Titulo(), "Habilidade passiva - sem custo");
            else
                embed.AddField("Custo de mana".Titulo(), habilidade.CustoMana.Texto2Casas());
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
