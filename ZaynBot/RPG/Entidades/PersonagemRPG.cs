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
    }
}
