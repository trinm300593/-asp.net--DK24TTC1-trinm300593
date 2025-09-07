using System.ComponentModel.DataAnnotations;

namespace BubbleTeaCafe.Models.ViewModels;

public class CartViewModel
{
    public List<CartItem> Items { get; set; } = new List<CartItem>();
    
    public decimal TotalAmount => Items.Sum(item => item.TotalPrice);
    
    public int TotalItems => Items.Sum(item => item.Quantity);
}

public class AddToCartViewModel
{
    public int ProductId { get; set; }
    
    [Required]
    [Range(1, 99, ErrorMessage = "Số lượng phải từ 1 đến 99")]
    public int Quantity { get; set; } = 1;
    
    [StringLength(20)]
    public string? Size { get; set; }
    
    [StringLength(20)]
    public string? Temperature { get; set; }
    
    [StringLength(20)]
    public string? Sweetness { get; set; }
    
    [StringLength(200)]
    public string? SpecialInstructions { get; set; }
}
