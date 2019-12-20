﻿using System;
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

        public double RecuperarVida(double quantidade)
        {
            if (quantidade + Personagem.VidaAtual > Personagem.VidaMaxima)
            {
                double v = Personagem.VidaMaxima - Personagem.VidaAtual;
                Personagem.VidaAtual = Personagem.VidaMaxima;
                return v;
            }
            Personagem.VidaAtual += quantidade;
            return quantidade;
        }
        public double RecuperarMagia(double quantidade)
        {
            if (quantidade + Personagem.MagiaAtual > Personagem.MagiaMaxima)
            {
                double v = Personagem.MagiaMaxima - Personagem.MagiaAtual;
                Personagem.MagiaAtual = Personagem.MagiaMaxima;
                return v;
            }
            Personagem.MagiaAtual += quantidade;
            return quantidade;
        }
        public void RemoverVida(double quantidade)
        {
            Personagem.VidaAtual -= quantidade;
            if (Personagem.VidaAtual <= 0)
            {
                Personagem.VidaAtual = Personagem.VidaMaxima / 3;
                Salvar(this);
                throw new PersonagemNoLifeException();
            }
        }

        public static void Salvar(RPGUsuario usuario)
            => ModuloBanco.UsuarioEdit(usuario);

        public void Salvar()
            => ModuloBanco.UsuarioEdit(this);
    }
}
