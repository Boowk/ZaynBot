using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Ativavel
{
    public class TrocarEmpregoComando : BaseCommandModule
    {
        [Command("trocaremprego")]
        [Description("Permite trocar de emprego quando disponível.")]
        [UsoAtributo("trocaremprego [emprego]")]
        [ExemploAtributo("trocaremprego plebeu")]
        [Aliases("te")]
        [Cooldown(1, 6, CooldownBucketType.User)]
        public async Task ComandoPersonagemAb(CommandContext ctx, [RemainingText] string nome)
        {
            //await ctx.TriggerTypingAsync();
            //UsuarioRPG.TryGetPersonagemRPG(ctx, out UsuarioRPG usuario);
            //PersonagemRPG personagem = usuario.Personagem;
            //if (string.IsNullOrWhiteSpace(nome))
            //{
            //    await ctx.RespondAsync($"{ctx.User.Mention}, qual é o nome do emprego?");
            //    return;
            //}
            //if (nome.ToLower() == personagem.EmpregoAtualIndex)
            //{
            //    await ctx.RespondAsync($"{ctx.User.Mention}, você já tem este emprego.");
            //    return;
            //}
            //EmpregoRPG emprego = personagem.EmpregoGet();

            //if (emprego.IsTrocaComPadre)
            //{
            //    await ctx.RespondAsync($"{ctx.User.Mention}, você somente pode trocar de emprego com a ajuda de um Padre.");
            //    return;
            //}

            //bool temEmprego = personagem.Empregos.TryGetValue(nome.ToLower(), out EmpregoRPG empregoEscolhido);
            //if (!temEmprego)
            //{
            //    await ctx.RespondAsync($"{ctx.User.Mention}, você não tem este emprego disponível!");
            //    return;
            //}
            //personagem.EmpregoAtualIndex = nome.ToLower();
            //await ctx.RespondAsync($"{ctx.User.Mention}, você trocou para o emprego ⌈{nome.ToLower().PrimeiraLetraMaiuscula()}⌋");
            //usuario.Salvar();
        }
    }
}
