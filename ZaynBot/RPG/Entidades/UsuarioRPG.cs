﻿using System;
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
        public DateTime DataContaCriada { get; set; }
        public int Estrelas { get; set; }
        public PersonagemRPG Personagem { get; set; }

        public UsuarioRPG(ulong id)
        {
            Id = id;
            DataContaCriada = DateTime.Now;
            Estrelas = 0;
            Personagem = new PersonagemRPG();
        }

        public static void TryGetPersonagemRPG(CommandContext ctx, out UsuarioRPG usuario, MensagemAvisoEnum mensagem = MensagemAvisoEnum.Todos)
        {
            usuario = ModuloBanco.UsuarioGet(ctx.User.Id);
            if (mensagem.HasFlag(MensagemAvisoEnum.PersonagemNull))
                if (usuario == null)
                    throw new PersonagemNullException();
            if (mensagem.HasFlag(MensagemAvisoEnum.SemVida))
                if (usuario.Personagem.VidaAtual <= 0)
                    throw new PersonagemNoLifeException();
        }

        public static UsuarioRPG UsuarioGet(DiscordUser discordUsuario)
        {
            UsuarioRPG usuario = ModuloBanco.UsuarioGet(discordUsuario.Id);
            if (usuario == null)
                throw new PersonagemNullException($"{discordUsuario.Username}#{discordUsuario.Discriminator} não tem um personagem.");
            return usuario;
        }

        public RegiaoRPG RegiaoGet()
             => ModuloBanco.RegiaoGet(Personagem.LocalAtualId);

        public static void Salvar(UsuarioRPG usuario)
            => ModuloBanco.UsuarioEdit(usuario);

        public void Salvar()
            => ModuloBanco.UsuarioEdit(this);
    }
}
