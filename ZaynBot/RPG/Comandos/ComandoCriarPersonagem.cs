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
        [Description("Cria um personagem.")]
        [UsoAtributo("criar-personagem")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task ReencarnarComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario usuario = ModuloBanco.UsuarioGet(ctx.User.Id);
            if (usuario == null)
            {
                usuario = new RPGUsuario(ctx.User.Id);
                ModuloBanco.UsuarioColecao.InsertOne(usuario);
                DiscordEmbedBuilder de = new DiscordEmbedBuilder();
                de.WithDescription($"Parabéns {ctx.User.Mention}, você concluiu a criação do personagem.\n" +
                    $"Agora você está prestes a entrar na Academia Brasileira de RPG Hideki, onde aprenderá a jogar Zayn.\n" +
                    $"Mesmo se você for um jogador experiente, vale a pena concluir a academia, já que os formandos são recompensados com moedas de ouro, equipamentos, experiência e pontos de missão.\n" +
                    $"Você pode sair da academia a qualquer minuto e retornar as lições mais tarde, você não precisa completar tudo de uma vez.\n" +
                    $"Se você é um jogador com problemas de interpretação, nosso servidor tem pessoas dedicadas a resolver a sua dúvida, como também temos mapas especiais que você pode achar úteis.\n" +
                    $"Escreva ajuda para mais detalhes.\n" +
                    $"   Aproveite as aventuras e seja bem - vindo ao Zayn.");
                await ctx.RespondAsync(embed: de);
                try
                {
                    DiscordGuild MundoZayn = await ModuloCliente.Client.GetGuildAsync(420044060720627712);
                    DiscordChannel CanalRPG = MundoZayn.GetChannel(519176927265947689);
                    await CanalRPG.SendMessageAsync($"*{ctx.User.Username}#{ctx.User.Discriminator} criou um personagem!*");
                }
                catch { }
            }
            else
            {
                await ctx.RespondAsync($"Você já tem um personagem {ctx.User.Mention}! Use `z!ajuda` para ver os comandos disponíveis.");
                return;
            }
        }
    }
}
