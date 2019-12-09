using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Ativavel
{
    public class DencansarComando : BaseCommandModule
    {
        [Command("descansar")]
        [Description("Permite descansar para recuperar um pouco da energia perdida.")]
        [UsoAtributo("descansar")]
        [Cooldown(1, 30, CooldownBucketType.User)]
        public async Task AtacarComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.TryGetPersonagemRPG(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;
            double valorCura = personagem.VidaMax / 15;
            double valorRecuperado = usuario.Personagem.RecuperarVida(valorCura);
            double valorMagia = personagem.MagiaMax / 25;
            double magiaRecuperado = usuario.Personagem.RecuperarMagia(valorMagia);
            usuario.Salvar();
            if ((valorRecuperado > 0.0) && (magiaRecuperado > 0.0))
                await ctx.RespondAsync($"**({valorRecuperado.Texto2Casas()})** de vida e **({magiaRecuperado.Texto2Casas()})** de magia recuperados! {ctx.User.Mention}.");
            else if (valorRecuperado > 0.0)
                await ctx.RespondAsync($"**({valorRecuperado.Texto2Casas()})** de vida recuperada! {ctx.User.Mention}.");
            else if (magiaRecuperado > 0.0)
                await ctx.RespondAsync($"**({magiaRecuperado.Texto2Casas()})** de magia recuperada! {ctx.User.Mention}.");
            else
                await ctx.RespondAsync($"Já descansou demasiadamente! {ctx.User.Mention}.");
        }
    }
}
