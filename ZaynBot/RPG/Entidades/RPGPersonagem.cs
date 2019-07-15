using MongoDB.Bson;
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
        public RPGRaça Raca { get; set; }

        #region Atributos

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
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float Velocidade { get; set; }

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
        [BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        public float ExperienciaArmazenada { get; set; } = 0;

        public RPGEquipamento Equipamento { get; set; } = new RPGEquipamento();
        public RPGInventario Inventario { get; set; }
        public Dictionary<string, RPGHabilidade> Habilidades { get; set; } = new Dictionary<string, RPGHabilidade>();
        public RPGTitulo Titulo { get; set; } = new RPGTitulo();
        public RPGEmprego Emprego { get; set; }

        public DateTime DataMorte { get; set; }

        public RPGBatalha Batalha { get; set; } = new RPGBatalha();

        public RPGPersonagem(RPGRaça raca)
        {
            Inventario = new RPGInventario(raca.Forca, raca.Destreza);
            Raca = raca;
            PontosDeVida = Sortear.Atributo(raca.Constituicao, raca.Sorte);
            PontosDeVidaMaxima = PontosDeVida;
            PontosDeMana = Sortear.Atributo(Raca.Inteligencia, raca.Sorte);
            PontosDeManaMaximo = PontosDeMana;

            AtaqueFisico = Sortear.Atributo(Raca.Forca, raca.Sorte);
            DefesaFisica = Sortear.Atributo(Raca.Constituicao, raca.Sorte);
            AtaqueMagico = Sortear.Atributo(Raca.Inteligencia, raca.Sorte);
            DefesaMagica = Sortear.Atributo(Raca.Inteligencia, raca.Sorte);
            Velocidade = Sortear.Atributo(Raca.Destreza, raca.Sorte);
            Emprego = new RPGEmprego("Desempregado");
            LocalAtualId = 0;

            AdicionarHabilidade(CuraMensagemHabilidade.CuraMensagemHabilidadeAb());
        }

        public void AdicionarHabilidade(RPGHabilidade habilidade)
            => Habilidades.Add(habilidade.Nome, habilidade);

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
