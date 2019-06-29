using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
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
        public int Nivel { get; set; } = 0;
        public double ExperienciaProximoNivel { get; set; } = 100;
        public double ExperienciaAtual { get; set; } = 0;
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

        public static async Task<RPGUsuario> GetRPGUsuarioComPersonagemAsync(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            CancelamentoToken.CancelarToken(ctx.User.Id);
            RPGUsuario usuario = ModuloBanco.GetUsuario(ctx.User.Id);
            if (usuario.Personagem == null)
                throw new PersonagemNullException();
            return usuario;
        }

        public static async Task<RPGUsuario> GetRPGUsuarioAsync(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            return GetRPGUsuario(ctx.User.Id);
        }

        public static RPGUsuario GetRPGUsuario(ulong id, bool cancelarComandoAnterior = true)
        {
            if (cancelarComandoAnterior)
                CancelamentoToken.CancelarToken(id);
            RPGUsuario usuario = ModuloBanco.GetUsuario(id);
            if (usuario == null)
            {
                usuario = new RPGUsuario(id);
                ModuloBanco.UsuarioColecao.InsertOne(usuario);
            }
            return usuario;
        }

        public RPGRegiao GetRPGRegiao()
             => ModuloBanco.GetRPGRegiao(Personagem.LocalAtualId);

        public bool AdicionarExp(int exp)
        {
            double expResultante = ExperienciaAtual + exp;
            if (expResultante >= ExperienciaProximoNivel)
            {
                do
                {
                    double quantosPrecisaProxNivel = expResultante - ExperienciaProximoNivel;
                    Evoluir();
                    expResultante = quantosPrecisaProxNivel;
                } while (expResultante >= ExperienciaProximoNivel);
                ExperienciaAtual += expResultante;
                return true;
            }
            ExperienciaAtual += exp;
            return false;
        }

        public Tuple<bool, int> AdicionarExp()
        {
            Sortear sortear = new Sortear();
            int exp = sortear.Valor(5, 25);
            return new Tuple<bool, int>(AdicionarExp(exp), exp);
        }

        private void Evoluir()
        {
            Nivel += 1;
            ExperienciaAtual = 0;
            ExperienciaProximoNivel += 100;
        }

        public void RegeneraçãoVida()
        {
            float quantidade = Nivel / 25.0F;
            Personagem.PontosDeVida += quantidade;
            if (Personagem.PontosDeVida >= Personagem.PontosDeVidaMaxima) Personagem.PontosDeVida = Personagem.PontosDeVidaMaxima;
        }

        public void RegeneraçãoMana()
        {
            float quantidade = Nivel / 30.0F;
            Personagem.PontosDeMana += quantidade;
            if (Personagem.PontosDeMana >= Personagem.PontosDeManaMaximo) Personagem.PontosDeMana = Personagem.PontosDeManaMaximo;
        }
    }
}
