using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.Comandos
{
    public class Basicos
    {
        [Command("conquistas")]
        [Description("Exibe todas as conquistas.")]
        [AtributoComoUsar("conquistas")]
        [Cooldown(1, 5, CooldownBucketType.User)]
        public async Task ConquistaAsync(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            var usuario = ModBanco.GetUsuario(ctx.User.Id);
            StringBuilder srb = new StringBuilder();
            foreach (var item in usuario.Conquistas)
                srb.AppendLine($"{item.Value.ProgressoAtual} {item.Value.Nome}.".Bold());
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Conquistas", ctx);
            embed.WithDescription(srb.ToString());
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
