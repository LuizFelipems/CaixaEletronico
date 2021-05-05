using CaixaEletronico.Domain.Entidades;
using CaixaEletronico.Domain.Servicos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace AppCaixaEletronico
{
    class Program
    {
        static int SaldoCliente;
        static List<Nota> Resultado;
        static int ValorSaque;
        static ServicoCaixaEletronico servicoCaixaEletronico = new ServicoCaixaEletronico();

        static void Main(string[] args)
        {
            Console.WriteLine("Caixa Eletrônico");
            ReposicaoAutomatica();

            bool continuar = true;

            SaldoCliente = new Random().Next(0, 1500);

            while (continuar)
            {
                if (servicoCaixaEletronico.TotalEstoque() <= 100)
                    ReposicaoAutomatica();

                Console.WriteLine($"Saldo Disponível do Cliente: {SaldoCliente}");
                Console.WriteLine("Valor de Saque");
                ValorSaque = int.Parse(Console.ReadLine());

                if (ValorSaque == 0)
                {
                    continuar = false;
                    return;
                }

                try
                {
                    Resultado = servicoCaixaEletronico.Saque(ValorSaque, SaldoCliente);
                    SaldoCliente -= ValorSaque;

                    Console.WriteLine($"Entregar: ");
                    Resultado.Select(i => $"{i.Nome}: {i.Quantidade}").ToList().ForEach(Console.WriteLine);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public static void ReposicaoAutomatica()
        {
            servicoCaixaEletronico.ReporEstoque(new List<Nota>
                                                { new Nota(100, 3),
                                                    new Nota(50, 3),
                                                    new Nota(20, 3),
                                                    new Nota(10, 3)
                                                });

            Console.WriteLine($"Repondo Estoque...");
        }
    }
}
