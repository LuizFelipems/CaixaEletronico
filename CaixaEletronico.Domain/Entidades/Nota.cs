using System;
using System.Collections.Generic;
using System.Text;

namespace CaixaEletronico.Domain.Entidades
{
    public class Nota
    {
        public string Nome { get; set; }
        public int Valor { get; set; }
        public int Quantidade { get; set; }

        public Nota() { }

        public Nota(int valor, int quantidade)
        {
            Nome = $"R$ {valor},00";
            Valor = valor;
            Quantidade = quantidade;
        }
    }
}
