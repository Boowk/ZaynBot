using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Exibir
{
    public class ReceitaComando : BaseCommandModule
    {
        [Command("receitas")]
        [Cooldown(1, 4, CooldownBucketType.User)]
        [Description("Exibe as informações de uma receita ou todas as receitas disponíveis.")]
        [UsoAtributo("receitas [id|]")]
        [ExemploAtributo("receitas 2")]
        [ExemploAtributo("receitas")]
        public async Task HabilidadeComandoAb(CommandContext ctx, string idText = "-1")
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.GetPersonagem(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao();
            int id = -1;
            try
            {
                id = Convert.ToInt32(idText);
            }
            catch { }

            if (id < 0)
            {
                StringBuilder str = new StringBuilder();
                List<ReceitaRPG> receitas = ModuloBanco.ReceitaColecao.Find(FilterDefinition<ReceitaRPG>.Empty).ToList();
                foreach (var item in receitas)
                {
                    str.Append($"`{item.Nome.PrimeiraLetraMaiuscula()}(ID {item.Id})`, ");
                }
                embed.AddField("Receitas".Titulo(), str.ToString());
            }
            else
            {
                ReceitaRPG receita = ModuloBanco.ReceitaGet(id);
                if (receita == null)
                {
                    await ctx.RespondAsync($"{ctx.User.Mention}, receita não encontrada!");
                    return;
                }
                embed.WithTitle($"{receita.Nome.PrimeiraLetraMaiuscula()}(ID {receita.Id}))");
                embed.AddField("Resultado", $"{receita.Quantidade} - {ModuloBanco.ItemGet(receita.Resultado).Nome}");
                StringBuilder str = new StringBuilder();
                foreach (var item in receita.Ingredientes)
                {
                    str.AppendLine($"{item.Value} - {ModuloBanco.ItemGet(item.Key).Nome}");
                }
                embed.AddField("Ingredientes", str.ToString());
            }
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
