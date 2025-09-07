using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BubbleTeaCafe.Models;

public class Order
{
    public int OrderId { get; set; }
    
    [Required]
    [StringLength(100)]
    public string CustomerName { get; set; } = string.Empty;
    
    [Required]
    [StringLength(100)]
    [EmailAddress]
    public string CustomerEmail { get; set; } = string.Empty;
    
    [Required]
    [StringLength(20)]
    public string CustomerPhone { get; set; } = string.Empty;
    
    [StringLength(200)]
    public string? DeliveryAddress { get; set; }
    
    public DateTime OrderDate { get; set; } = DateTime.Now;
    
    [Column(TypeName = "decimal(18,2)")]
    public decimal TotalAmount { get; set; }
    
    [StringLength(20)]
    public string Status { get; set; } = "Pending"; // Pending, Preparing, Ready, Delivered, Cancelled
    
    [StringLength(20)]
    public string OrderType { get; set; } = "Pickup"; // Pickup, Delivery
    
    [StringLength(500)]
    public string? Notes { get; set; }
    
    // Navigation property
    public List<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
