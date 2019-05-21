﻿using ZaynBot.RPG.Entidades;

namespace ZaynBot._Gameplay.Raças
{
    public static class Humano
    {
        public static RPGRaça HumanoAb()
        {
            RPGRaça raça = new RPGRaça("Humano");
            raça.SetPontosDeVidaBase(10, 100);
            raça.SetPontosDeManaBase(8, 20);
            raça.SetAtaqueFisicoBase(9, 15);
            raça.SetDefesaFisicaBase(7, 15);
            raça.SetAtaqueMagicoBase(4, 12);
            raça.SetDefesaMagicaBase(3, 8);
            raça.SetVelocidadeBase(4, 6);
            raça.SetSorteBase(8, 10);
            return raça;
        }
    }
}
