using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;

namespace ZaynBot.Core.Comandos
{
    public class Ajuda
    {
        [Command("ajuda")]
        [Aliases("h", "?")]
        [Description("Exibe os comandos, a descrição do comando, suas abreviações e argumentos.")]
        public async Task HelpAsync(CommandContext ctx, [Description("Nome do comando")]params string[] comando)
        {
            await ctx.TriggerTypingAsync();

            if (comando.IsNullOrEmpty())
            {
                await ctx.RespondAsync("```css\nLista de comandos```\n" +
                    "Use `z!ajuda [comando]` para obter mais ajuda sobre o comando específico, por exemplo: `z!ajuda ajuda`\n\n" +
                    "**Core -** `ajuda` `convite`\n" +
                    "**RPG -** `reencarnar` `localizacao` `norte` `sul` `leste` `oeste` `falar com` `perfil` `personagem` " +
                    "`inimigos` `missao` `raca`\n" +
                    "**Informativos -** `info ranque nivel`\n\n" +
                    "```csharp\n# Não inclua os colchetes do exemplo quando utilizar os comandos!```");
            }
            else
            {
                await ctx.CommandsNext.DefaultHelpAsync(ctx, comando);
            }
        }
    }
}
