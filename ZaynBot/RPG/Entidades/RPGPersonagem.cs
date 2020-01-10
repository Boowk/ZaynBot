using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGPersonagem : RPGProgresso
    {
        public double VidaAtual { get; set; }
        public double VidaMaxima { get; set; }

        public double MagiaAtual { get; set; }
        public double MagiaMaxima { get; set; }

        public double AtaqueFisicoBase { get; set; }
        public double AtaqueFisicoExtra { get; set; }

        public double AtaqueMagicoBase { get; set; }
        public double AtaqueMagicoExtra { get; set; }

        public double DefesaFisicaBase { get; set; }
        public double DefesaFisicaExtra { get; set; }

        public double DefesaMagicaBase { get; set; }
        public double DefesaMagicaExtra { get; set; }

        public double FomeAtual { get; set; }
        public double FomeMaxima { get; set; }

        public double SedeAtual { get; set; }
        public double SedeMaxima { get; set; }

        public double EstaminaAtual { get; set; }
        public double EstaminaMaxima { get; set; }

        public int RegiaoAtualId { get; set; }
        public int ProficienciaPontos { get; set; }
        public RPGBatalha Batalha { get; set; }
        public RPGMochila Mochila { get; set; }
        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<EnumProficiencia, RPGProficiencia> Proficiencias { get; set; }
        public RPGPersonagem(double expMax = 200) : base(expMax)
        {
            VidaAtual = SortearMetadeValor(50);
            VidaMaxima = VidaAtual;
            MagiaAtual = SortearMetadeValor(50);
            MagiaMaxima = MagiaAtual;
            AtaqueFisicoBase = SortearMetadeValor(50);
            AtaqueMagicoBase = SortearMetadeValor(50);
            DefesaFisicaBase = SortearMetadeValor(50);
            DefesaMagicaBase = SortearMetadeValor(50);
            FomeAtual = 100;
            FomeMaxima = 100;
            SedeAtual = 100;
            SedeMaxima = 100;
            EstaminaAtual = 100;
            EstaminaMaxima = 100;
            RegiaoAtualId = 0;
            ProficienciaPontos = 0;
            Batalha = new RPGBatalha();
            Mochila = new RPGMochila();
            //Adiciona as proficiencias
            Proficiencias = new Dictionary<EnumProficiencia, RPGProficiencia>
            {
                { EnumProficiencia.Ataque, new ProficienciaAtaque()},
                { EnumProficiencia.Defesa, new ProficienciaDefesa()},
                { EnumProficiencia.Forca, new ProficienciaForca()}
            };
            Mochila.AdicionarItem("moeda de Zeoin", new RPGMochilaItemData()
            {
                Id = 0,
                Quantidade = 10
            });
        }
        public new bool AdicionarExp(double exp)
        {
            int quant = base.AdicionarExp(exp);
            if (quant != 0)
            {
                while (quant != 0)
                {
                    quant--;
                    VidaMaxima = Evoluir(VidaMaxima);
                    MagiaMaxima = Evoluir(MagiaMaxima);
                    AtaqueFisicoBase = Evoluir(AtaqueFisicoBase);
                    AtaqueMagicoBase = Evoluir(AtaqueMagicoBase);
                    DefesaFisicaBase = Evoluir(DefesaFisicaBase);
                    DefesaMagicaBase = Evoluir(DefesaMagicaBase);
                    FomeMaxima = Evoluir(FomeMaxima);
                    SedeMaxima = Evoluir(SedeMaxima);
                    ProficienciaPontos++;
                }
                return true;
            }
            return false;
        }

        public bool TryGetProficiencia(string hab, out RPGProficiencia proficiencia)
        {
            var h = hab.ToLower();
            h = h.FirstUpper();
            if (Enum.IsDefined(typeof(EnumProficiencia), h))
            {
                proficiencia = Proficiencias[Enum.Parse<EnumProficiencia>(h)];
                return true;
            }
            proficiencia = null;
            return false;
        }

        public void EquiparItem(RPGItem item)
        {
            Mochila.RemoverItem(item.Nome.ToLower());
            DesequiparItem(item.Tipo);
            Mochila.Equipamentos.Add(item.Tipo, item);

            AtaqueFisicoExtra += item.AtaqueFisico;
            AtaqueMagicoExtra += item.AtaqueMagico;

            DefesaFisicaExtra += item.DefesaFisica;
            DefesaMagicaExtra += item.DefesaMagica;
        }

        public string DesequiparItem(EnumTipo itemTipo)
        {
            if (Mochila.Equipamentos.TryGetValue(itemTipo, out RPGItem item))
            {
                Mochila.AdicionarItem(item);
                Mochila.Equipamentos.Remove(itemTipo);

                AtaqueFisicoExtra -= item.AtaqueFisico;
                AtaqueMagicoExtra -= item.AtaqueMagico;

                DefesaFisicaExtra -= item.DefesaFisica;
                DefesaMagicaExtra -= item.DefesaMagica;
                return item.Nome;
            }
            return "";
        }

        private double SortearMetadeValor(double valor)
            => Sortear.Valor(valor / 2, valor);
    }
}
