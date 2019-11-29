using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.Options;
using System;
using System.Collections.Generic;
using ZaynBot.RPG.Entidades.Enuns;
using ZaynBot.RPG.Habilidades;
using ZaynBot.Utilidades;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    public class PersonagemRPG : ProgressoRPG
    {
        public MochilaRPG Inventario { get; set; }
        public RacaRPG Raca { get; set; }
        public BatalhaRPG Batalha { get; set; }

        [BsonDictionaryOptions(DictionaryRepresentation.ArrayOfArrays)]
        public Dictionary<HabilidadeEnum, HabilidadeRPG> Habilidades { get; set; }

        public int LocalAtualId { get; set; } = 0;


        public double VidaAtual { get; set; }
        public double VidaMax { get; set; }
        public double MagiaAtual { get; set; }
        public double MagiaMax { get; set; }
        public double FomeAtual { get; set; }
        public double FomeMax { get; set; }

        public PersonagemRPG(int nivel = 1, int nivelMax = 0, double expIncremento = 84, double incremento = 1.67)
            : base(nivel, nivelMax, expIncremento, incremento)
        {
            Inventario = new MochilaRPG();
            Batalha = new BatalhaRPG();
            Raca = ModuloBanco.RacaGetRandom();
            VidaMax = Raca.Resistencia * 2;
            VidaAtual = this.VidaMax;
            MagiaMax = Raca.Inteligencia * 2;
            MagiaAtual = MagiaMax;
            FomeMax = Raca.Resistencia * 1.6;
            FomeAtual = FomeMax;

            //Populando as habilidades
            Habilidades = new Dictionary<HabilidadeEnum, HabilidadeRPG>
            {
                { HabilidadeEnum.Perfurante, new PerfuranteHabilidade() },
                { HabilidadeEnum.Esmagante, new EsmaganteHabilidade() },
                { HabilidadeEnum.Desarmado, new DesarmadoHabilidade() }
            };
        }

        public bool TryGetHabilidade(string hab, out HabilidadeRPG habilidade)
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

        public new bool AdicionarExp(double exp)
        {
            int nivelAntigo = NivelAtual;
            bool isEvoluiu = base.AdicionarExp(exp);
            if (isEvoluiu)
            {
                Raca.Pontos += NivelAtual - nivelAntigo;
                VidaMax += Raca.Resistencia / 10;

                return true;
            }
            return false;
        }

        public void ReduzirFome()
            => FomeAtual -= (Raca.Forca * 1.3) / Raca.Resistencia;

        public double RecuperarVida(double quantidade)
        {
            if (quantidade + VidaAtual > VidaMax)
            {
                double v = VidaMax - VidaAtual;
                VidaAtual = VidaMax;
                return v;
            }
            VidaAtual += quantidade;
            return quantidade;
        }

        public double RecuperarMagia(double quantidade)
        {
            if (quantidade + MagiaAtual > MagiaMax)
            {
                double v = MagiaMax - MagiaAtual;
                MagiaAtual = MagiaMax;
                return v;
            }
            MagiaAtual += quantidade;
            return quantidade;
        }
    }
}
