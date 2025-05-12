using System;
using System.Collections.Generic;
using System.Linq;
using State_Solution_2.domains.state;
using System.Text;
using System.Threading.Tasks;
using State_Solution_2.domains;
using PadroesDeProjeto.Objects.Model;

namespace State_Solution_2.domains.state
{
    public class CanceladoState : State
    {
        private Pedido pedido;

        public CanceladoState(Pedido pedido)
        {
            this.pedido = pedido;
        }

        void State.cancelarPedido()
        {
            throw new Exception("Operacao não suportada, pedido foi cancelado");
        }

        void State.despacharPedido()
        {
            throw new Exception("Operacao não suportada, pedido foi cancelado");
        }

        void State.sucessoAoPagar()
        {
            throw new Exception("Operacao não suportada, pedido foi cancelado");
        }
    }

}
