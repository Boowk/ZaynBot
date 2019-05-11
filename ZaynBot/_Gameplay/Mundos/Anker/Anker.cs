using System.Collections.Generic;
using ZaynBot.Entidades.EntidadesRpg.Mapa;

namespace ZaynBot._Gameplay.Mundos.Anker
{
    public static class Anker
    {
        public static Região AnkarEstrada()
        {

            /*
            *  RegiaoId: Deve ser unico, utilizado para encontrar a região
            *  RegiaoNome: Nome da região
            *  Descrição: Descrição da região.
            *  Saida = new List<Saida>(),     
               Mob = new List<Mob>(),
               Terreno = Região.Tipo.Campo,        
            * 
            */

            Região regiao = new Região()
            {
                RegiaoId = 0,
                RegiaoNome = "Estrada",
                Descrição = "Uma estrada de terra.",
                Terreno = Região.Tipo.Campo,

                //Padrão
                Saidas = new List<Saida>()
            };

            #region saidas

            // Cria as saidas
            Saida norte = new Saida
            {
                Direcao = Saida.Direcoes.Norte,
                RegiaoId = 1
            };

            #endregion  
            regiao.Saidas.Add(norte);

            return regiao;
        }

        public static Região AnkarEstrada2()
        {

            Região regiao = new Região()
            {
                RegiaoId = 1,
                RegiaoNome = "Estrada com pedras",
                Descrição = "Uma estrada de terra com pedras.",
                Terreno = Região.Tipo.Campo,

                //Padrão
                Saidas = new List<Saida>()
            };

            #region saidas

            // Cria as saidas
            Saida sul = new Saida
            {
                Direcao = Saida.Direcoes.Sul,
                RegiaoId = 0
            };

            #endregion  
            regiao.Saidas.Add(sul);

            return regiao;
        }
    }

}
