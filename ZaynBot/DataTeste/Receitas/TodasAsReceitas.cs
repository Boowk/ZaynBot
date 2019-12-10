using System.Collections.Generic;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.DataTeste.Receitas
{
    public class TodasAsReceitas
    {
        public TodasAsReceitas()
        {
            ModuloBanco.Database.DropCollection("receitas");
            List<RPGReceita> receitas = new List<RPGReceita>()
            {
                ReceitasReceita.CarneDeCoelho(),
                ReceitasReceita.OssoAfiado(),
            };
            ModuloBanco.ReceitaColecao.InsertMany(receitas);
        }

    }
}
