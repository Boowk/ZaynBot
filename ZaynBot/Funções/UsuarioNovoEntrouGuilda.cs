using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System.Threading.Tasks;

namespace ZaynBot.Funções
{
   public static class UsuarioNovoEntrouGuilda
    {
        public static async Task EventoBemVindoAsync(GuildMemberAddEventArgs e)
        {
            if (e.Guild.Id == 508615273515843584)
            {
                DiscordChannel f = e.Guild.GetChannel(551469878268395530);
                await f.SendMessageAsync($"Bem-vindo {e.Member.Mention} ao anime'sworld, leia as regras antes de qualquer coisa.");
            }
            if (e.Guild.Id == 420044060720627712)
            {
                DiscordChannel f = e.Guild.GetChannel(423347465912320000);
                await f.SendMessageAsync($"Bem-vindo {e.Member.Mention} ao Dragon and Zayn's RPG!");
            }
        }
    }
}
