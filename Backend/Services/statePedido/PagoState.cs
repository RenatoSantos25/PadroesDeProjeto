using PadroesDeProjeto.Objects.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrabalhoDesignPatterns.WebAPI.Services.States;

namespace State_Solution_2.domains.state
{
	public class PagoState : IPedidoState
	{
		public IPedidoState CancelarPedido()
		{
			return new CanceladoState();
		}

		public IPedidoState DespacharPedido()
		{
			return new EnviadoState();
		}

		public IPedidoState SucessoAoPagar()
		{
			throw new Exception("Operação não suportada, o pedido já foi pago");
		}
	}


}
