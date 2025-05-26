using PadroesDeProjeto.Objects.Enum;
using TrabalhoDesignPatterns.WebAPI.Objects.Enums;

namespace PadroesDeProjeto.Objects.Model
{
	public class Pedido
	{
		public int Id { get; set; }
		public double Subtotal { get; set; }
		public double ValorFrete { get; set; }
		public EstadoPedido EstadoAtual { get; set; }
		public Frete Frete { get; set; }

		public Pedido() { }

		public Pedido(int id, double subtotal, double valorFrete, Frete tipoFrete)
		{
			Id = id;
			Subtotal = subtotal;
			ValorFrete = valorFrete;
			EstadoAtual = EstadoPedido.AGUARDANDO_PAGAMENTO;
			Frete = tipoFrete;
		}
	}
}