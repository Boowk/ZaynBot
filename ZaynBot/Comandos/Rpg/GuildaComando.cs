using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ZaynBot.Entidades;
using ZaynBot.Entidades.Rpg;
using ZaynBot.Funções;

namespace ZaynBot.Comandos.Rpg
{
    [Group("guilda")]
    [Description("Comandos da guilda.")]
    public class GuildaComando
    {

        private Usuario _userDep;
        public GuildaComando(Usuario userDep)
        {
            _userDep = userDep;
        }

        [Command("convidar")]
        [Description("Convida um usuario para a sua guilda")]
        public async Task GuildaConvidar(CommandContext ctx, [Description("Membro do servidor")]DiscordMember membro = null)
        {
            await ctx.TriggerTypingAsync();

            if (_userDep.IdGuilda.ToString() == "000000000000000000000000")
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você precisa de uma guilda antes de sair convidando.");
                return;
            };

            if (membro == null || membro.Id == ctx.User.Id)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, quem você está tentando convidar?");
                return;
            }

            if (membro.IsBot)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você não pode convidar outros bots para a sua guilda!! >:(");
                return;
            }

            Guilda guilda = Banco.ConsultarGuilda(_userDep.IdGuilda);
            if (guilda.Convites.Count > 5)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, a sua guilda já alcançou o limite máximo de convites!");
                return;
            }

            Usuario userConvidado = Banco.ConsultarUsuario(membro);
            if (userConvidado.ConvitesGuildas.Count > 5)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, {membro.Mention} tem 5 convites pendentes ainda!");
                return;
            }


            guilda.Convites.Add(new Convite() { IdUsuario = membro.Id });
            Banco.AlterarGuilda(guilda);
            userConvidado.ConvitesGuildas.Add(new Convite() { IdGuilda = guilda.Id });
            Banco.AlterarUsuario(userConvidado);

            await ctx.RespondAsync($"{membro.Mention} foi convidado para a guilda {guilda.Nome} por {ctx.User.Mention}!");
        }

        [Command("criar")]
        [Description("Cria uma guilda")]
        public async Task GuildaCriar(CommandContext ctx, [RemainingText, Description("Nome")]string nome = null)
        {
            await ctx.TriggerTypingAsync();

            if (_userDep.IdGuilda.ToString() != "000000000000000000000000")
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você já tem uma guilda! Saia antes de tentar criar uma.");
                return;
            };

            if (nome == null)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, qual seria o nome da guilda?");
                return;
            }

            if (nome.Length > 25)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, o nome da guilda passou de 25 caracteres!");
                return;
            }

            nome = nome.Replace("<", "").Replace(">", "").Replace("@", "");

            Uri uriResult;
            bool result = Uri.TryCreate(nome, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (result == true)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você não pode colocar links no nome da sua guilda!! >:(");
                return;
            }


            bool criou = Banco.CriarGuilda(new Guilda()
            {
                IdDono = ctx.User.Id,
                Nome = nome
            });

            if (criou == false)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, o nome {nome} já está em uso! Use outro nome.");
                return;
            }
            ObjectId idGuilda = Banco.ConsultarGuildaCriador(ctx.User.Id);
            _userDep.IdGuilda = idGuilda;
            _userDep.ConvitesGuildas.Clear();
            Banco.AlterarUsuario(_userDep);
            await ctx.RespondAsync($"{ctx.User.Mention}, a guilda {nome} foi criada!");
        }
    }
}
