using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.Core.Comandos
{
    [Group("adm")]
    [RequireOwner]
    public class ComandoAdm : BaseCommandModule
    {
        [Command("mp")]
        public async Task Mp(CommandContext ctx, DiscordGuild guilda, DiscordUser usuario, [RemainingText] string texto = "")
        {
            await ctx.TriggerTypingAsync();
            DiscordMember membro = await guilda.GetMemberAsync(usuario.Id);
            await membro.SendMessageAsync(texto);
            await ctx.RespondAsync("**Enviado.**");
        }

        [Command("sudo")]
        public async Task Sudo(CommandContext ctx, DiscordUser member, [RemainingText] string command)
        {
            await ctx.TriggerTypingAsync();
            var invocation = command.Substring(2);
            var cmd = ctx.CommandsNext.FindCommand(invocation, out var args);
            if (cmd == null)
            {
                await ctx.RespondAsync("Comando não encontrado");
                return;
            }

            var cfx = ctx.CommandsNext.CreateFakeContext(member, ctx.Channel, "", "z!", cmd, args);
            await ctx.CommandsNext.ExecuteCommandAsync(cfx);
        }

        [Command("deletar")]
        public async Task Remover(CommandContext ctx, DiscordUser user = null)
        {
            if (user == null)
                user = ctx.User;
            await ctx.TriggerTypingAsync();
            ModuloBanco.UsuarioColecao.DeleteOne(x => x.Id == user.Id);
            await ctx.RespondAsync("Deletado!");
        }

        [Command("adicionar-item")]
        [Aliases("ai")]
        public async Task AdicionarItem(CommandContext ctx, int quantidade = 1, int id = 0, DiscordUser discordUser = null)
        {
            if (discordUser == null)
                discordUser = ctx.User;
            await ctx.TriggerTypingAsync();
            RPGItem item = ModuloBanco.GetItem(id);
            if (item == null)
            {
                await ctx.RespondAsync("Item não encontrado!");
                return;
            }
            RPGUsuario.GetUsuario(discordUser, out RPGUsuario usuario);
            usuario.Personagem.Mochila.AdicionarItem(item, quantidade);
            usuario.Salvar();
            await ctx.RespondAsync($"Adicionado {quantidade} [{item.Nome}] para {discordUser.Mention}!");
        }

        [Command("atualizar")]
        public async Task Atualizar(CommandContext ctx)
        {
            FilterDefinition<RPGUsuario> filter = FilterDefinition<RPGUsuario>.Empty;
            FindOptions<RPGUsuario> options = new FindOptions<RPGUsuario>
            {
                BatchSize = 8,
                NoCursorTimeout = false
            };

            using (IAsyncCursor<RPGUsuario> cursor = await ModuloBanco.UsuarioColecao.FindAsync(filter, options))
            {
                while (await cursor.MoveNextAsync())
                {
                    IEnumerable<RPGUsuario> usuarios = cursor.Current;

                    foreach (RPGUsuario user in usuarios)
                    {
                        user.Personagem.DefesaFisicaBase = user.Personagem.DefesaFisica;
                        user.Personagem.DefesaMagicaBase = user.Personagem.DefesaMagica;
                        user.Personagem.AtaqueFisicoBase = user.Personagem.AtaqueFisico;
                        user.Personagem.AtaqueMagicoBase = user.Personagem.AtaqueMagico;
                        user.Salvar();
                    }
                }
            }

            ModuloBanco.ItemColecao.InsertOne(new RPGItem()
            {
                AtaqueFisico = 50,
                Descricao = "Espada de treinamento",
                Id = 50,
                Nome = "Espada de treinamento",
                PrecoCompra = 10,
                PrecoVenda = 2,
                Proficiencia = EnumProficiencia.Ataque,
                ProficienciaNivelRequisito = 5,
                Tipo = EnumTipo.ArmaPrimaria,
            });

            await ctx.RespondAsync("Atualiazado");
        }

        [Command("adicionar-xp")]
        [Aliases("ax")]
        public async Task AdicionarXP(CommandContext ctx, int quantidade = 1, DiscordUser discordUser = null)
        {
            if (discordUser == null)
                discordUser = ctx.User;
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetUsuario(discordUser, out RPGUsuario usuario);
            usuario.Personagem.AdicionarExp(quantidade);
            usuario.Salvar();
            await ctx.RespondAsync($"Adicionado {quantidade}XP para {discordUser.Mention}!");
        }
    }
}
