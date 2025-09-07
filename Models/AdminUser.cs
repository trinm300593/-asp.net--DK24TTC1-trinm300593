using System.ComponentModel.DataAnnotations;

namespace BubbleTeaCafe.Models
{
    public class AdminUser
    {
        public int AdminUserId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Username { get; set; } = string.Empty;
        
        [Required]
        [StringLength(100)]
        public string FullName { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        [Required]
        public string Role { get; set; } = "Admin"; // Admin, SuperAdmin
        
        public bool IsActive { get; set; } = true;
        
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        public DateTime? LastLoginAt { get; set; }
        
        public string? Avatar { get; set; }
        
        public string? Phone { get; set; }
    }
    
    public class AdminLoginViewModel
    {
        [Required(ErrorMessage = "Vui lòng nhập tên đăng nhập")]
        public string Username { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
        
        public bool RememberMe { get; set; }
    }
    
    public class AdminDashboardViewModel
    {
        public int TotalProducts { get; set; }
        public int TotalOrders { get; set; }
        public int TotalUsers { get; set; }
        public decimal TotalRevenue { get; set; }
        public int TodayOrders { get; set; }
        public decimal TodayRevenue { get; set; }
        public List<RecentOrder> RecentOrders { get; set; } = new();
        public List<TopProduct> TopProducts { get; set; } = new();
    }
    
    public class RecentOrder
    {
        public string OrderId { get; set; } = string.Empty;
        public string CustomerName { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
    }
    
    public class TopProduct
    {
        public string ProductName { get; set; } = string.Empty;
        public int SoldCount { get; set; }
        public decimal Revenue { get; set; }
    }
}
