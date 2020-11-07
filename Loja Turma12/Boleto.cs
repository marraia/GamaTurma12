using System;

namespace Loja_Turma12
{
    public class Boleto : Pagamento
    {
        public Boleto(double valor)
            : base (valor)
        {

        }

        public Guid Numero { get; set; }
        public DateTime DataVencimento { get; set; }
        public string Banco { get; set; }
        public decimal Juros { get; set; }

        public void GerarBoleto()
        {
            Numero = Guid.NewGuid();
            DataVencimento = DateTime.Now.AddDays(-1);
        }

        public void CalcularJurosVencimento()
        {
            var taxa = Valor * 0.05;
            Valor = Valor + taxa;
        }

        public override void Pagar()
        {
            Data = DateTime.Now;
            Confirmacao = true;
        }
    }
}
