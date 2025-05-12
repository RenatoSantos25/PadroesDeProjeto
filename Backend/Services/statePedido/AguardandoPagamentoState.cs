using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using PadroesDeProjeto.Objects.Model;
using State_Solution_2.domains.state;
namespace PadroesDeProjeto
{
    internal class AguardandoPagamentoState : State
    {
        private Pedido pedido;

        public AguardandoPagamentoState(Pedido pedido)
        {
            this.pedido = pedido;
        }

        void State.cancelarPedido()
        {
            //  this.pedido.estadoAtual = new CanceladoState(pedido);
        }

        void State.despacharPedido()
        {
            throw new Exception("Operacao não suportada, " +
                                "pedido ainda não foi pago");
        }

        void State.sucessoAoPagar()
        {
        //    this.pedido.estadoAtual = new PagoSate(pedido);
        }
    }

}
