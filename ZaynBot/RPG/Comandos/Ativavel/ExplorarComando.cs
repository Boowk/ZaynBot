//using DSharpPlus.CommandsNext;
//using DSharpPlus.CommandsNext.Attributes;
//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using ZaynBot.Core.Atributos;
//using ZaynBot.RPG.Entidades;
//using ZaynBot.RPG.Entidades.Mapa;
//using ZaynBot.Utilidades;

//namespace ZaynBot.RPG.Comandos.Ativavel
//{
//    public class ExplorarComando : BaseCommandModule
//    {
//        [Command("explorar")]
//        [Aliases("ex")]
//        [Cooldown(1, 2, CooldownBucketType.User)]
//        [Description("Procura por inimigos na região atual.")]
//        [UsoAtributo("explorar")]
//        [Cooldown(1, 10, CooldownBucketType.User)]
//        public async Task ExplorarComandoAb(CommandContext ctx)
//        {
//            await ctx.TriggerTypingAsync();
//            UsuarioRPG.GetPersonagem(ctx, out UsuarioRPG usuario);
//            PersonagemRPG personagem = usuario.Personagem;
//            RegiaoRPG localAtual = usuario.RegiaoGet();

//            if (localAtual.Mobs.Count == 0)
//            {
//                await ctx.RespondAsync($"Você olha em volta, mas não encontra nenhum inimigo! {ctx.User.Mention}");
//                return;
//            }

//            if (personagem.Batalha.Inimigos.Count == 0)
//            {
//               MobRPG mobSorteado =  MobProcurar(localAtual, personagem, ctx);

//                await ctx.RespondAsync($"**<{mobSorteado.Nome}>** apareceu! {ctx.User.Mention}.");
//                UsuarioRPG.Salvar(usuario);
//            }
//            else
//                await ctx.RespondAsync($"Você já está batalhando! {ctx.User.Mention}.");
//        }

//        public static MobRPG MobProcurar(RegiaoRPG localAtual, PersonagemRPG personagem, CommandContext ctx)
//        {
//            MobRPG mobSorteado = Sortear.Mobs(localAtual.Mobs);
//            personagem.Batalha.Inimigos.Add(mobSorteado);

//            personagem.Batalha.Turno = 0;
//            int velocidadeInimigos = 0;
//            foreach (var inimigo in personagem.Batalha.Inimigos)
//                velocidadeInimigos += inimigo.Velocidade;
//            personagem.Batalha.PontosDeAcaoTotal = personagem.Raca.Agilidade + velocidadeInimigos;
//            return mobSorteado;
//        }
//    }
//}
