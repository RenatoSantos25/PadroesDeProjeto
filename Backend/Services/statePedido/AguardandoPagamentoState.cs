using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using PadroesDeProjeto.Objects.Model;
using State_Solution_2.domains.state;
using TrabalhoDesignPatterns.WebAPI.Services.States;
namespace PadroesDeProjeto
{
	public class AguardandoPagamentoState : IPedidoState
	{
		public IPedidoState CancelarPedido()
		{
			return new CanceladoState();
		}

		public IPedidoState DespacharPedido()
		{
			throw new Exception("Operação não suportada, " +
				"o pedido ainda não foi pago");
		}

		public IPedidoState SucessoAoPagar()
		{
			return new PagoState();
		}
	}
}
