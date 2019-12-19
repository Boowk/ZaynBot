using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;
using ZaynBot.RPG.Habilidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoProficiencia : BaseCommandModule
    {
        [Command("proficiencia")]
        [Cooldown(1, 2, CooldownBucketType.User)]
        [Description("Exibe todas as proficiencia de um personagem ou mais detalhes sobre uma em especifico")]
        [ComoUsar("proficiencia [nome]")]
        [Exemplo("proficiencia perfurante")]
        [Exemplo("proficiencia")]
        public async Task HabilidadeComandoAb(CommandContext ctx, string habNome = "")
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
            RPGPersonagem personagem = usuario.Personagem;
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao();

            if (string.IsNullOrWhiteSpace(habNome))
            {
                StringBuilder str = new StringBuilder();
                foreach (var item in personagem.Proficiencias)
                {
                    str.Append($"`{item.Value.Nome.PrimeiraLetraMaiuscula()}`, ");
                }
                embed.AddField("Proficiencia".Titulo(), str.ToString());
            }
            else
            {
                bool isAchou = personagem.TryGetHabilidade(habNome, out RPGProficiencia habilidade);
                if (!isAchou)
                {
                    await ctx.RespondAsync($"{ctx.User.Mention}, proficiencia não encontrada!");
                    return;
                }
                embed.WithTitle(habilidade.Nome.Titulo())
                .AddField("Nível".Titulo(), $"Nv.{habilidade.NivelAtual}", true)
                .AddField("Experiencia".Titulo(), $"{Extensoes.Text(habilidade.ExpAtual)}/{Extensoes.Text(habilidade.ExpMax)}", true);
            }
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}

