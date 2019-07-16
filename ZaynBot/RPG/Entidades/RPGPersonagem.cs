using System;
using System.Collections.Generic;
using ZaynBot.Data.Habilidades.Passivas;
using ZaynBot.Utilidades;

namespace ZaynBot.RPG.Entidades
{
    public class RPGPersonagem
    {
        public RPGRaça Raca { get; set; }

        #region Atributos

        public float PontosDeVida { get; set; }
        public float PontosDeVidaMaxima { get; set; }
        public float PontosDeMana { get; set; }
        public float PontosDeManaMaximo { get; set; }
        public float AtaqueFisico { get; set; }
        public float DefesaFisica { get; set; }
        public float AtaqueMagico { get; set; }
        public float DefesaMagica { get; set; }
        public float Velocidade { get; set; }

        #endregion

        //[BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        //public float Fome { get; set; }
        //[BsonRepresentation(BsonType.Double, AllowTruncation = true)]
        //public float Sede { get; set; }

        public int LocalAtualId { get; set; }
        public Dictionary<string, RPGItem> ItensNoChao { get; set; } = new Dictionary<string, RPGItem>();
        public List<int> MissoesConcluidasId { get; set; } = new List<int>();
        public RPGMissao MissaoEmAndamento { get; set; }
        public float Essencia { get; set; } = 0;

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
            PontosDeVida = Sortear.Atributo(Raca.Constituicao, Raca.Sorte);
            PontosDeVidaMaxima = PontosDeVida;
            PontosDeMana = Sortear.Atributo(Raca.Inteligencia, Raca.Sorte);
            PontosDeManaMaximo = PontosDeMana;

            AtaqueFisico = Sortear.Atributo(Raca.Forca, Raca.Sorte);
            DefesaFisica = Sortear.Atributo(Raca.Constituicao, Raca.Sorte);
            AtaqueMagico = Sortear.Atributo(Raca.Inteligencia, Raca.Sorte);
            DefesaMagica = Sortear.Atributo(Raca.Inteligencia, Raca.Sorte);
            Velocidade = Sortear.Atributo(Raca.Destreza, Raca.Sorte);
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
