using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Exibir
{
    public class EmpregoComando : BaseCommandModule
    {
        [Command("emprego")]
        [Description("Exibe informações do emprego atual")]
        [UsoAtributo("emprego")]
        [Cooldown(1, 6, CooldownBucketType.User)]
        public async Task EmpregoComandoAb(CommandContext ctx)
        {
            //    await ctx.TriggerTypingAsync();
            //    UsuarioRPG.TryGetPersonagemRPG(ctx, out UsuarioRPG usuario);
            //    PersonagemRPG personagem = usuario.Personagem;
            //    if (personagem.EmpregoAtualIndex == "desempregado")
            //    {
            //        await ctx.RespondAsync($"{ctx.User.Mention}, você está desempregado, troque de emprego antes.");
            //        return;
            //    }
            //    EmpregoRPG empregoatual = personagem.EmpregoGet();
            //    DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Emprego", ctx);
            //    embed.WithTitle(empregoatual.Nome.PrimeiraLetraMaiuscula().Titulo());
            //    embed.WithDescription(empregoatual.Descrição);
            //    if (empregoatual.Nivel == ModuloBanco.EmpregoGet(empregoatual.Nome).NivelMax)
            //        embed.AddField("Nível".Titulo(), $"Nv.{empregoatual.Nivel} Max", true);
            //    else
            //        embed.AddField("Nível".Titulo(), $"Nv.{empregoatual.Nivel}", true);
            //    embed.AddField("Experiencia".Titulo(), $"{empregoatual.ExperienciaAtual.Texto2Casas()}/{empregoatual.ExperienciaProximoNivel.Texto2Casas()}", true);
            //    await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
