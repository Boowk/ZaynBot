﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace ZaynBot.Core.Comandos
{
    public class ComandoAjuda
    {
        [Command("ajuda")]
        [Aliases("h", "?")]
        [Description("Exibe os comandos, a descrição, suas abreviações e exemplos.\n\n" +
            "Uso: z!ajuda [comando]\n\n" +
            "Exemplo: z!ajuda ajuda")]
        public async Task ComandoAjudaAb(CommandContext ctx, [Description("Nome do comando")]params string[] comando)
        {
            await ctx.TriggerTypingAsync();
            if (comando.IsNullOrEmpty())
            {
                await ctx.RespondAsync("```css\nLista de comandos```\n" +
                    "Use `z!ajuda [comando]` para obter mais ajuda sobre o comando específico, por exemplo: `z!ajuda ajuda`\n\n" +
                    "**Core -** `ajuda` `convite` `informacao` `votar`\n" +
                    "**RPG -** `reencarnar` `localizacao` `norte` `sul` `leste` `oeste` `falar com` `perfil` `personagem` `atacar` " +
                    "`inimigos` `missao` `raca` `mochila` `pegar` `explorar` `coletar` `habilidades` `habilidade` `examinar`\n" +
                    "```csharp\n# Não inclua os colchetes do exemplo quando utilizar o comando!```");
            }
            else
            {
                await ctx.CommandsNext.DefaultHelpAsync(ctx, comando);
            }
        }
    }
}
