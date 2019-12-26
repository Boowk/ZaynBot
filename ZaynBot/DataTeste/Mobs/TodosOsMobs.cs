using System.Collections.Generic;
using ZaynBot.DataTeste.Mobs.Normal;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.DataTeste.Mobs
{
    public class TodosOsMobs
    {
        public TodosOsMobs()
        {
            ModuloBanco.Database.DropCollection("mobs");
            List<RPGMob> mobs = new List<RPGMob>()
            {
                MobsAnker.MobLadrao(),
                MobsAnker.MobLadrao1(),
                MobsAnker.MobRato(),
                MobsAnker.MobCoelho(),
                MobsAnker.MobCoelho1(),
                MobsAnker.MobPato(),
                MobsAnker.MobVaca(),
                MobsAnker.MobVaca1(),
                MobsAnker.MobVaca2(),
                MobsAnker.MobGalinha(),
                MobsAnker.MobGalinha1(),
                MobsAnker.MobGalinha2(),
            };
            ModuloBanco.MobColecao.InsertMany(mobs);
        }
    }
}
