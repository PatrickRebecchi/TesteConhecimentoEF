using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WEBAPI.Models;

public class Produtos
{
    [Key]
    public int IdProduto { get; set; }
    [Required]
    public string? Nome { get; set; }
    [Required]
    public int QuantidadeEstoque { get; set; }

    public int? IdCategoria { get; set; }

    [ForeignKey(nameof(IdCategoria))]
    public Categorias? Categorias { get; set; }
}