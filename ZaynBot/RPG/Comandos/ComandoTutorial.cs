using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;

namespace ZaynBot.RPG.Comandos
{
    [Group("tutorial")]
    [Description("Tutoriais.")]
    [ComoUsar("tutorial [pagina]")]
    [Exemplo("tutorial 1")]
    public class ComandoTutorial : BaseCommandModule
    {
        [GroupCommand]
        public async Task GroupCommandAsync(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            StringBuilder str = new StringBuilder();
            str.AppendLine("Escreva z!tutorial [pagina] para ver um tutorial.".Bold());
            str.AppendLine("1. Descrição".Bold());
            str.AppendLine("2. Combate".Bold());
            str.AppendLine("3. Itens".Bold());
            str.AppendLine("4. Regiões".Bold());
            str.AppendLine("5. Mensagem automática de boas vindas".Bold());
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithDescription(str.ToString());
            await ctx.RespondAsync(embed: embed.Build());
        }

        [Command("1")]
        public async Task Pagina1(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithDescription("O seu objetivo é evoluir e ser o mais forte de todos! Acumule itens, evolua seus atributos, " +
                "evolua o seu personagem até o apice! A ZaynRPG é um RPG baseado em texto para o Discord separado por regiões, " +
                "onde cada região pode ter inimigos, loja, missões e objetivos. Mate inimigos para conseguir itens, venda itens " +
                "para conseguir moedas, gaste moedas para conseguir itens. Desafie outras pessoas em batalha até a morte por moedas.");
            await ctx.RespondAsync(embed: embed.Build());
        }

        [Command("2")]
        public async Task Pagina2(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithDescription("O combate é separado por turnos, cada turno você pode atacar ou defender. Tudo isso é feito " +
                "automaticamente, você só precisa usar o seu turno com sabedoria. Use `explorar` para encontrar o seu primeiro " +
                "inimigo, como será o seu turno, você pode usar `atacar` logo em seguida. Após atacar sera mostrado uma breve " +
                "descrição do que ocorreu na batalha, preste atenção a sua vida, você não ira querer acabar morrendo..");
            await ctx.RespondAsync(embed: embed.Build());
        }

        [Command("3")]
        public async Task Pagina3(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithDescription("Cada inimigo deixa cair um item. Ele é automaticamente adicionado a sua mochila, que pode ser " +
                "acessada usando `mochila`. Use `examinar` para receber mais detalhes sobre o item. Com o passar das batalhas " +
                "você vai ficando sem vida, para recuperar sua vida, a unica forma é tomando poções recuperadoras. Você precisa " +
                "viajar usando `leste`, `oeste`, `norte` ou `sul` até encontrar uma loja de poções. Compre algumas poções com " +
                "`comprar`, e use-as com `usar`. Caso não saiba qual itens é vendido, use `loja`.");
            await ctx.RespondAsync(embed: embed.Build());
        }

        [Command("5")]
        public async Task Pagina5(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithDescription("A ZaynRPG também envia mensagens de boas vindas! Para configurar é fácil. " +
                "Use `mod bem-vindo` [mensagem]. Onde no lugar da mensagem você colocará a sua menasgem desejada! Lembre-se " +
                "que o bot ira enviar essa mensagem, no local que você usou este comando! Então use este comando em uma area que " +
                "todos possam ver! Também temos outras funções para ajudar na criação da sua mensagem. Use #Menção para mencionar " +
                "a pessoa e #Quantidade para saber quantos membros tem no servidor após aquela pessoa ter entrado! Mais dúvidas? Use " +
                "`ajuda mod bem-vindo`.");
            await ctx.RespondAsync(embed: embed.Build());
        }
        [Command("4")]
        public async Task Pagina4(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            DiscordEmbedBuilder embed = new DiscordEmbedBuilder();
            embed.WithDescription("A ZaynRPG é separado por regiões, cada região pode ter 4 direções para se locomover que são: " +
                "`norte` , `oeste` , `leste` e `sul`. Use `local` para se encontrar na região, recebendo descrições detalhas " +
                "e possíveis direções para ir. Sempre que você explora locais novos, você pode encontrar lojas, e mobs mais fortes " +
                "por isso tome cuidado, e sempre leia as descrições de cada região antes de ir avançando.");
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}
