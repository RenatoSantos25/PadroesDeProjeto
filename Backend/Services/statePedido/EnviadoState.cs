using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using State_Solution_2.domains.state;
using State_Solution_2.domains;
using PadroesDeProjeto.Objects.Model;

namespace State_Solution_2.domains.state
{
    public class EnviadoState : State
    {
        private Pedido pedido;

        public EnviadoState(Pedido pedido)
        {
            this.pedido = pedido;
        }

        void State.cancelarPedido()
        {
            throw new Exception("Operacao não suportada, pedido já foi enviado");
        }

        void State.despacharPedido()
        {
            throw new Exception("Operacao não suportada, pedido já foi enviado");
        }

        void State.sucessoAoPagar()
        {
            throw new Exception("Operacao não suportada, pedido já foi enviado");
        }
    }

}
