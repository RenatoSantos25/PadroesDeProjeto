using Microsoft.EntityFrameworkCore;
using TrabalhoDesignPatterns.WebAPI.Objects.Models;

namespace TrabalhoDesignPatterns.WebAPI.Data.Builders;

public class PedidoBuilder
{
    public static void Build(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pedido>().HasKey(p => p.Id);
        modelBuilder.Entity<Pedido>().Property(p => p.Subtotal).IsRequired();
        modelBuilder.Entity<Pedido>().Property(p => p.ValorFrete).IsRequired();
        modelBuilder.Entity<Pedido>().Property(p => p.EstadoAtual).IsRequired();
        modelBuilder.Entity<Pedido>().Property(p => p.TipoFrete).IsRequired();
    }
}