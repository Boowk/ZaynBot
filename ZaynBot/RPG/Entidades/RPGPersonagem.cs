using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;
using ZaynBot.RPG.Entidades.Enuns;
using ZaynBot.RPG.Habilidades;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGPersonagem : RPGProgresso
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

        public RPGMochila Inventario { get; set; }
        public RPGBatalha Batalha { get; set; }

        [BsonIgnore]
        public double DanoRecebido { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<HabilidadeEnum, RPGHabilidade> Habilidades { get; set; }


        public RPGPersonagem(int nivel = 1, int nivelMax = 0, double expIncremento = 30, double incremento = 1.02)
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

            Inventario = new RPGMochila();
            Batalha = new RPGBatalha();


            //Populando as habilidades
            Habilidades = new Dictionary<HabilidadeEnum, RPGHabilidade>
            {
                { HabilidadeEnum.Perfurante, new HabilidadePerfurante() },
                { HabilidadeEnum.Esmagante, new HabilidadeEsmagante() },
                { HabilidadeEnum.Desarmado, new HabilidadeDesarmado() }
            };
        }


        public new bool AdicionarExp(double exp)
        {
            bool evoluiu = base.AdicionarExp(exp);
            if (evoluiu)
            {
                VidaMaxima = Evoluir(VidaMaxima);
                MagiaMaxima = Evoluir(MagiaMaxima);
                AtaqueFisico = Evoluir(AtaqueFisico);
                AtaqueMagico = Evoluir(AtaqueMagico);
                DefesaFisica = Evoluir(DefesaFisica);
                DefesaMagica = Evoluir(DefesaMagica);
                Velocidade = Evoluir(Velocidade);
                FomeMaxima *= Evoluir(FomeMaxima);
                SedeMaxima *= Evoluir(SedeMaxima);
                PesoMaximo = (AtaqueFisico / 2) + (DefesaFisica / 2);

                return true;
            }
            return false;
        }


        public bool TryGetHabilidade(string hab, out RPGHabilidade habilidade)
        {
            var h = hab.ToLower();
            h = h.PrimeiraLetraMaiuscula();
            if (Enum.IsDefined(typeof(HabilidadeEnum), h))
            {
                habilidade = Habilidades[Enum.Parse<HabilidadeEnum>(h)];
                return true;
            }
            habilidade = null;
            return false;
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
