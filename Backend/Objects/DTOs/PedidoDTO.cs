namespace TrabalhoDesignPatterns.WebAPI.Objects.DTOs;

public class PedidoDTO
{
    public int Id { get; set; }
    public double Subtotal { get; set; }
    public double ValorFrete { get; set; }
    public int EstadoAtual { get; set; }
    public int TipoFrete { get; set; }
}