// Data/StudentContext.cs
using Microsoft.EntityFrameworkCore;
using WEBAPI.Models;

public class WebApiContext : DbContext
{
    public WebApiContext(DbContextOptions<WebApiContext> options)
        : base(options)
    {
    }

    public DbSet<Produtos> Produtos { get; set; }
    public DbSet<Categorias> Categorias { get; set; }
}
