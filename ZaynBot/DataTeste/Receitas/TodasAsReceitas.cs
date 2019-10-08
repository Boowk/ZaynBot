using System.Collections.Generic;
using ZaynBot.RPG.Entidades;

namespace ZaynBot.DataTeste.Receitas
{
    public class TodasAsReceitas
    {
        public TodasAsReceitas()
        {
            ModuloBanco.Database.DropCollection("receitas");
            List<ReceitaRPG> receitas = new List<ReceitaRPG>()
            {
                ReceitasReceita.CarneDeCoelho(),
                ReceitasReceita.OssoAfiado(),
            };
            ModuloBanco.ReceitaColecao.InsertMany(receitas);
        }

    }
}
