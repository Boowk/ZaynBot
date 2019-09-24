using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    public class BatalhaRPG
    {
        public float PontosDeAcaoTotal { get; set; }
        public float PontosDeAcao { get; set; }
        public int Turno { get; set; } = 0;

        public List<MobRPG> Inimigos { get; set; } = new List<MobRPG>();
    }
}
