using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.Entidades.EntidadesRpg
{
    public class Npc
    {
        public string Nome { get; set; }
        public string FalaInicio { get; set; }
        public Dictionary<int, string> Variaveis { get; set; }
        public List<NpcLogica> Logica { get; set; }
        public Dictionary<string, NpcVenda> ItensAVenda { get; set; }
    }

    public class NpcVenda
    {
        public int Preco { get; set; }
        public Item Item { get; set; }
    }

    public class NpcLogica
    {
        public int Pergunta { get; set; }
        public int Respostas { get; set; }
        public bool Loja { get; set; } = false;
    }
}
