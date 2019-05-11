using MongoDB.Bson.Serialization.Attributes;
using System;
using ZaynBot._Gameplay.Raças;
using ZaynBot.Entidades.EntidadesRpg.Mapa;
using ZaynBot.Funções;

namespace ZaynBot.Entidades.EntidadesRpg
{
    [BsonIgnoreExtraElements]
    public class Personagem
    {
        public Raça RaçaPersonagem { get; set; }
        public float PontosDeVida { get; set; }
        public float PontosDeVidaMaxima { get; set; }
        public float PontosDeMana { get; set; }
        public float PontosDeManaMaximo { get; set; }
        public float AtaqueFisico { get; set; }
        public float DefesaFisica { get; set; }
        public float AtaqueMagico { get; set; }
        public float DefesaMagica { get; set; }
        public int Velocidade { get; set; }
        public int Sorte { get; set; }

        public float Fome { get; set; }
        public float Sede { get; set; }

        public Região LocalAtual { get; set; }

        public Equipamento Equipamento { get; set; }
        public Habilidade Habilidade { get; set; }
        public Titulo Titulo { get; set; }
        public Emprego Emprego { get; set; }

        public bool Vivo { get; set; }
        public DateTime DataMorte { get; set; }

        public Personagem()
        {
            RaçaPersonagem = Humano.HumanoAb();
            Sortear sortear = new Sortear();
            PontosDeVida = sortear.Valor(RaçaPersonagem.PontosDeVidaBase, 100);
            PontosDeVidaMaxima = PontosDeVida;
            PontosDeMana = sortear.Valor(RaçaPersonagem.PontosDeManaBase, 100);
            PontosDeManaMaximo = PontosDeMana;
            AtaqueFisico = sortear.Valor(RaçaPersonagem.AtaqueFisicoBase, 20);
            DefesaFisica = sortear.Valor(RaçaPersonagem.DefesaFisicaBase, 20);
            AtaqueMagico = sortear.Valor(RaçaPersonagem.AtaqueMagicoBase, 20);
            DefesaMagica = sortear.Valor(RaçaPersonagem.DefesaMagicaBase, 20);
            Velocidade = sortear.Valor(RaçaPersonagem.VelocidadeBase, 6);
            Sorte = 10;
            Equipamento = new Equipamento();
            Habilidade = new Habilidade();
            Titulo = new Titulo();
            Emprego = new Emprego("Desempregado");
            LocalAtual = _Gameplay.Mundos.Anker.Areas.Regiões[0];
            Vivo = true;
        }

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
