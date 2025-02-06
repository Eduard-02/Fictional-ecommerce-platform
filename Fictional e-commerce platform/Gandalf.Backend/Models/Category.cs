using System.ComponentModel.DataAnnotations;

namespace Gandalf.Backend.Models;

public class Category
{
    [Key]
    public int CategoryId { get; set; }

    [Required]
    public string Name { get; set; }
    public ICollection<Product> Products { get; set; } = new List<Product>();
}
