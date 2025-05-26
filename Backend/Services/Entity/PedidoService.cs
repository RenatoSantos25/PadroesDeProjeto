using TrabalhoDesignPatterns.WebAPI.Data.Interfaces;
using TrabalhoDesignPatterns.WebAPI.Objects.DTOs;
using TrabalhoDesignPatterns.WebAPI.Objects.Enums;
using TrabalhoDesignPatterns.WebAPI.Objects.Models;
using TrabalhoDesignPatterns.WebAPI.Services.Interfaces;
using TrabalhoDesignPatterns.WebAPI.Services.States;
using TrabalhoDesignPatterns.WebAPI.Services.Strategies;

namespace TrabalhoDesignPatterns.WebAPI.Services.Entities;

public class PedidoService : Pedido, IPedidoService
{
    private readonly IPedidoRepository _repository;

    public PedidoService(IPedidoRepository pedidoRepository)
    {
        _repository = pedidoRepository;
    }

    public async Task<IEnumerable<PedidoDTO>> ListarTodos()
    {
        var pedidos = await _repository.Get();
        List<PedidoDTO> pedidosDTO = [];

        foreach (var pedido in pedidos)
        {
            pedidosDTO.Add(ConverterParaDTO(pedido));
        }

        return pedidosDTO;
    }

    public async Task<PedidoDTO> ObterPorId(int id)
    {
        var pedido = await _repository.GetById(id);

        if (pedido is null)
        {
            return null;
        }

        return ConverterParaDTO(pedido);
    }

    public async Task<PedidoDTO> GerarPedido(PedidoDTO pedidoDTO)
    {
        var pedido = ConverterParaModel(pedidoDTO);
        IFrete frete = CriarFretePorTipo(pedido.TipoFrete);

        pedido.EstadoAtual = EstadoPedido.AGUARDANDO_PAGAMENTO;
        pedido.ValorFrete = frete.CalcularFrete(pedido.Subtotal);

        await _repository.Add(pedido);
        return ConverterParaDTO(pedido);
    }

    public async Task<PedidoDTO> Atualizar(PedidoDTO pedidoDTO, int id)
    {
        var existingPedido = await _repository.GetById(id);

        if (existingPedido is null)
        {
            throw new KeyNotFoundException($"Pedido com id {id} n�o encontrado.");
        }

        if (existingPedido.EstadoAtual == EstadoPedido.AGUARDANDO_PAGAMENTO)
        {
            // Evita que o campo de estado seja alterado diretamente pelo usu�rio
            pedidoDTO.EstadoAtual = (int)existingPedido.EstadoAtual;

            // Recalcula o valor do frete
            IFrete frete = CriarFretePorTipo((TipoFrete)pedidoDTO.TipoFrete);
            pedidoDTO.ValorFrete = frete.CalcularFrete(pedidoDTO.Subtotal);
        } else
        {
            throw new Exception("N�o � permitido atualizar o pedido, ap�s sua confirma��o/cancelamento.");
        }

        var pedido = ConverterParaModel(pedidoDTO);
        await _repository.Update(pedido);

        return pedidoDTO;
    }

    public async Task<PedidoDTO> SucessoAoPagar(PedidoDTO pedidoDTO)
    {
        // Converter estado do DTO para uma classe State
        IPedidoState state = ObterEstadoClasse(ConverterParaModel(pedidoDTO).EstadoAtual);

        // Fazer a transi��o de estado
        IPedidoState novoEstado = state.SucessoAoPagar();

        // Converter o novo estado para o estado da DTO
        pedidoDTO.EstadoAtual = (int)ObterEstadoEnum(novoEstado);

        // Atualizar no banco
        await _repository.Update(ConverterParaModel(pedidoDTO));

        return pedidoDTO;
    }

    public async Task<PedidoDTO> DespacharPedido(PedidoDTO pedidoDTO)
    {
        IPedidoState state = ObterEstadoClasse(ConverterParaModel(pedidoDTO).EstadoAtual);
        IPedidoState novoEstado = state.DespacharPedido();
        pedidoDTO.EstadoAtual = (int)ObterEstadoEnum(novoEstado);

        await _repository.Update(ConverterParaModel(pedidoDTO));

        return pedidoDTO;
    }

    public async Task<PedidoDTO> CancelarPedido(PedidoDTO pedidoDTO)
    {
        IPedidoState state = ObterEstadoClasse(ConverterParaModel(pedidoDTO).EstadoAtual);
        IPedidoState novoEstado = state.CancelarPedido();
        pedidoDTO.EstadoAtual = (int)ObterEstadoEnum(novoEstado);

        await _repository.Update(ConverterParaModel(pedidoDTO));

        return pedidoDTO;
    }

    #region M�todos de Convers�o
    private IPedidoState ObterEstadoClasse(EstadoPedido estadoPedido)
    {
        return estadoPedido switch
        {
            EstadoPedido.AGUARDANDO_PAGAMENTO => new AguardandoPagamentoState(),
            EstadoPedido.PAGO => new PagoState(),
            EstadoPedido.ENVIADO => new EnviadoState(),
            EstadoPedido.CANCELADO => new CanceladoState(),
            _ => throw new ArgumentException("Estado inv�lido"),
        };
    }

    private EstadoPedido ObterEstadoEnum(IPedidoState state)
    {
        return state switch
        {
            AguardandoPagamentoState _ => EstadoPedido.AGUARDANDO_PAGAMENTO,
            PagoState _ => EstadoPedido.PAGO,
            EnviadoState _ => EstadoPedido.ENVIADO,
            CanceladoState _ => EstadoPedido.CANCELADO,
            _ => throw new ArgumentException("Estado inv�lido"),
        };
    }

    private IFrete CriarFretePorTipo(TipoFrete tipoFrete)
    {
        return tipoFrete switch
        {
            TipoFrete.AEREO => new FreteAereo(),
            TipoFrete.TERRESTRE => new FreteTerrestre(),
            _ => throw new ArgumentException("Tipo de frete inv�lido"),
        };
    }

    private PedidoDTO ConverterParaDTO(Pedido pedido)
    {
        PedidoDTO pedidoDTO = new()
        {
            Id = pedido.Id,
            Subtotal = pedido.Subtotal,
            ValorFrete = pedido.ValorFrete,
            EstadoAtual = (int)pedido.EstadoAtual,
            TipoFrete = (int)pedido.TipoFrete,
        };

        return pedidoDTO;
    }

    private Pedido ConverterParaModel(PedidoDTO pedidoDTO)
    {
        Pedido pedido = new()
        {
            Id = pedidoDTO.Id,
            Subtotal = pedidoDTO.Subtotal,
            ValorFrete = pedidoDTO.ValorFrete,
            EstadoAtual = (EstadoPedido)pedidoDTO.EstadoAtual,
            TipoFrete = (TipoFrete)pedidoDTO.TipoFrete,
        };

        return pedido;
    }
    #endregion
}