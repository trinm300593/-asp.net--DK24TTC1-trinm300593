using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BubbleTeaCafe.Models;

public class Product
{
    public int ProductId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;
    
    [StringLength(500)]
    public string? Description { get; set; }
    
    [Required]
    [Column(TypeName = "decimal(18,2)")]
    public decimal Price { get; set; }
    
    [StringLength(200)]
    public string? ImageUrl { get; set; }
    
    public bool IsAvailable { get; set; } = true;
    
    [StringLength(200)]
    public string? Ingredients { get; set; }
    
    // Size options (Small, Medium, Large)
    [StringLength(50)]
    public string? SizeOptions { get; set; }
    
    // Temperature options (Hot, Cold, Iced)
    [StringLength(50)]
    public string? TemperatureOptions { get; set; }
    
    // Sweetness level (0%, 25%, 50%, 75%, 100%)
    [StringLength(50)]
    public string? SweetnessOptions { get; set; }
    
    // Foreign key
    public int CategoryId { get; set; }
    
    // Navigation property
    public Category Category { get; set; } = null!;
}
