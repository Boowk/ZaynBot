using System.Collections.Generic;
using ZaynBot._Gameplay.Raças;
using ZaynBot.Entidades.EntidadesRpg;
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
                Id = 0,
                RegiaoNome = "Estrada",
                Descrição = "Uma estrada de terra.",
                Terreno = Região.Tipo.Campo,

                //Padrão
                Saidas = new List<Saida>()
            };

            Mob NpcTeste = new Mob("Npc de teste :)")
            {

            }.SetRaça(Humano.HumanoAb());
            regiao.Inimigos.Add(NpcTeste);

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
                Id = 1,
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
