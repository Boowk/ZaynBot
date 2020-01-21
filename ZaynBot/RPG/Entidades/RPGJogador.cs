using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;
using ZaynBot.RPG.Entidades.Enuns;
using ZaynBot.RPG.Exceptions;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class RPGJogador : RPGProgresso
    {
        [BsonId]
        public ulong Id { get; set; }
        public DateTime DataCriacao { get; set; }

        public int MobsMortos { get; set; }
        public int MortoPorMobs { get; set; }

        public int JogadoresMortos { get; set; }
        public int MortoPorJogadores { get; set; }

        public double VidaAtual { get; set; }
        public double VidaMaxima { get; set; }

        public double MagiaAtual { get; set; }
        public double MagiaMaxima { get; set; }

        public double AtaqueFisicoBase { get; set; }
        public double AtaqueFisicoExtra { get; set; }

        public double DefesaFisicaBase { get; set; }
        public double DefesaFisicaExtra { get; set; }

        public double DefesaMagicaBase { get; set; }
        public double DefesaMagicaExtra { get; set; }

        public double FomeAtual { get; set; }
        public double FomeMaxima { get; set; }

        public string RegiaoAtual { get; set; }

        public int ProficienciaPontos { get; set; }

        public RPGBatalha Batalha { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public RPGMochila Mochila { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<EnumTipo, RPGItem> Equipamentos { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<EnumProficiencia, RPGProficiencia> Proficiencias { get; set; }

        public RPGJogador(ulong id, double expMax = 200) : base(expMax)
        {
            Id = id;
            DataCriacao = DateTime.Now;
            VidaAtual = SortearMetadeValor(50);
            VidaMaxima = VidaAtual;
            MagiaAtual = SortearMetadeValor(50);
            MagiaMaxima = MagiaAtual;
            AtaqueFisicoBase = SortearMetadeValor(50);
            DefesaFisicaBase = SortearMetadeValor(50);
            DefesaMagicaBase = SortearMetadeValor(50);
            FomeAtual = 100;
            FomeMaxima = 100;
            RegiaoAtual = "esgotos";
            ProficienciaPontos = 0;
            Batalha = new RPGBatalha();
            Mochila = new RPGMochila();
            Equipamentos = new Dictionary<EnumTipo, RPGItem>();

            Proficiencias = new Dictionary<EnumProficiencia, RPGProficiencia>
            {
                { EnumProficiencia.Ataque, new ProficienciaAtaque()},
                { EnumProficiencia.Defesa, new ProficienciaDefesa()},
                { EnumProficiencia.Forca, new ProficienciaForca()}
            };
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
                    DefesaFisicaBase = Evoluir(DefesaFisicaBase);
                    DefesaMagicaBase = Evoluir(DefesaMagicaBase);
                    FomeMaxima = Evoluir(FomeMaxima);
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
            Mochila.RemoverItem(item.Nome);
            DesequiparItem(item.Tipo);
            Equipamentos.Add(item.Tipo, item);

            AtaqueFisicoExtra += item.AtaqueFisico;

            DefesaFisicaExtra += item.DefesaFisica;
            DefesaMagicaExtra += item.DefesaMagica;
        }

        public string DesequiparItem(EnumTipo itemTipo)
        {
            if (Equipamentos.TryGetValue(itemTipo, out RPGItem item))
            {
                Mochila.AdicionarItem(item);
                Equipamentos.Remove(itemTipo);

                AtaqueFisicoExtra -= item.AtaqueFisico;

                DefesaFisicaExtra -= item.DefesaFisica;
                DefesaMagicaExtra -= item.DefesaMagica;
                return item.Nome;
            }
            return null;
        }

        private double SortearMetadeValor(double valor) => Sortear.Valor(valor / 2, valor);

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
        public void RemoverVida(double quantidade)
        {
            VidaAtual -= quantidade;
            if (VidaAtual <= 0)
            {
                VidaAtual = VidaMaxima / 3;
                MortoPorMobs++;
                ExpAtual = 0;
                Salvar(this);
                throw new PersonagemNoLifeException();
            }
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

        public static void Salvar(RPGJogador jogador) => ModuloBanco.EditJogador(jogador);
        public void Salvar() => ModuloBanco.EditJogador(this);
    }
}
