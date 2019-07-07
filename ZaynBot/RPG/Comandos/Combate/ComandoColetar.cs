using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos.Combate
{
    public class ComandoColetar
    {
        [Command("coletar")]
        [Description("Coleta os loots que estão no chão.\n\n" +
        "Exemplo: z!coletar [quantidade] [nome]\n\n" +
            "Uso: z!coletar 4 espada enferrujada")]
        public async Task ComandoLootAb(CommandContext ctx, int quantidade, [RemainingText] string nome)
        {
            RPGUsuario usuario = await RPGUsuario.GetRPGUsuarioBaseAsync(ctx);
            RPGPersonagem personagem = usuario.Personagem;

            // Se a vida do personagem for igual ou menor que 0, não poderá atacar.
            if (personagem.PontosDeVida <= 0)
            {
                await ctx.RespondAsync($"**{ctx.User.Mention}, você está sem vida.**");
                return;
            }

            personagem.ItensNoChao.TryGetValue(nome, out RPGItem itemColetado);
            if (itemColetado == null)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você não encontrou esse item no chão.");
                return;
            }
            if (itemColetado.Quantidade < quantidade)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, somente tem {itemColetado.Quantidade} no chão para coletar.");
                return;
            }

            float pesoTotal = (itemColetado.Peso * quantidade) + personagem.Inventario.PesoAtual;
            if (pesoTotal > personagem.Inventario.PesoMaximo)
            {
                await ctx.RespondAsync($"{ctx.User.Mention}, você está muito pesado para pegar mais itens.");
                return;
            }
            itemColetado.Quantidade -= quantidade;
            if (itemColetado.Quantidade == 0)
                personagem.ItensNoChao.Remove(itemColetado.Nome);
            personagem.Inventario.Adicionar(itemColetado, quantidade);
            RPGUsuario.UpdateRPGUsuario(usuario);
            await ctx.RespondAsync($"{ctx.User.Mention}, você coletou {quantidade} {nome.PrimeiraLetraMaiuscula()}.");
        }
    }
}
