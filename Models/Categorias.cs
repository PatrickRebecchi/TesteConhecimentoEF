using System.ComponentModel.DataAnnotations;

namespace WEBAPI.Models;

public class Categorias

{
    public Categorias()
    {
        
    }

    [Key]
    public int IdCategoria { get; set; }
    [Required]
    public string? NomeCategoria { get; set; }
}