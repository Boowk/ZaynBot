using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Ativavel
{
    public class ReencarnarComando : BaseCommandModule
    {
        [Command("reencarnar")]
        [Description("Cria um novo personagem")]
        [UsoAtributo("reencarnar [sim|]")]
        [Cooldown(1, 500, CooldownBucketType.User)]
        public async Task ReencarnarComandoAb(CommandContext ctx, string aceitar = "nao")
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG usuario = ModuloBanco.UsuarioGet(ctx.User.Id);
            if (usuario == null)
            {
                await ctx.RespondAsync($"Um teto desconhecido… na verdade não. Em primeiro lugar, não há nenhum teto.\n" +
                $"Onde eu estou ? Ou melhor, que horas são ?\n" +
                $"… ha. Isto não é bom.\n" +
                $"Divirta-se no Dragons & Zayn's RPG!");
                usuario = new UsuarioRPG(ctx.User.Id);
                ModuloBanco.UsuarioColecao.InsertOne(usuario);
            }
            else
            {
                if (aceitar != "sim")
                {
                    await ctx.RespondAsync($"Atenção {ctx.User.Mention}! Reencarnar faz você perder todo o seu progresso. Use o comando `reencarnar sim` para reencarnar novamente.");
                    return;
                }
                usuario.Personagem = null;
                usuario.Personagem = new PersonagemRPG
                {
                    LocalAtualId = 0
                };
                usuario.Salvar();
            }
            await ctx.RespondAsync($"{ctx.User.Mention} acabou de reencanar como `{usuario.Personagem.Raca.Nome.PrimeiraLetraMaiuscula()}`.");
            try
            {
                DiscordGuild MundoZayn = await ModuloCliente.Client.GetGuildAsync(420044060720627712);
                DiscordChannel CanalRPG = MundoZayn.GetChannel(519176927265947689);
                await CanalRPG.SendMessageAsync($"*{ctx.User.Username} reencarnou como `{usuario.Personagem.Raca.Nome.PrimeiraLetraMaiuscula()}`.*");
            }
            catch { }
        }
    }
}
