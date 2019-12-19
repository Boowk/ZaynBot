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
                de.WithDescription($"Parabéns {ctx.User.Mention}, você concluiu a criação do personagem.\n" +
                    $"Escreva `z!tutorial` para mais detalhes sobre como me usar.\n" +
                    $"Seja bem-vindo ao Zayn.");
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
