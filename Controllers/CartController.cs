using Microsoft.AspNetCore.Mvc;
using BubbleTeaCafe.Data;
using BubbleTeaCafe.Models;
using BubbleTeaCafe.Models.ViewModels;
using BubbleTeaCafe.Services;
using Microsoft.EntityFrameworkCore;

namespace BubbleTeaCafe.Controllers;

public class CartController : Controller
{
    private readonly BubbleTeaCafeContext _context;
    private readonly ICartService _cartService;

    public CartController(BubbleTeaCafeContext context, ICartService cartService)
    {
        _context = context;
        _cartService = cartService;
    }

    // GET: Cart
    public IActionResult Index()
    {
        var cart = _cartService.GetCart(HttpContext.Session);
        
        // Load product details for each cart item
        foreach (var item in cart.Items)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
            if (product != null)
            {
                item.Product = product;
            }
        }
        
        return View(cart);
    }

    // POST: Cart/AddToCart
    [HttpPost]
    public async Task<IActionResult> AddToCart(AddToCartViewModel model)
    {
        if (ModelState.IsValid)
        {
            var product = await _context.Products.FindAsync(model.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            var cartItem = new CartItem
            {
                ProductId = model.ProductId,
                Quantity = model.Quantity,
                Size = model.Size,
                Temperature = model.Temperature,
                Sweetness = model.Sweetness,
                SpecialInstructions = model.SpecialInstructions,
                UnitPrice = product.Price,
                Product = product
            };

            _cartService.AddToCart(HttpContext.Session, cartItem);
            
            return RedirectToAction(nameof(Index));
        }
        
        return RedirectToAction("Details", "Product", new { id = model.ProductId });
    }

    // POST: Cart/RemoveFromCart
    [HttpPost]
    public IActionResult RemoveFromCart(int cartItemId)
    {
        _cartService.RemoveFromCart(HttpContext.Session, cartItemId);
        return RedirectToAction(nameof(Index));
    }

    // POST: Cart/UpdateQuantity
    [HttpPost]
    public IActionResult UpdateQuantity(int cartItemId, int quantity)
    {
        if (quantity > 0)
        {
            _cartService.UpdateQuantity(HttpContext.Session, cartItemId, quantity);
        }
        return RedirectToAction(nameof(Index));
    }

    // POST: Cart/ClearCart
    [HttpPost]
    public IActionResult ClearCart()
    {
        _cartService.ClearCart(HttpContext.Session);
        return RedirectToAction(nameof(Index));
    }

    // GET: Cart/Checkout
    public IActionResult Checkout()
    {
        var cart = _cartService.GetCart(HttpContext.Session);
        if (!cart.Items.Any())
        {
            return RedirectToAction(nameof(Index));
        }

        // Load product details
        foreach (var item in cart.Items)
        {
            var product = _context.Products.FirstOrDefault(p => p.ProductId == item.ProductId);
            if (product != null)
            {
                item.Product = product;
            }
        }

        return View(cart);
    }

    // POST: Cart/PlaceOrder
    [HttpPost]
    public async Task<IActionResult> PlaceOrder(string customerName, string customerEmail, string customerPhone, string deliveryAddress, string orderType, string notes)
    {
        var cart = _cartService.GetCart(HttpContext.Session);
        if (!cart.Items.Any())
        {
            return RedirectToAction(nameof(Index));
        }

        // Tạo đơn hàng mới
        var order = new Order
        {
            CustomerName = customerName,
            CustomerEmail = customerEmail,
            CustomerPhone = customerPhone,
            DeliveryAddress = deliveryAddress,
            OrderType = orderType,
            Notes = notes,
            OrderDate = DateTime.Now,
            TotalAmount = cart.TotalAmount,
            Status = "Pending"
        };

        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        // Thêm các item vào đơn hàng
        foreach (var item in cart.Items)
        {
            var orderItem = new OrderItem
            {
                OrderId = order.OrderId,
                ProductId = item.ProductId,
                Quantity = item.Quantity,
                Size = item.Size,
                Temperature = item.Temperature,
                Sweetness = item.Sweetness,
                SpecialInstructions = item.SpecialInstructions,
                UnitPrice = item.UnitPrice
            };
            _context.OrderItems.Add(orderItem);
        }

        await _context.SaveChangesAsync();

        // Xóa giỏ hàng
        _cartService.ClearCart(HttpContext.Session);

        TempData["Success"] = "Đặt hàng thành công! Chúng tôi sẽ liên hệ với bạn sớm nhất.";
        return RedirectToAction("OrderSuccess", new { orderId = order.OrderId });
    }

    // GET: Cart/OrderSuccess
    public async Task<IActionResult> OrderSuccess(int orderId)
    {
        var order = await _context.Orders
            .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
            .FirstOrDefaultAsync(o => o.OrderId == orderId);

        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }
}
