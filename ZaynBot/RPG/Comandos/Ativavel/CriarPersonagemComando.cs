using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Ativavel
{
    public class CriarPersonagemComando : BaseCommandModule
    {
        [Command("criar-personagem")]
        [Aliases("cp")]
        [Description("Cria um personagem.")]
        [UsoAtributo("criar-personagem")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task ReencarnarComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG usuario = ModuloBanco.UsuarioGet(ctx.User.Id);
            if (usuario == null)
            {
                usuario = new UsuarioRPG(ctx.User.Id);
                ModuloBanco.UsuarioColecao.InsertOne(usuario);
                DiscordEmbedBuilder de = new DiscordEmbedBuilder();
                de.WithDescription($"Após algum tempo desconhecido, você acorda. " +
                    $"Ao abrir os olhos, você enxerga uma escuridão sem fim. A sua cabeça do lateja com dor, " +
                    $"como se estivesse prestes a explodir.\n O cheiro de terra e mofo se eleva das grandes " +
                    $"pedras que revestem o interior dessa minúscula área úmida.\n Um gotejamento constante ecoa do teto " +
                    $"baixo do canto ao lado de um caminho estreito, e um único candelabro de ferro se projeta para fora da parede, " +
                    $"segurando uma vela curta e gorda.\n Fumaça se enrola no ar enquanto a chama resplandece ruidosamente em seu pavio " +
                    $"ardente.\n No canto, uma faca com sangue seco meio congelado, em baixo, respingos de sangue seco meio congelados " +
                    $"por onde eles deslizaram pelas pedras.");
                await ctx.RespondAsync($"Bem-vindo {ctx.User.Mention}!");
                await ctx.RespondAsync(embed: de);
                try
                {
                    DiscordGuild MundoZayn = await ModuloCliente.Client.GetGuildAsync(420044060720627712);
                    DiscordChannel CanalRPG = MundoZayn.GetChannel(519176927265947689);
                    await CanalRPG.SendMessageAsync($"*{ctx.User.Username} agora está jogando ZaynRPG.*");
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
