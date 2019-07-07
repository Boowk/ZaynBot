using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Data.Raças;
using ZaynBot.RPG.Entidades.Mapa;
using ZaynBot.RPG.Exceptions;
using ZaynBot.Utilidades;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGUsuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.Int64, AllowTruncation = true)]
        public ulong Id { get; set; }
        public DateTime DataContaCriada { get; set; } = DateTime.UtcNow;
        public DateTime DataUltimaMensagemEnviada { get; set; } = DateTime.UtcNow;
        public RPGPersonagem Personagem { get; set; }
        public List<int> RacasDisponiveisId { get; set; }

        public RPGUsuario(ulong id)
        {
            Id = id;
            RacasDisponiveisId = new List<int>
            {
                0,
                1,
                2,
                3,
            };
        }

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
