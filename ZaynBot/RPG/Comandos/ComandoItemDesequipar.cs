using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoItemDesequipar : BaseCommandModule
    {
        [Command("desequipar")]
        [Aliases("deq")]
        [Description("Permite desequipar itens.")]
        [ComoUsar("desequipar [região]")]
        [Exemplo("desequipar peitoral")]
        [Exemplo("desequipar elmo")]
        [Cooldown(1, 15, CooldownBucketType.User)]
        public async Task ComandoDesequiparAb(CommandContext ctx, [RemainingText] string nome = "")
        {
            await ctx.TriggerTypingAsync();
            if (string.IsNullOrWhiteSpace(nome))
            {
                await ctx.ExecutarComandoAsync("ajuda desequipar");
                return;
            }

            var usuario = ModuloBanco.GetJogador(ctx);
            nome = nome.ToLower();
            string equipamento = "";
            switch (nome)
            {
                case "arma primaria":
                case "arma primária":
                case "arma":
                case "arma 1":
                case "arma principal":
                    equipamento = usuario.DesequiparItem(EnumTipo.ArmaPrimaria);
                    break;
                case "arma secundaria":
                case "arma 2":
                    equipamento = usuario.DesequiparItem(EnumTipo.ArmaPrimaria);
                    break;
                case "elmo":
                    equipamento = usuario.DesequiparItem(EnumTipo.Elmo);
                    break;
                case "peitoral":
                    equipamento = usuario.DesequiparItem(EnumTipo.Peitoral);
                    break;
                case "luvas":
                case "luva":
                    equipamento = usuario.DesequiparItem(EnumTipo.Luvas);
                    break;
                case "botas":
                case "bota":
                    equipamento = usuario.DesequiparItem(EnumTipo.Botas);
                    break;
                case "pernas":
                case "perna":
                    equipamento = usuario.DesequiparItem(EnumTipo.Pernas);
                    break;
                default:
                    await ctx.ExecutarComandoAsync("ajuda desequipar");
                    return;
            }

            if (!string.IsNullOrEmpty(equipamento))
            {
                usuario.Salvar();
                await ctx.RespondAsync($"[{equipamento}] desequipado {ctx.User.Mention}!".Bold());
            }
            else
                await ctx.RespondAsync($"Você não tem este item equipado {ctx.User.Mention}!".Bold());

        }
    }
}
