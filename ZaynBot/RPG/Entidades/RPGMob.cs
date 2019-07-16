using System.Collections.Generic;

namespace ZaynBot.RPG.Entidades
{
    public class RPGMob
    {
        public string Nome { get; set; }
        public float PontosDeVida { get; set; }
        public float AtaqueFisico { get; set; }
        public float DefesaFisica { get; set; }
        public float AtaqueMagico { get; set; }
        public float DefesaMagica { get; set; }
        public int Velocidade { get; set; }
        public int ChanceDeAparecer { get; set; }
        public float Essencia { get; set; }
        public List<RPGItemDrop> ChanceItemUnico { get; set; } = new List<RPGItemDrop>();
        public List<RPGItemDrop> ChanceItemTodos { get; set; } = new List<RPGItemDrop>();

        //Calculos feitos em outras áreas 
        public float DanoFeito { get; set; } = 0;

        public float PontosDeAcao { get; set; } //Determina se ataca ou não.
    }
}
