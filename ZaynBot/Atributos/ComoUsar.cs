using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Threading.Tasks;

namespace ZaynBot.Atributos
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    class ComoUsar : CheckBaseAttribute
    {
        public string Uso { get; }

        public ComoUsar(string uso) => Uso = uso;

        public override async Task<bool> ExecuteCheckAsync(CommandContext ctx, bool help)
        {
            if (help) return true;
            var bot = await ctx.Guild.GetMemberAsync(ctx.Client.CurrentUser.Id);
            if (bot == null) return false;
            var pbot = ctx.Channel.PermissionsFor(bot);

            var botok = (pbot & Permissions.Administrator) != 0 || (pbot & Permissions.EmbedLinks) == Permissions.EmbedLinks;
            if (!botok)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, é necessario que a permissão de **Inserir links** esteja ativa para que o bot funcione corretamente.");
                throw new AggregateException();
            }
            return botok;
        }
    }
}
