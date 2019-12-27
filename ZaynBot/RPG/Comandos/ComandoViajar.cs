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
            if (usuario.Personagem.Batalha.Mob.VidaAtual > 0)
                if (Sortear.Sucesso(0.3))
                {
                    usuario.Personagem.Batalha = new RPGBatalha();
                    await ViajandoAbAsync(usuario, enumDirecao, ctx);
                }
                else
                {
                    ComandoAtacar f = new ComandoAtacar();
                    await f.ComandoAtacarAb(ctx);
                    await Task.CompletedTask;
                }
            else
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
                    localAtual = RPGRegiao.GetRegiao(usuario.Personagem.RegiaoAtualId);
                    usuario.Salvar();
                    DiscordEmbedBuilder embedViajeNormal = new DiscordEmbedBuilder().Padrao("Viajem", ctx);
                    embedViajeNormal.WithDescription($"Você foi para o {enumDirecao.ToString()}.");
                    embedViajeNormal.AddField("----", localAtual.Descrição);
                    await ctx.RespondAsync(embed: embedViajeNormal.Build());
                    return;
                }
            }
            await ctx.RespondAsync($"{ctx.User.Mention} você não pode ir para esta direção!");
        }

        [Command("oeste")]
        [Aliases("o")]
        [Description("Explora a área Oeste.")]
        [Cooldown(1, 6, CooldownBucketType.User)]
        public async Task Oeste(CommandContext ctx)
        {
            await new ComandoViajar().ViajarAbAsync(ctx, EnumDirecao.Oeste);
            await Task.CompletedTask;
        }

        [Command("norte")]
        [Aliases("n")]
        [Description("Explora a área Norte.")]
        [Cooldown(1, 6, CooldownBucketType.User)]
        public async Task Norte(CommandContext ctx)
        {
            await new ComandoViajar().ViajarAbAsync(ctx, EnumDirecao.Norte);
            await Task.CompletedTask;
        }

        [Command("leste")]
        [Aliases("l")]
        [Description("Explora a área leste.")]
        [Cooldown(1, 6, CooldownBucketType.User)]
        public async Task Leste(CommandContext ctx)
        {
            await new ComandoViajar().ViajarAbAsync(ctx, EnumDirecao.Leste);
            await Task.CompletedTask;
        }

        [Command("sul")]
        [Aliases("s")]
        [Description("Explora a área Sul.")]
        [Cooldown(1, 6, CooldownBucketType.User)]
        public async Task Sul(CommandContext ctx)
        {
            await new ComandoViajar().ViajarAbAsync(ctx, EnumDirecao.Sul);
            await Task.CompletedTask;
        }
    }
}
