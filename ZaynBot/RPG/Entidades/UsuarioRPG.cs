using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using MongoDB.Bson.Serialization.Attributes;
using ZaynBot.RPG.Entidades.Enuns;
using ZaynBot.RPG.Entidades.Mapa;
using ZaynBot.RPG.Exceptions;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class UsuarioRPG
    {
        [BsonId]
        public ulong Id { get; set; }
        public DateTime CriacaoUsuarioData { get; set; }
        public PersonagemRPG Personagem { get; set; }

        public UsuarioRPG(ulong id)
        {
            Id = id;
            CriacaoUsuarioData = DateTime.Now;
            Personagem = new PersonagemRPG();
        }

        public static void GetPersonagem(CommandContext ctx, out UsuarioRPG usuario)
        {
            usuario = ModuloBanco.UsuarioGet(ctx.User.Id);
            if (usuario == null)
                throw new PersonagemNullException();
        }

        public static UsuarioRPG UsuarioGet(DiscordUser discordUsuario)
        {
            UsuarioRPG usuario = ModuloBanco.UsuarioGet(discordUsuario.Id);
            if (usuario == null)
                throw new PersonagemNullException($"{discordUsuario.Username}#{discordUsuario.Discriminator} não tem um personagem.");
            return usuario;
        }
        public static async Task<UsuarioRPG> UsuarioGetAsync(ulong discordUserId)
        {
            UsuarioRPG usuario = ModuloBanco.UsuarioGet(discordUserId);
            if (usuario == null)
            {
                DiscordUser du = await ModuloCliente.Client.GetUserAsync(discordUserId);
                throw new PersonagemNullException($"{du.Username}#{du.Discriminator} não tem um personagem.");
            }
            return usuario;
        }

        public RegiaoRPG RegiaoGet()
             => ModuloBanco.RegiaoGet(Personagem.RegiaoAtualId);

        public static void Salvar(UsuarioRPG usuario)
            => ModuloBanco.UsuarioEdit(usuario);

        public void Salvar()
            => ModuloBanco.UsuarioEdit(this);
    }
}
