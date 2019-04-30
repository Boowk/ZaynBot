using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Entidades;
using ZaynBot.Funções;

namespace ZaynBot.Comandos.Diversão
{
    public class DarRespeito
    {
        private Usuario _userDep;
        public DarRespeito(Usuario userDep)
        {
            _userDep = userDep;
        }

        [Command("respeitar")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task Respeitar(CommandContext ctx, DiscordMember membro)
        {
            if (DateTime.UtcNow >= _userDep.DataRespeitosReset)
            {
                _userDep.DataRespeitosReset = DateTime.UtcNow.AddDays(1);
                _userDep.RespeitosDisponiveis = 3;
                Banco.AlterarUsuario(_userDep);
            }
            if (membro.IsBot == true)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você não pode respeitar um bot!! >:(");
                return;
            }
            if(membro == ctx.User)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você não se auto respeitar !! >:( MENINO MAU!");
                return;
            }
            if (_userDep.RespeitosDisponiveis != 0)
            {

                Usuario userRecebe = Banco.ConsultarUsuario(membro);
                userRecebe.Respeitos++;
                _userDep.RespeitosDisponiveis--;
                Banco.AlterarUsuario(userRecebe);
                Banco.AlterarUsuario(_userDep);
                await ctx.RespondAsync($"{membro.Mention} recebeu respeito de {ctx.User.Mention}! :+1:");
                return;
            }
            await ctx.RespondAsync($"{ctx.User.Mention}, você não tem mais respeitos para dar!");

        }
    }
}
