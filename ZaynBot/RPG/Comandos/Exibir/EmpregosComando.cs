using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Exibir
{
    public class EmpregosComando : BaseCommandModule
    {
        [Command("empregos")]
        [Description("Exibe todos os empregos disponível.")]
        [UsoAtributo("empregos")]
        public async Task EmpregosComandoAb(CommandContext ctx)
        {
            //await ctx.TriggerTypingAsync();
            //UsuarioRPG.TryGetPersonagemRPG(ctx, out UsuarioRPG usuario);
            //PersonagemRPG personagem = usuario.Personagem;

            //DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Empregos", ctx);
            ////embed.WithDescription(string.Join(", ", personagem.Empregos.Select(xc => xc.Key.PrimeiraLetraMaiuscula())));
            //StringBuilder str = new StringBuilder();
            //int index = 0;
            //foreach (var item in personagem.Empregos)
            //{
            //    str.Append($"**{item.Key.PrimeiraLetraMaiuscula()}** Nv.{item.Value.Nivel}");
            //    index++;
            //    if (personagem.Empregos.Count != index)
            //        str.Append(", ");
            //}
            //embed.WithDescription(str.ToString());
            //embed.WithColor(DiscordColor.Lilac);
            //await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
