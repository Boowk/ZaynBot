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
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task ReencarnarComandoAb(CommandContext ctx, string aceitar = "nao")
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG usuario = ModuloBanco.UsuarioGet(ctx.User.Id);
            if (usuario == null)
            {
                await ctx.RespondAsync($"Após algum tempo desconhecido, {ctx.User.Mention} acorda. " +
                    $"Ao abrir os olhos, {ctx.User.Mention} enxerga uma escuridão sem fim. A sua cabeça do lateja com dor, " +
                    $"como se estivesse prestes a explodir. O cheiro de terra e mofo se eleva das grandes " +
                    $"pedras que revestem o interior dessa minúscula área úmida. Um gotejamento constante ecoa do teto " +
                    $"baixo do canto ao lado de um caminho estreito, e um único candelabro de ferro se projeta para fora da parede, " +
                    $"segurando uma vela curta e gorda. Fumaça se enrola no ar enquanto a chama resplandece ruidosamente em seu pavio " +
                    $"ardente. No canto, uma faca com sangue seco meio congelado, em baixo, respingos de sangue seco meio congelados " +
                    $"por onde eles deslizaram pelas pedras.");
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
