using System;
using System.Collections.Generic;
using System.Text;
using ZaynBot.DataTeste.Mobs.Normal;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.DataTeste.Mobs
{
    public class TodosOsMobs
    {
        public TodosOsMobs()
        {
            ModuloBanco.Database.DropCollection("mobs");
            List<MobRPG> mobs = new List<MobRPG>()
            {
             MobsAnker.CoelhoMobAb(),
             MobsAnker.EsqueletoMobAb(),
             MobsAnker.SlimeMobAb(),
            };
            ModuloBanco.MobColecao.InsertMany(mobs);
        }
    }
}
