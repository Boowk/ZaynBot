using MongoDB.Bson.Serialization.Attributes;
using System;
using ZaynBot.RPG.Habilidades;

namespace ZaynBot.RPG.Entidades
{
    [BsonIgnoreExtraElements]
    [BsonDiscriminator("Hab")]
    [BsonKnownTypes(typeof(DesarmadoHabilidade), typeof(EsmaganteHabilidade), typeof(PerfuranteHabilidade))]
    public class HabilidadeRPG : ProgressoRPG
    {
        public string Nome { get; set; }

        public HabilidadeRPG(string nome, int nivel, int nivelMax, double expIncremento, double incremento) : base(nivel, nivelMax, expIncremento, incremento)
        {
            Nome = nome;
        }


        //    [BsonId]
        //    public int Id { get; set; }
        //    public string Nome { get; set; }
        //    public string Descricao { get; set; }
        //    public float CustoMana { get; set; } = 0;//Aumenta 5% a cada nivel = * 1,05
        //    public float DanoFisicoPorcentagem { get; set; } = 0;// 100%(1.0) ou mais baseado no ataque fisico
        //    public float DanoMagicoPorcentagem { get; set; } = 0;// 100%(1.0) ou mais baseado no ataque magico
        //    public float CuraPorcentagem { get; set; } = 0;// 100%(1.0) ou mais baseado na defesa magica

        //    public void GetAtributos(int Nivel, RacaRPG raca)
        //    {
        //        DanoFisicoPorcentagem = DanoFisicoPorcentagem * raca.AtaqueFisico;
        //        DanoMagicoPorcentagem = DanoMagicoPorcentagem * raca.AtaqueMagico;
        //        CuraPorcentagem = CuraPorcentagem * raca.DefesaMagica;
        //        for (int i = 0; i < Nivel; i++)
        //        {
        //            CustoMana *= 1.05F;
        //            DanoFisicoPorcentagem *= 1.05F;
        //            DanoMagicoPorcentagem *= 1.05F;
        //            CuraPorcentagem *= 1.05F;
        //        }
        //    }
    }

    //public class HabilidadeDataRPG
    //{
    //    public int Id { get; set; }
    //    public int Nivel { get; set; }
    //}
}