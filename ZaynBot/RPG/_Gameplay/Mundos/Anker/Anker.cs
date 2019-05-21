using System.Collections.Generic;
using ZaynBot.RPG._Gameplay.Raças;
using ZaynBot.RPG.Entidades;
using ZaynBot.RPG.Entidades.Mapa;

namespace ZaynBot.RPG._Gameplay.Mundos.Anker
{
    public static class Anker
    {
        public static RPGRegião AnkarEstrada()
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

            RPGRegião regiao = new RPGRegião()
            {
                Id = 0,
                RegiaoNome = "Estrada",
                Descrição = "Uma estrada de terra.",
                Terreno = RPGRegião.Tipo.Campo,

                //Padrão
                Saidas = new List<RPGSaida>()
            };

            RPGMob NpcTeste = new RPGMob("Npc de teste :)")
            {

            }.SetRaça(Humano.HumanoAb());
            regiao.Inimigos.Add(NpcTeste);

            #region saidas

            // Cria as saidas
            RPGSaida norte = new RPGSaida
            {
                Direcao = RPGSaida.Direcoes.Norte,
                RegiaoId = 1
            };

            #endregion  
            regiao.Saidas.Add(norte);

            return regiao;
        }

        public static RPGRegião AnkarEstrada2()
        {

            RPGRegião regiao = new RPGRegião()
            {
                Id = 1,
                RegiaoNome = "Estrada com pedras",
                Descrição = "Uma estrada de terra com pedras.",
                Terreno = RPGRegião.Tipo.Campo,

                //Padrão
                Saidas = new List<RPGSaida>()
            };

            #region saidas

            // Cria as saidas
            RPGSaida sul = new RPGSaida
            {
                Direcao = RPGSaida.Direcoes.Sul,
                RegiaoId = 0
            };

            #endregion  
            regiao.Saidas.Add(sul);

            return regiao;
        }
    }

}
