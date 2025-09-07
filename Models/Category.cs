using System.ComponentModel.DataAnnotations;

namespace BubbleTeaCafe.Models;

public class Category
{
    public int CategoryId { get; set; }
    
    [Required]
    [StringLength(50)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(200)]
    public string? Description { get; set; }
    
    // Navigation property
    public List<Product> Products { get; set; } = new List<Product>();
}
