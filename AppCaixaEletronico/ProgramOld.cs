using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace AppCaixaEletronico
{
    class ProgramOld
    {
        static int[] NotasDisponíveis = { 100, 50, 20, 10 };
        static Dictionary<int, int> Resultado;
        static int ValorSaque;

        static void MainOld(string[] args)
        {
            Console.WriteLine("Caixa Eletrônico");

            bool continuar = true;

            while (continuar)
            {
                Resultado = new Dictionary<int, int>();

                Console.WriteLine("Valor de Saque");
                ValorSaque = int.Parse(Console.ReadLine());

                if (ValorSaque == 0)
                {
                    continuar = false;
                    return;
                }

                var notas = NotasDisponíveis.ToList().OrderByDescending(x => x);
                foreach (int item in notas)
                {
                    ValidaNota(item);
                }

                Console.WriteLine($"Entregar: ");
                Resultado.Select(i => $"{i.Key}: {i.Value}").ToList().ForEach(Console.WriteLine);
            }
        }

        public static void ValidaNota(int item)
        {
            if (item <= ValorSaque)
            {
                int res = ValorSaque / item;
                ValorSaque -= res * item;
                Resultado.Add(item, res);
            }
        }
    }
}
