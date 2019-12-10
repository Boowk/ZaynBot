using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Data.Itens;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Enuns;

namespace ZaynBot.RPG.Comandos
{
    public class ExaminarComando : BaseCommandModule
    {
        //[Command("examinar")]
        //[Description("Mostra uma breve descrição do item.\n\n" +
        //   "Exemplo: z!examinar [id item]\n\n" +
        //    "Uso: z!examinar 1:1")]
        //public async Task ComandoExaminarAb(CommandContext ctx, [RemainingText] string nome)
        //{
        //    await ctx.TriggerTypingAsync();
        //    UsuarioRPG.TryGetPersonagemRPG(ctx, out UsuarioRPG usuario);
        //    PersonagemRPG personagem = usuario.Personagem;
        //    if (string.IsNullOrWhiteSpace(nome))
        //    {
        //        await ctx.RespondAsync($"{ctx.User.Mention}, você precisa informar o id do item.");
        //        return;
        //    }
        //    ItemRPG item;
        //    bool temItem = personagem.Inventario.Itens.TryGetValue(nome, out ItemDataRPG itemData);
        //    if (!temItem)
        //    {
        //        await ctx.RespondAsync($"{ctx.User.Mention}, esse item é muito antigo, impossível examina-lo!");
        //        return;
        //    }

        //    item = ModuloBanco.ItemGet(itemData.Id);


        //    DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Item", ctx);
        //    embed.WithTitle($"**{item.Nome.PrimeiraLetraMaiuscula()}**");
        //    embed.WithDescription((item.Descricao) != "" ? item.Descricao : "Sem descrição");
        //    embed.AddField("Tipo".Titulo(), item.TipoItem.ToString(), true);
        //    if (item.TipoItem.HasFlag(TipoItemEnum.Arma) || item.TipoItem.HasFlag(TipoItemEnum.Armadura))
        //    {
        //        embed.AddField("Durabilidade".Titulo(), $"{itemData.Durabilidade}/{item.Durabilidade}", true);
        //        if (item.AtaqueFisico != 0)
        //            embed.AddField("Ataque físico".Titulo(), item.AtaqueFisico.Texto2Casas(), true);
        //        if (item.AtaqueMagico != 0)
        //            embed.AddField("Ataque mágico".Titulo(), item.AtaqueMagico.Texto2Casas(), true);
        //        if (item.DefesaFisica != 0)
        //            embed.AddField("Defesa física".Titulo(), item.DefesaFisica.Texto2Casas(), true);
        //        if (item.DefesaMagica != 0)
        //            embed.AddField("Defesa mágica".Titulo(), item.DefesaMagica.Texto2Casas(), true);
        //    }
        //    embed.WithColor(DiscordColor.Green);
        //    await ctx.RespondAsync(embed: embed.Build());
        //}
    }
}
