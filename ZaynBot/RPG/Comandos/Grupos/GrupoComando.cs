using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Grupos
{
    [Group("grupo")]
    public class GrupoComando : BaseCommandModule
    {
        [Command("criar")]
        [Description("Permite criar um novo grupo.")]
        [UsoAtributo("grupo [nome]")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task GrupoCriar(CommandContext ctx, [RemainingText] string nome = "")
        {
            await ctx.TriggerTypingAsync();
            UsuarioRPG.GetPersonagem(ctx, out UsuarioRPG usuario);
            PersonagemRPG personagem = usuario.Personagem;

            if (personagem.Batalha.LiderGrupo != 0)
            {
                await ctx.RespondAsync($"Você precisa sair do Grupo atual antes! {ctx.User.Mention}");
                return;
            }

            if (string.IsNullOrWhiteSpace(nome))
            {
                await ctx.RespondAsync($"Você precisa dar um nome ao seu Grupo! {ctx.User.Mention}");
                return;
            }

            if (nome.Length > 20)
            {
                await ctx.RespondAsync($"O nome do Grupo não pode ser maior que 20 letras! {ctx.User.Mention}");
                return;
            }

            personagem.Batalha.LiderGrupo = ctx.User.Id;
            personagem.Batalha.NomeGrupo = nome;
            usuario.Salvar();
            await ctx.RespondAsync($"O Grupo **{nome}** foi criado! {ctx.User.Mention}");
        }
    }
}
