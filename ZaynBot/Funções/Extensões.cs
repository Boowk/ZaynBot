using System;
using System.Collections.Generic;
using System.Text;
using ZaynBot.Entidades.EntidadesRpg;

namespace ZaynBot.Funções
{
    public static class Extensões
    {
        public static Mob Raca(this Mob mob, Raça raca)
        {
            Sortear sortear = new Sortear();
            mob.RaçaMob = raca;
            mob.PontosDeVida += sortear.Valor(raca.PontosDeVidaBaseMin, raca.PontosDeVidaBaseMax);
            mob.PontosDeVidaMaxima = mob.PontosDeVida;
            mob.AtaqueFisico += sortear.Valor(raca.AtaqueFisicoBaseMin, raca.AtaqueFisicoBaseMax);
            mob.DefesaFisica += sortear.Valor(raca.DefesaFisicaBaseMin, raca.DefesaFisicaBaseMax);
            mob.AtaqueMagico += sortear.Valor(raca.AtaqueMagicoBaseMin, raca.AtaqueMagicoBaseMax);
            mob.DefesaMagica += sortear.Valor(raca.DefesaMagicaBaseMin, raca.DefesaMagicaBaseMax);
            mob.Velocidade += sortear.Valor(raca.VelocidadeBaseMin, raca.VelocidadeBaseMax);
            return mob;
        }
    }
}
