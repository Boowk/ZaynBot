﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Exibir
{
    public class InimigosComandos : BaseCommandModule
    {
        [Command("inimigos")]
        [Description("Mostra os inimigos e a vida dos que você está batalhando com.")]
        [Cooldown(1, 10, CooldownBucketType.User)]

        public async Task VerInimigos(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.GetPersonagem(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;
            StringBuilder inimigos = new StringBuilder();
            int quant = 0;
            foreach (var item in personagem.Batalha.Inimigos)
                inimigos.Append($"{item.Nome}(ID {quant}) | Vida: {item.PontosDeVida.Texto2Casas()}\n");
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Inimigos", ctx);
            embed.WithDescription(inimigos.ToString());
            embed.WithColor(DiscordColor.Red);
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
