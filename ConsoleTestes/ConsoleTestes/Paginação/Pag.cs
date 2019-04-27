﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleTestes.Paginação
{
    public class Pag
    {
        List<string> listaItens = new List<string>()
        {
            "Item 1","Item 2","Item 3","Item 4","Item 5","Item 6","Item 7","Item 8","Item 9","Item 10",
            "Item 11","Item 12","Item 13","Item 14","Item 15","Item 16","Item 17","Item 18","Item 19","Item 20",
            "Item 21","Item 22","Item 23","Item 24","Item 25","Item 26","Item 27","Item 28","Item 29","Item 30",
        };

        int itensPorPagina = 4;

        int paginaAtual = 0;

        private string ObterItem(int indice)
        {
            return listaItens[indice];
        }

        public void ProximaPagina()
        {
            paginaAtual++;

            ExibirItens();
        }

        public void PaginaAnterior()
        {
            paginaAtual--;

            ExibirItens();
        }

        public void ExibirItens()
        {
            int indiceItem = (paginaAtual * itensPorPagina);

            int maximoItens = (indiceItem + itensPorPagina);

            for (int item = indiceItem; item < maximoItens; item++)
            {
                Console.WriteLine(ObterItem(item));
            }
        }
    }
}
