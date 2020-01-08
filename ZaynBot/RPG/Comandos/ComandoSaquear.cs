using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoSaquear : BaseCommandModule
    {
        [Command("saquear")]
        [Description("Permite saquear os itens de um mob.")]
        [ComoUsar("saquear")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task ComandoExaminarAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
            RPGMob mob = usuario.Personagem.Batalha.Mob;
            if (mob.VidaAtual <= 0 && mob.Item.QuantidadeRestante > 0)
            {
                //Pega a data do item no Banco de dados
                var itemData = ModuloBanco.GetItem(mob.Item.ItemID);

                usuario.Personagem.Mochila.AdicionarItem(itemData, mob.Item.QuantidadeRestante);
                mob.Item.QuantidadeRestante = 0;
                usuario.Salvar();
                await ctx.RespondAsync($"{DiscordEmoji.FromName(ctx.Client, ":inbox_tray:")} Adicionado +{mob.Item.QuantidadeRestante} [{itemData.Nome.FirstUpper()}] na mochila {ctx.User.Mention}!".Bold());
            }
        }
    }
}
