using DSharpPlus.Entities;
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
        public int RegiaoAtualId { get; set; }
        public RPGMochila Mochila { get; set; }
        public RPGBatalha Batalha { get; set; }
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<ProficienciaEnum, RPGProficiencia> Proficiencias { get; set; }

        public RPGPersonagem(double expMax = 120) : base(expMax)
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
            RegiaoAtualId = 0;
            Mochila = new RPGMochila();
            Batalha = new RPGBatalha();
            //Populate lhas habilitdades
            Proficiencias = new Dictionary<ProficienciaEnum, RPGProficiencia>
            {
                { ProficienciaEnum.Perfurante, new ProficienciaPerfurante() },
                { ProficienciaEnum.Esmagante, new ProficienciaEsmagante() },
                { ProficienciaEnum.Desarmado, new ProficienciaDesarmado() }
            };
        }
        public new bool AdicionarExp(double exp)
        {
            if (base.AdicionarExp(exp))
            {
                VidaMaxima = Evoluir(VidaMaxima);
                MagiaMaxima = Evoluir(MagiaMaxima);
                AtaqueFisico = Evoluir(AtaqueFisico);
                AtaqueMagico = Evoluir(AtaqueMagico);
                DefesaFisica = Evoluir(DefesaFisica);
                DefesaMagica = Evoluir(DefesaMagica);
                Velocidade *= 1.0002;
                FomeMaxima = Evoluir(FomeMaxima);
                SedeMaxima = Evoluir(SedeMaxima);
                return true;
            }
            return false;
        }

        public bool TryGetHabilidade(string hab, out RPGProficiencia habilidade)
        {
            var h = hab.ToLower();
            h = h.FirstUpper();
            if (Enum.IsDefined(typeof(ProficienciaEnum), h))
            {
                habilidade = Proficiencias[Enum.Parse<ProficienciaEnum>(h)];
                return true;
            }
            habilidade = null;
            return false;
        }

        private double SortearMetadeValor(double valor)
            => Sortear.Valor(valor / 2, valor);
    }
}
