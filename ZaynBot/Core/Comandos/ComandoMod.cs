using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.Core.Entidades;

namespace ZaynBot.Core.Comandos
{
    [Group("mod")]
    [Description("Comandos administrativos do servidor.")]
    [ComoUsar("mod [comando]")]
    class ComandoMod : BaseCommandModule
    {
        [GroupCommand]
        public async Task ComandoModAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.ExecutarComandoAsync("ajuda mod");
        }

        [Command("bem-vindo")]
        [Description("Permite a adição de mensagem de boas vindas sempre que alguém entrar no servidor. Este comando deve ser " +
            "usado no canal onde se deseja que seja enviado as mensagens.\n\n" +
            "#Menção => Menciona a pessoa\n" +
            "#Quantidade => Mostra a quantidade de pessoas do servidor")]
        [ComoUsar("mod bem-vindo [mensagem]")]
        [Exemplo("mod bem-vindo Bem-vindo #Menção agora temos #Quantidade no servidor.")]
        [RequireUserPermissions(DSharpPlus.Permissions.ManageMessages)]
        public async Task Mp(CommandContext ctx, [RemainingText] string texto = "")
        {
            await ctx.TriggerTypingAsync();
            if (string.IsNullOrEmpty(texto))
            {
                await ctx.ExecutarComandoAsync("ajuda mod bem-vindo");
                return;
            }
            if (texto.Length > 1350)
            {
                await ctx.RespondAsync($"{ctx.User.Mention} eu sei que você gostaria de fazer uma recepção calorosa, mas não " +
                    $"consigo processar um bem-vindo tão grande assim! Diminua um pouco o seu bem-vindo!");
                return;
            }
            StringBuilder str = new StringBuilder(texto);
            ServidorCore ser = ModuloBanco.GetServidor(ctx.Guild.Id);
            ser.BemVindoMensagem = texto;
            ser.BemVindoCanalId = ctx.Channel.Id;
            ser.Salvar();
            str.Replace("#Menção", $"{ctx.User.Mention}");
            str.Replace("#Quantidade", $"{ctx.Guild.MemberCount}");
            await ctx.RespondAsync($"{ctx.User.Mention} mensagem de boas vindas será exibida da seguinte forma: {str.ToString()}");
        }
    }
}
