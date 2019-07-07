﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using ZaynBot.Data.Habilidades.Passivas;
using ZaynBot.Utilidades;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGPersonagem
    {
        #region Atributos

        public RPGRaça Raca { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float PontosDeVida { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float PontosDeVidaMaxima { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float PontosDeMana { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float PontosDeManaMaximo { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float AtaqueFisico { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float DefesaFisica { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float AtaqueMagico { get; set; }
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float DefesaMagica { get; set; }
        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public int Velocidade { get; set; }

        #endregion

        //[BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        //public float Fome { get; set; }
        //[BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        //public float Sede { get; set; }

        [BsonRepresentation(BsonType.Int32, AllowTruncation = true)]
        public int LocalAtualId { get; set; }
        [BsonRepresentation(BsonType.Document, AllowTruncation = true)]
        public Dictionary<string, RPGItem> ItensNoChao { get; set; } = new Dictionary<string, RPGItem>();
        public List<int> MissoesConcluidasId { get; set; } = new List<int>();
        public RPGMissao MissaoEmAndamento { get; set; }

        public float PontosDeAcao { get; set; }

        public RPGEquipamento Equipamento { get; set; } = new RPGEquipamento();
        public RPGInventario Inventario { get; set; }
        public Dictionary<string, RPGHabilidade> Habilidades { get; set; } = new Dictionary<string, RPGHabilidade>();
        public RPGTitulo Titulo { get; set; } = new RPGTitulo();
        public RPGEmprego Emprego { get; set; }

        public bool Vivo { get; set; }
        public DateTime DataMorte { get; set; }

        public RPGBatalha Batalha { get; set; } = new RPGBatalha();

        public RPGPersonagem(RPGRaça raca)
        {
            Inventario = new RPGInventario(raca);
            Raca = raca;
            Sortear sortear = new Sortear();
            int sorte = sortear.Valor(Raca.Sorte, Raca.Sorte);
            PontosDeVida = sortear.Valor(raca.Constituicao * 1 + sorte, raca.Constituicao * 10 + sorte);
            sorte = sortear.Valor(Raca.Sorte, Raca.Sorte);
            PontosDeMana = sortear.Valor(Raca.Inteligencia * 1 + sorte, raca.Inteligencia * 10 + sorte);
            PontosDeManaMaximo = PontosDeMana;
            PontosDeVidaMaxima = PontosDeVida;
            sorte = sortear.Valor(Raca.Sorte, Raca.Sorte);
            AtaqueFisico = sortear.Valor(Raca.Forca * 1 + sorte, Raca.Forca * 10 + sorte);
            sorte = sortear.Valor(Raca.Sorte, Raca.Sorte);
            DefesaFisica = sortear.Valor(Raca.Constituicao * 1 + sorte, Raca.Constituicao * 10 + sorte);
            sorte = sortear.Valor(Raca.Sorte, Raca.Sorte);
            AtaqueMagico = sortear.Valor(Raca.Inteligencia * 1 + sorte, Raca.Inteligencia * 10 + sorte);
            sorte = sortear.Valor(Raca.Sorte, Raca.Sorte);
            DefesaMagica = sortear.Valor(Raca.Inteligencia * 1 + sorte, Raca.Inteligencia * 10 + sorte);
            sorte = sortear.Valor(Raca.Sorte, Raca.Sorte);
            Velocidade = sortear.Valor(Raca.Destreza * 1 + sorte, Raca.Destreza * 10 + sorte);
            Emprego = new RPGEmprego("Desempregado");
            LocalAtualId = 0;
            Vivo = true;

            Habilidades.Add(CuraMensagemHabilidade.CuraMensagemHabilidadeAb().Nome, CuraMensagemHabilidade.CuraMensagemHabilidadeAb());
        }

        //public int Alimentar(int quantidade)
        //{
        //    Fome += quantidade;
        //    if (Fome > 100) Fome = 100;
        //    return (int)Fome;
        //}

        //public int Beber(int quantidade)
        //{
        //    Sede += quantidade;
        //    if (Sede > 100) Sede = 100;
        //    return (int)Sede;
        //}
    }
}
