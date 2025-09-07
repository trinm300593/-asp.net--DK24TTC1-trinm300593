using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BubbleTeaCafe.Models;

public class OrderItem
{
    public int OrderItemId { get; set; }
    
    public int OrderId { get; set; }
    
    public int ProductId { get; set; }
    
    [Required]
    [Range(1, 99)]
    public int Quantity { get; set; }
    
    [StringLength(20)]
    public string? Size { get; set; }
    
    [StringLength(20)]
    public string? Temperature { get; set; }
    
    [StringLength(20)]
    public string? Sweetness { get; set; }
    
    [StringLength(200)]
    public string? SpecialInstructions { get; set; }
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal UnitPrice { get; set; }
    
    [NotMapped]
    public decimal TotalPrice => UnitPrice * Quantity;
    
    // Navigation properties
    public Order Order { get; set; } = null!;
    public Product Product { get; set; } = null!;
}
