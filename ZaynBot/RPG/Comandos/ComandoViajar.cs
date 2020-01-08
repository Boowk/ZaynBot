using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoViajar : BaseCommandModule
    {
        public async Task ViajarAbAsync(CommandContext ctx, EnumDirecao enumDirecao)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
            usuario.Personagem.Batalha = new RPGBatalha();
            await ViajandoAbAsync(usuario, enumDirecao, ctx);
        }

        public async Task ViajandoAbAsync(RPGUsuario usuario, EnumDirecao enumDirecao, CommandContext ctx)
        {
            RPGRegiao localAtual = RPGRegiao.GetRegiao(usuario.Personagem.RegiaoAtualId);
            foreach (var regiao in localAtual.SaidasRegioes)
            {
                if (regiao.Direcao == enumDirecao)
                {
                    usuario.Personagem.RegiaoAtualId = regiao.RegiaoId;
                    usuario.Salvar();
                    localAtual = RPGRegiao.GetRegiao(usuario.Personagem.RegiaoAtualId);
                    DiscordEmbedBuilder embedViajeNormal = new DiscordEmbedBuilder().Padrao("Viajem", ctx);
                    embedViajeNormal.WithDescription($"Você foi para o {enumDirecao.ToString()}.\n".Bold() +
                        $"----\n" +
                        $"{localAtual.Descrição}");
                    await ctx.RespondAsync(embed: embedViajeNormal.Build());
                    return;
                }
            }
            await ctx.RespondAsync($"{ctx.User.Mention} este caminho não está disponível!".Bold());
        }

        [Command("oeste")]
        [Aliases("o")]
        [Description("Explora a área Oeste.")]
        [Cooldown(1, 8, CooldownBucketType.User)]
        public async Task Oeste(CommandContext ctx)
        {
            await ViajarAbAsync(ctx, EnumDirecao.Oeste);
            await Task.CompletedTask;
        }

        [Command("norte")]
        [Aliases("n")]
        [Description("Explora a área Norte.")]
        [Cooldown(1, 8, CooldownBucketType.User)]
        public async Task Norte(CommandContext ctx)
        {
            await ViajarAbAsync(ctx, EnumDirecao.Norte);
            await Task.CompletedTask;
        }

        [Command("leste")]
        [Aliases("l")]
        [Description("Explora a área leste.")]
        [Cooldown(1, 8, CooldownBucketType.User)]
        public async Task Leste(CommandContext ctx)
        {
            await ViajarAbAsync(ctx, EnumDirecao.Leste);
            await Task.CompletedTask;
        }

        [Command("sul")]
        [Aliases("s")]
        [Description("Explora a área Sul.")]
        [Cooldown(1, 8, CooldownBucketType.User)]
        public async Task Sul(CommandContext ctx)
        {
            await ViajarAbAsync(ctx, EnumDirecao.Sul);
            await Task.CompletedTask;
        }
    }
}
