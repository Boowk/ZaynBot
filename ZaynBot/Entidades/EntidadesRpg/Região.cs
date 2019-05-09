using System;
using System.Collections.Generic;
using System.Text;

namespace ZaynBot.Entidades.EntidadesRpg
{
    public class Região
    {
        //CIDADE               |   1  | 
        //CAMPO                |   2  | 
        //FLORESTA             |   3  | 
        //COLINAS              |   4  | 
        //MONTANHAS            |   5  |
        //MAR OU RIO           |   6  |    
        //AR                   |   7  |
        //DESERTO              |   8  |
        //DESCONHECIDO         |   9  |    
        //PROIBIDO

        public enum Tipo
        {
            Cidade,
            Campo,
            Floresta,
            Colina,
            Montanha,
            Agua,
            Ar,
            Deserto,
            Desconhecido,
            Proibido
        }
        public string Nome { get; set; }
        public string Area { get; set; }
        public int AreaId { get; set; }
        public Tipo Terreno { get; set; }
        public string Descrição { get; set; }
    }
}
