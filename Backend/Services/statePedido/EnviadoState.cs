using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using State_Solution_2.domains.state;
using State_Solution_2.domains;
using PadroesDeProjeto.Objects.Model;
using TrabalhoDesignPatterns.WebAPI.Services.States;

namespace State_Solution_2.domains.state
{
	public class EnviadoState : IPedidoState
	{
		public IPedidoState CancelarPedido()
		{
			throw new Exception("Operação não suportada, o pedido já foi enviado");
		}

		public IPedidoState DespacharPedido()
		{
			throw new Exception("Operação não suportada, o pedido já foi enviado");
		}

		public IPedidoState SucessoAoPagar()
		{
			throw new Exception("Operação não suportada, o pedido já foi enviado");
		}
	}
}
