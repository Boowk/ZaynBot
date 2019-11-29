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

namespace ZaynBot.RPG.Comandos.Exibir
{
    public class HabilidadeComando : BaseCommandModule
    {
        [Command("habilidade")]
        [Cooldown(1, 2, CooldownBucketType.User)]
        [Description("Exibe as informações de uma habilidade ou todas as habilidades de um personagem.")]
        [UsoAtributo("habilidade [nome]")]
        [ExemploAtributo("habilidade perfurante")]
        [ExemploAtributo("habilidade")]
        public async Task HabilidadeComandoAb(CommandContext ctx, string habNome = "")
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.TryGetPersonagemRPG(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao();

            if (string.IsNullOrWhiteSpace(habNome))
            {
                StringBuilder str = new StringBuilder();
                foreach (var item in personagem.Habilidades)
                {
                    str.Append($"`{item.Value.Nome.PrimeiraLetraMaiuscula()}`, ");
                }
                embed.AddField("Habilidades".Titulo(), str.ToString());
            }
            else
            {
                bool isAchou = personagem.TryGetHabilidade(habNome, out HabilidadeRPG habilidade);
                if (!isAchou)
                {
                    await ctx.RespondAsync($"{ctx.User.Mention}, habilidade não encontrada!");
                    return;
                }
                embed.WithTitle(habilidade.Nome.Titulo())
                .AddField("Nível".Titulo(), $"Nv.{habilidade.NivelAtual}/{habilidade.NivelMax}", true)
                .AddField("Experiencia".Titulo(), $"{habilidade.ExpAtual.Texto2Casas()}/{habilidade.ExpMax.Texto2Casas()}", true);
            }
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}

