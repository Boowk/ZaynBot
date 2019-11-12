using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;
using ZaynBot.RPG.Entidades.Enuns;
using ZaynBot.RPG.Habilidades;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class PersonagemRPG : ProgressoRPG
    {
        public double VidaAtual { get; set; }
        public double VidaMaxima { get; set; }


        public double MagiaAtual { get; set; }
        public double MagiaMaxima { get; set; }


        public double AtaqueFisico { get; set; }
        public double AtaqueMagico { get; set; }


        public double DefesaFisica { get; set; }
        public double DefesaMagica { get; set; }


        public double Velocidade { get; set; }
        public double Sorte { get; set; }


        public double FomeAtual { get; set; }
        public double FomeMaxima { get; set; }


        public double SedeAtual { get; set; }
        public double SedeMaxima { get; set; }


        public double EstaminaAtual { get; set; }
        public double EstaminaMaxima { get; set; }

        public double PesoAtual { get; set; }
        public double PesoMaximo { get; set; }


        public int RegiaoAtualId { get; set; }
        public int RegiaoCasaId { get; set; }
        public bool CasaConstruida { get; set; }

        public InventarioRPG Inventario { get; set; }
        public BatalhaRPG Batalha { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<HabilidadeEnum, HabilidadeRPG> Habilidades { get; set; }


        public PersonagemRPG(int nivel = 1, int nivelMax = 0, double expIncremento = 30, double incremento = 1.02)
            : base(nivel, nivelMax, expIncremento, incremento)
        {
            VidaAtual = SortearMetadeValor(50);
            VidaMaxima = VidaAtual;

            MagiaAtual = SortearMetadeValor(50);
            MagiaMaxima = MagiaAtual;

            AtaqueFisico = SortearMetadeValor(50);
            AtaqueMagico = SortearMetadeValor(50);

            DefesaFisica = SortearMetadeValor(50);
            DefesaMagica = SortearMetadeValor(50);

            Velocidade = SortearMetadeValor(25);
            Sorte = 0;

            FomeAtual = 100;
            FomeMaxima = 100;

            SedeAtual = 100;
            SedeMaxima = 100;

            EstaminaAtual = 100;
            EstaminaMaxima = 100;

            PesoAtual = 0;
            PesoMaximo = (AtaqueFisico / 2) + (DefesaFisica / 2);

            RegiaoAtualId = 0;
            RegiaoCasaId = 0;
            CasaConstruida = false;

            Inventario = new InventarioRPG();
            Batalha = new BatalhaRPG();


            //Populando as habilidades
            Habilidades = new Dictionary<HabilidadeEnum, HabilidadeRPG>
            {
                { HabilidadeEnum.Perfurante, new PerfuranteHabilidade() },
                { HabilidadeEnum.Esmagante, new EsmaganteHabilidade() },
                { HabilidadeEnum.Desarmado, new DesarmadoHabilidade() }
            };
        }

        public bool TryGetHabilidade(int idHabilidade, out HabilidadeRPG habilidade)
        {
            HabilidadeEnum habilidadeEnum = HabilidadeEnum.Nenhum;
            if (Enum.IsDefined(typeof(HabilidadeEnum), idHabilidade))
                habilidadeEnum = (HabilidadeEnum)idHabilidade;
            if (habilidadeEnum == HabilidadeEnum.Nenhum)
            {
                habilidade = null;
                return false;
            }
            else
            {
                habilidade = Habilidades[habilidadeEnum];
                return true;
            }
        }

        public double RecuperarVida(double quantidade)
        {
            if (quantidade + VidaAtual > VidaMaxima)
            {
                double v = VidaMaxima - VidaAtual;
                VidaAtual = VidaMaxima;
                return v;
            }
            VidaAtual += quantidade;
            return quantidade;
        }

        public double RecuperarMagia(double quantidade)
        {
            if (quantidade + MagiaAtual > MagiaMaxima)
            {
                double v = MagiaMaxima - MagiaAtual;
                MagiaAtual = MagiaMaxima;
                return v;
            }
            MagiaAtual += quantidade;
            return quantidade;
        }

        private double SortearMetadeValor(double valor)
            => Sortear.Valor(valor / 2, valor);
    }
}
