using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZaynBot.Core.Atributos;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.RPG.Comandos
{
    public class ComandoBatalha : BaseCommandModule
    {
        [Command("batalha")]
        [Description("Exibe o status da batalha atual.")]
        [UsoAtributo("batalha")]
        [Cooldown(1, 10, CooldownBucketType.User)]
        public async Task BatalhaComandoAb(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            RPGUsuario.GetPersonagem(ctx, out RPGUsuario usuario);
            RPGPersonagem personagem = usuario.Personagem;
            RPGBatalha batalha = new RPGBatalha();

            //Caso não tenha grupo
            if (personagem.Batalha.LiderGrupo == 0)
            {
                await ctx.RespondAsync($"Você deve criar um Grupo antes! {ctx.User.Mention}.");
                return;
            }

            //Caso o lider do grupo não seja ele
            if (personagem.Batalha.LiderGrupo != ctx.User.Id)
            {
                RPGUsuario liderUsuario = await RPGUsuario.UsuarioGetAsync(personagem.Batalha.LiderGrupo);
                batalha = liderUsuario.Personagem.Batalha;
            }

            //Caso ele seja o lider
            if (personagem.Batalha.LiderGrupo == ctx.User.Id)
                batalha = personagem.Batalha;

            if(batalha.Mobs.Count == 0)
            {
                await ctx.RespondAsync($"Você não tem nenhuma batalha em andamento, {ctx.User.Mention}!");
                return;
            }


            DiscordEmbedBuilder embed = new DiscordEmbedBuilder().Padrao("Batalha", ctx);
            embed.WithColor(DiscordColor.PhthaloGreen);
            embed.WithTitle($"**{batalha.NomeGrupo}**".Titulo());
            embed.WithDescription($"**Turno**: {batalha.Turno.ToString()}\n");

            //Caso tenha mobs
            if (batalha.Mobs.Count != 0)
            {
                StringBuilder sr = new StringBuilder();
                foreach (var item in batalha.Mobs)
                    sr.AppendLine($"{item.Key.PrimeiraLetraMaiuscula()} - Vida: {item.Value.PontosDeVida.Texto2Casas()}" +
                        $"");
                embed.AddField("**Mobs**".Titulo(), sr.ToString(), true);
            }

            ////Caso tenha jogadores inimigos
            //if (personagem.Batalha.LiderPartyInimiga != 0)
            //{

            //    return;
            //}

            await ctx.RespondAsync(embed: embed.Build());
        }

        public string CalcularVez(double estaminaAtual, double estaminaMaxima)
        {
            if (estaminaAtual >= estaminaMaxima)
                return "Pronto";
            if (estaminaAtual != 0)
                return "Se preparando";
            return "Já atacou";
        }
    }
}
