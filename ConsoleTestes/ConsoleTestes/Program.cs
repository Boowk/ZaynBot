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

            //ClassCopia c1 = new ClassCopia();
            //c1.Id = 1;
            //c1.Nome = "Classe 1";
            //Console.WriteLine($"Classe 1 criada: id: {c1.Id}, nome: {c1.Nome}");

            //// ClassCopia c2 = c1;
            //ClassCopia c2 = new ClassCopia();
            //c2 = c1;
            //c2.Id = 5;
            //c2.Nome = "Classe 2";
            //Console.WriteLine($"Classe 2 criada: id: {c2.Id}, nome: {c2.Nome}");

            //Console.WriteLine("\n\n\n");

            //Console.WriteLine($"Classe 1: id: {c1.Id}, nome: {c1.Nome}");
            //Console.WriteLine($"Classe 2: id: {c2.Id}, nome: {c2.Nome}");

            //double calc;
            //calc = 24 / 100.0;
            //Console.WriteLine(calc);

            //Console.ReadKey();

            ////Teste de rounds
            //int velocidadejogador = 33;
            //int velocidadeinimigo = 100;
            //float turnojogador = 0;
            //float turnoinimigo = 0;
            //int velocidadebatalha = velocidadejogador + velocidadeinimigo;

            //int turno = 1;

            //while (turno <= 30)
            //{

            //    if (turnojogador >= velocidadebatalha)
            //    {
            //        turnojogador = 0;
            //        Console.WriteLine($"Turno {turno}");
            //        turno++;
            //        Console.ForegroundColor = ConsoleColor.Red;
            //        Console.WriteLine("Vez do jogador");
            //        Console.ResetColor();

            //    }
            //    if (turnoinimigo >= velocidadebatalha)
            //    {
            //        turnoinimigo = 0;
            //        Console.WriteLine($"Turno {turno}");
            //        turno++;
            //        Console.WriteLine("--- Vez do inimigo");
            //    }
            //    turnojogador += velocidadejogador / 4;
            //    turnoinimigo += velocidadeinimigo / 4;
            //}

            //Console.WriteLine("Fim");
            //Console.ReadLine();

            //float resistencia = 26;
            //float porcentagemFinal = 100 / (100 + resistencia);
            //Console.WriteLine("Porcentagem: " + porcentagemFinal);
            //float danoReal = 111;
            //float dano = danoReal * porcentagemFinal;

            //Console.WriteLine("Dano real: " + dano);

            //Console.WriteLine("Fim");
            //Console.ReadLine();
            Dictionary<string, ClassCopia> f = new Dictionary<string, ClassCopia>();
            f.Add("1", new ClassCopia
            {
                Id = 1,
                Nome = "Nome1"
            });

            f.TryGetValue("1", out ClassCopia valor);

            ClassCopia copia2 = valor.DeepCopy();
            copia2.Nome = "nome2";
            copia2.Id = 2;
            Console.WriteLine(valor.Nome + " " + valor.Id);
            Console.WriteLine(copia2.Nome + " " + copia2.Id);

            ClassCopia c3 = valor;
            c3.Nome = "nome3";
            c3.Id = 3;

            Console.WriteLine(valor.Nome + " " + valor.Id);
            Console.WriteLine(c3.Nome + " " + c3.Id);

            Console.ReadLine();

        }

        //public static double GetRandomNumber(double minimum, double maximum)
        //{
        //    Random random = new Random();
        //    return random.NextDouble() * (maximum - minimum) + minimum;
        //}
    }
}
