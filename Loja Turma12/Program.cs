using System;
using System.Collections.Generic;
using System.Linq;

namespace Loja_Turma12
{
    class Program
    {
        private static List<Pagamento> listaCompraDinheiro;
        private static List<Boleto> listaCompraBoleto;
        static void Main(string[] args)
        {
            listaCompraBoleto = new List<Boleto>();
            listaCompraDinheiro = new List<Pagamento>();

            while (true)
            {
                Console.WriteLine("======================================");
                Console.WriteLine("Selecione uma Opção");
                Console.WriteLine("1-Compra | 2-Pagamento | 3-Dinheiro");
                var opcao = int.Parse(Console.ReadLine());

                switch (opcao)
                {
                    case 1:
                        Comprar();
                        break;
                    case 2:
                        PagarBoleto();
                        break;
                    case 3:
                        PagarDinheiro();
                        break;
                    default:
                        break;
                }
            }
        }

        public static void Comprar()
        {
            Console.WriteLine("Digite o valor:");
            var valor = Double.Parse(Console.ReadLine());
            Console.WriteLine("Digite se haverá parcelamento:");
            var parcelamento = int.Parse(Console.ReadLine());

            if (parcelamento > 0)
            {
                var compraDinheiro = new Dinheiro(valor, parcelamento);
                Console.WriteLine($"Numero da transaçao {compraDinheiro.Id}");
                listaCompraDinheiro.Add(compraDinheiro);
            }
            else
            {
                var compraBoleto = new Boleto(valor);
                compraBoleto.GerarBoleto();
                Console.WriteLine($"Numero do boleto {compraBoleto.Numero} com data para  {compraBoleto.DataVencimento}");
                listaCompraBoleto.Add(compraBoleto);
            }
        }

        public static void PagarDinheiro()
        {
            Console.WriteLine($"Digite a transação");
            var transacao = Guid.Parse(Console.ReadLine());

            var pagamento = listaCompraDinheiro
                                .Where(a => a.Id == transacao)
                                .FirstOrDefault();

            if (pagamento != null)
            {
                Console.WriteLine($"=============== Á VISTA { pagamento.Valor } ==================");
                pagamento.Pagar();
                Console.WriteLine($"Número do pagamento {pagamento.Id} pago no valor: {pagamento.Valor}");
            }
        }
        public static void PagarBoleto()
        {
            Console.WriteLine($"Digite o número do boleto");
            var numeroBoleto = Guid.Parse(Console.ReadLine());

            var boleto = listaCompraBoleto
                                .Where(a => a.Numero == numeroBoleto)
                                .FirstOrDefault();

            if (boleto != null)
            {
                if (boleto.DataVencimento < DateTime.Now)
                {
                    boleto.CalcularJurosVencimento();
                    Console.WriteLine($"Boleto está vencido, terá acrescimo de 5% ==== R$ {boleto.Valor}");
                }

                boleto.Pagar();
                Console.WriteLine($"Número do boleto {boleto.Numero} pago no valor: {boleto.Valor}");
            }
        }
    }
}
