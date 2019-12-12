using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Exceptions;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGUsuario
    {
        [BsonId]
        public ulong Id { get; set; }
        public DateTime CriacaoUsuarioData { get; set; }
        public RPGPersonagem Personagem { get; set; }

        #region Usado em outra areas
        [BsonIgnore]
        public DiscordUser DiscordUser { get; set; }
        #endregion

        public RPGUsuario(ulong id)
        {
            Id = id;
            CriacaoUsuarioData = DateTime.Now;
            Personagem = new RPGPersonagem();
        }

        public static void GetUsuario(CommandContext ctx, out RPGUsuario usuario)
        {
            usuario = ModuloBanco.GetUsuario(ctx.User.Id);
            if (usuario == null)
                throw new PersonagemNullException();
        }

        public static void UsuarioGet(DiscordUser discordUsuario, out RPGUsuario usuario)
        {
            usuario = ModuloBanco.GetUsuario(discordUsuario.Id);
            if (usuario == null)
                throw new PersonagemNullException($"{discordUsuario.Mention} não tem um personagem para ser convidado!");
        }
        public static async Task<RPGUsuario> UsuarioGetAsync(ulong discordUserId)
        {
            RPGUsuario usuario = ModuloBanco.GetUsuario(discordUserId);
            if (usuario == null)
            {
                DiscordUser du = await ModuloCliente.Client.GetUserAsync(discordUserId);
                throw new PersonagemNullException($"{du.Mention} não tem um personagem para ser convidado!");
            }
            return usuario;
        }

        public RPGRegiao RegiaoGet()
             => ModuloBanco.RegiaoGet(Personagem.RegiaoAtualId);

        public static void Salvar(RPGUsuario usuario)
            => ModuloBanco.UsuarioEdit(usuario);

        public void Salvar()
            => ModuloBanco.UsuarioEdit(this);
    }
}
