using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoCriarPersonagem : BaseCommandModule
    {
        [Command("criar-personagem")]
        [Aliases("cp")]
        [Description("Permite criar um personagem. Somente é possível usar este comando uma vez.")]
        [ComoUsar("criar-personagem")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task ComandoCriarPersonagemAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario usuario = ModuloBanco.GetUsuario(ctx.User.Id);
            if (usuario == null)
            {
                usuario = new RPGUsuario(ctx.User.Id);
                ModuloBanco.UsuarioColecao.InsertOne(usuario);
                DiscordEmbedBuilder de = new DiscordEmbedBuilder();
                de.WithDescription($"{ctx.User.Mention} você acabou de criar o seu personagem! [Clique aqui](https://zaynrpg.gitbook.io/zaynrpg/comecando/como-funciona)" +
                    $" para saber como usar o bot! Você também pode usar o comando `z!olhar`.");
                await ctx.RespondAsync(embed: de);
            }
            else
            {
                await ctx.RespondAsync($"Você já tem um personagem {ctx.User.Mention}! Use `z!ajuda` para ver os comandos disponíveis. " +
                    $"Se você está perdido, pode acessar nosso site: https://zaynrpg.gitbook.io/zaynrpg/comecando/como-funciona");
                return;
            }
        }
    }
}
