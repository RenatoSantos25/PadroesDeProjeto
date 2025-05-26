using System;
using System.Collections.Generic;
using System.Linq;
using State_Solution_2.domains.state;
using System.Text;
using System.Threading.Tasks;
using State_Solution_2.domains;
using PadroesDeProjeto.Objects.Model;
using TrabalhoDesignPatterns.WebAPI.Services.States;

namespace State_Solution_2.domains.state
{
	public class CanceladoState : IPedidoState
	{
		public IPedidoState CancelarPedido()
		{
			throw new Exception("Operação não suportada, o pedido foi cancelado");
		}

		public IPedidoState DespacharPedido()
		{
			throw new Exception("Operação não suportada, o pedido foi cancelado");
		}

		public IPedidoState SucessoAoPagar()
		{
			throw new Exception("Operação não suportada, o pedido foi cancelado");
		}
	}

}
