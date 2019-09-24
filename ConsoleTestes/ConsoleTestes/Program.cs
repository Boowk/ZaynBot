using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace ConsoleTestes
{
    public class Program
    {
        public enum g
        {
            [Description("São Paulo")]
            Item1,
            [Description("O loco meu")]
            item2


        }
        private static void Main(string[] args)
        {
            #region Organizar lista
            // List<Item> itens = new List<Item>()
            // {
            //     new Item(){ Nome = "Espada", TipoItem = Item.Tipo.Arma},
            //     new Item(){Nome = "Livro", TipoItem = Item.Tipo.Livro},
            //     new Item()      { Nome ="Livro do cara", TipoItem = Item.Tipo.Livro},
            //      new Item()      { Nome ="Livro da capa", TipoItem = Item.Tipo.Livro},
            //       new Item(){ Nome = "Arco", TipoItem = Item.Tipo.Arco},
            //       new Item()      { Nome ="Livro Outz", TipoItem = Item.Tipo.Livro},
            //       new Item()      { Nome ="Espada de pedra", TipoItem = Item.Tipo.Arma},
            // };

            //itens = itens.OrderBy(c => c.TipoItem).ToList();

            // foreach (var item in itens)
            // {
            //     Console.WriteLine($"{item.Nome} - {item.TipoItem}");
            // }
            #endregion

            #region Random de double
            //for (int i = 0; i < 20; i++)
            //{

            //Console.WriteLine(GetRandomNumber(50, 100));
            //}

            #endregion

            #region Páginação

            //Pag pag = new Pag();

            //while (true)
            //{
            //    Console.Clear();
            //    pag.ExibirItens();

            //    Console.WriteLine("1 - Proxima pagina\n2 - Pagina anterior");
            //    int escolha;
            //    int.TryParse(Console.ReadLine(), out escolha);

            //    switch (escolha)
            //    {
            //        case 1:
            //            pag.ProximaPagina();
            //            break;
            //        case 2:
            //            pag.PaginaAnterior();
            //            break;
            //    }
            //}
            #endregion

            #region pegar descrição enum
            //System.Reflection.MemberInfo[] mia = null;
            //System.ComponentModel.DescriptionAttribute[] atribs = null;
            //foreach (Enum teste in Enum.GetValues(typeof(g)))
            //{
            //    mia = teste.GetType().GetMember(teste.ToString());
            //    atribs = (mia != null && mia.Length > 0) ? (System.ComponentModel.DescriptionAttribute[])
            //    mia[0].GetCustomAttributes(typeof(DescriptionAttribute), false) : null;
            //    Console.WriteLine(atribs != null && atribs.Length == 1 ?
            //       atribs[0].Description : teste.ToString());
            //}
            //Console.WriteLine(atribs[0].Description.ToString());
            #endregion


            Tipos tipo = Tipos.Recurso | Tipos.Arma;

            Console.WriteLine("Os tipos são {0}.", tipo);

            if (tipo.HasFlag(Tipos.Recurso))
                Console.WriteLine("Recurso");

            Console.ReadLine();

        }

        [Flags]
        enum Tipos
        {
            Recurso = 1,
            Arma = 2,
            Unico = 4,
            Armadura = 8,
            Escudo = 16,
        }
    }
}
