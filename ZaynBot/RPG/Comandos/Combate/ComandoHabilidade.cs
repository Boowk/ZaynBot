using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Combate
{
    public class ComandoHabilidade
    {
        [Command("habilidade")]
        [Description("Exibe as informações de uma habilidade.\n\n" +
       "Exemplo: z!habilidade [nome]\n\n" +
           "Uso: z!habilidade regeneração")]
        public async Task ComandoHabilidadeAb(CommandContext ctx, [RemainingText] string nome)
        {
            RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioBaseAsync(ctx);
            RPGPersonagem personagem = usuario.Personagem;
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
                embed.AddField("Quantidade de cura".Titulo(), quantidadeCura.ToString());
            }

            embed.AddField("Experiência".Titulo(), $"{habilidade.ExperienciaAtual}/{habilidade.ExperienciaProximoNivel}");
            embed.AddField("Nível".Titulo(), habilidade.Nivel.ToString());
            if (habilidade.Passiva == true)
                embed.AddField("Custo de mana".Titulo(), "Habilidade passiva - sem custo");
            else
                embed.AddField("Custo de mana".Titulo(), habilidade.CustoMana.ToString());
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
