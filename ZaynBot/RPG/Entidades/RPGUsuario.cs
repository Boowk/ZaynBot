using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using ZaynBot.RPG.Entidades.Mapa;
using ZaynBot.RPG.Exceptions;

namespace ZaynBot.RPG.Entidades
{
    public class RPGUsuario
    {
        public ulong Id { get; set; }
        public DateTime DataContaCriada { get; set; } = DateTime.UtcNow;
        public DateTime DataUltimaMensagemEnviada { get; set; } = DateTime.UtcNow;
        public int Estrelas { get; set; } = 0;
        public RPGPersonagem Personagem { get; set; }

        public RPGUsuario(ulong id)
            => Id = id;

        /// <summary>
        /// Inserido sempre no começo dos comandos em que envolve personagem.
        /// </summary>
        /// <param name="ctx"></param>
        /// <returns></returns>
        public static async Task<RPGUsuario> GetRPGUsuarioBaseAsync(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            CancelamentoToken.CancelarToken(ctx.User.Id);
            RPGUsuario usuario = ModuloBanco.GetUsuario(ctx.User.Id);
            if (usuario == null)
                throw new PersonagemNullException();
            return usuario;
        }

        /// <summary>
        /// Usado sempre que for referenciar a outro usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static RPGUsuario GetRPGUsuario(DiscordUser discordUsuario)
        {
            RPGUsuario usuario = ModuloBanco.GetUsuario(discordUsuario.Id);
            if (usuario == null)
                throw new PersonagemNullException($"{discordUsuario.Username}#{discordUsuario.Discriminator} não tem um personagem.");
            return usuario;
        }

        public RPGRegiao GetRPGRegiao()
             => ModuloBanco.GetRPGRegiao(Personagem.LocalAtualId);

        public static void UpdateRPGUsuario(RPGUsuario usuario)
            => ModuloBanco.UpdateUsuario(usuario);
    }
}
