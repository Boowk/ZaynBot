using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
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
            ModuloBanco.ColecaoJogador.DeleteOne(x => x.Id == user.Id);
            await ctx.RespondAsync("Deletado!");
        }

        //[Command("dar-item")]
        //[Exemplo("dar-item [quantidade] [id] [|usuario]")]
        //public async Task AdicionarItem(CommandContext ctx, int quantidade = 1, int id = 0, DiscordUser discordUser = null)
        //{
        //    if (discordUser == null)
        //        discordUser = ctx.User;
        //    await ctx.TriggerTypingAsync();
        //    RPGItem item = ModuloBanco.GetItem(id);
        //    if (item == null)
        //    {
        //        await ctx.RespondAsync("Item não encontrado!");
        //        return;
        //    }
        //    var usario .GetUsuario(discordUser, out RPGUsuario usuario);
        //    usuario.Personagem.Mochila.AdicionarItem(item, quantidade);
        //    usuario.Salvar();
        //    await ctx.RespondAsync($"Adicionado {quantidade} [{item.Nome}] para {discordUser.Mention}!");
        //}

        //[Command("remover-item")]
        //[Exemplo("remover-item [quantidade] [usuario] [item]")]
        //public async Task Remover(CommandContext ctx, int quantidade = 1, DiscordUser discordUser = null, [RemainingText] string itemNome = "")
        //{
        //    if (discordUser == null)
        //        discordUser = ctx.User;
        //    await ctx.TriggerTypingAsync();
        //    RPGUsuario.GetUsuario(discordUser, out RPGUsuario usuario);
        //    try
        //    {
        //        usuario.Personagem.Mochila.RemoverItem(itemNome, quantidade);
        //        usuario.Salvar();
        //        await ctx.RespondAsync("Item removido!");
        //        return;
        //    }
        //    catch
        //    {
        //        await ctx.RespondAsync("Item não encontrado!");
        //        return;
        //    }
        //}

        [Command("atualizar")]
        public async Task Atualizar(CommandContext ctx)
        {

            using (var fileStream = File.OpenRead(Path.GetFullPath(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"../../../../" + "usuario.json"))))
            using (var streamReader = new StreamReader(fileStream, Encoding.UTF8, true))
            {
                String line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    if (ulong.TryParse(line, out ulong id))
                    {
                        var jogador = ModuloBanco.GetJogador(id);
                        jogador.AdicionarExp(4569);
                        jogador.Salvar();
                    }
                }
            }
            await ctx.RespondAsync("Atualiazado");
            return;
            FilterDefinition<RPGJogador> filter = FilterDefinition<RPGJogador>.Empty;
            FindOptions<RPGJogador> options = new FindOptions<RPGJogador>
            {
                BatchSize = 8,
                NoCursorTimeout = false
            };

            using (IAsyncCursor<RPGJogador> cursor = await ModuloBanco.ColecaoJogador.FindAsync(filter, options))
            {
                while (await cursor.MoveNextAsync())
                {
                    IEnumerable<RPGJogador> usuarios = cursor.Current;

                    foreach (RPGJogador user in usuarios)
                    {
                        //user.RegiaoAtualId = "Regiao de teste";
                        user.Salvar();
                    }
                }
            }
            await ctx.RespondAsync("Atualiazado");

        }

        [Command("dar-xp")]
        public async Task AdicionarXP(CommandContext ctx, int quantidade = 1, DiscordUser discordUser = null)
        {
            if (discordUser == null)
                discordUser = ctx.User;
            await ctx.TriggerTypingAsync();
            var jogador = ModuloBanco.GetJogador(discordUser);
            jogador.AdicionarExp(quantidade);
            jogador.Salvar();
            await ctx.RespondAsync($"Adicionado {quantidade}XP para {discordUser.Mention}!");
        }

        //[Command("money")]
        //public async Task money(CommandContext ctx)
        //{
        //    await ctx.TriggerTypingAsync();
        //    RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
        //    usuario.Personagem.Mochila.AdicionarItem("moeda de zeoin", new RPGMochilaItemData()
        //    {
        //        Id = 0,
        //        Quantidade = 9999999,
        //    });
        //    usuario.Salvar();
        //    await ctx.RespondAsync("Adicionado!");
        //}

        //[Command("tp")]
        //public async Task Teleportar(CommandContext ctx, int id = 1)
        //{
        //    await ctx.TriggerTypingAsync();
        //    RPGUsuario.GetUsuario(ctx, out RPGUsuario usuario);
        //    //usuario.Personagem.RegiaoAtualId = id;
        //    usuario.Salvar();
        //    await ctx.RespondAsync($"Teleportado para id {id}!");
        //}
    }
}
