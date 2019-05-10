using System;
using ZaynBot.Entidades.EntidadesRpg.EntidadesRpgMapa;

namespace ZaynBot.Entidades.EntidadesRpg
{

    public class Personagem
    {
        public string Raça { get; set; }
        public float PontosDeVida { get; set; }
        public float PontosDeVidaMaxima { get; set; }
        public float PontosDeMana { get; set; }
        public float PontosDeManaMaximo { get; set; }
        public float AtaqueFisico { get; set; }
        public float DefesaFisica { get; set; }
        public float AtaqueMagico { get; set; }
        public float DefesaMagica { get; set; }
        public float Velocidade { get; set; }
        public int Sorte { get; set; }

        public float Fome { get; set; }
        public float Sede { get; set; }

        public Região LocalAtual { get; set; }

        public Equipamento Equipamento { get; set; }
        public Habilidade Habilidade { get; set; }
        public Titulo Titulo { get; set; }
        public Emprego Emprego { get; set; }
        public object RegiaoAtual { get; internal set; }

        public Personagem()
        {
            Raça = "Humano";
            Random random = new Random();
            PontosDeVida = random.Next(10, 100);
            PontosDeVidaMaxima = PontosDeVida;
            PontosDeMana = random.Next(8, 100);
            PontosDeManaMaximo = PontosDeMana;
            AtaqueFisico = random.Next(9, 20);
            DefesaFisica = random.Next(7, 20);
            AtaqueMagico = random.Next(4, 20);
            DefesaMagica = random.Next(3, 20);
            Velocidade = random.Next(4, 6);
            Sorte = 10;
            Equipamento = new Equipamento();
            Habilidade = new Habilidade();
            Titulo = new Titulo();
            Emprego = new Emprego("Desempregado");
            LocalAtual = _Gameplay.Mundos.Anker.Areas.Regiões[0];
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
