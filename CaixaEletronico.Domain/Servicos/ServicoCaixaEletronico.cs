using CaixaEletronico.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CaixaEletronico.Domain.Servicos
{
    public class ServicoCaixaEletronico
    {
        public static List<Nota> Estoque { get; private set; }

        public List<Nota> Saque(int valorSaque, int saldoCliente)
        {
            if (!ValidarEstoque(valorSaque))
                throw new Exception($"Saldo Disponível do Caixa: {TotalEstoque()}");

            if (!ValidarSaldoCliente(valorSaque, saldoCliente))
                throw new Exception($"Saldo Indisponível: {saldoCliente}");

            List<Nota> Resultado = new List<Nota>();

            Estoque.OrderByDescending(x => x.Valor).ToList().ForEach(item => {
                if (item.Valor <= valorSaque)
                {
                    int quantidade = valorSaque / item.Valor;
                    valorSaque -= quantidade * item.Valor;
                    Resultado.Add(new Nota(item.Valor, quantidade));
                }
            });

            DebitarEstoque(Resultado);

            return Resultado;
        }

        public void ReporEstoque(List<Nota> notas)
        {
            Estoque = notas;
        }

        private void DebitarEstoque(List<Nota> notas)
        {
            notas.ForEach(item => {
                Nota nota = Estoque.Select(x => 
                                        new Nota(x.Valor, x.Quantidade - item.Quantidade))
                                        .FirstOrDefault(x => x.Valor == item.Valor);

                Estoque = Estoque.Where(x => x.Valor != item.Valor).ToList();
                Estoque.Add(nota);
            });
        }

        private bool ValidarSaldoCliente(int valorSaque, int saldoCliente)
        {
            return saldoCliente >= valorSaque;
        }

        private bool ValidarEstoque(int valorSaque)
        {
            return TotalEstoque() >= valorSaque;
        }

        public int TotalEstoque()
        {
            return Estoque?.Sum(x => x.Quantidade * x.Valor) ?? 0;
        }
    }
}
