using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BubbleTeaCafe.Data;
using BubbleTeaCafe.Models;
using System.Linq;
using System.Threading.Tasks;

namespace BubbleTeaCafe.Controllers;

public class ProductController : Controller
{
    private readonly BubbleTeaCafeContext _context;

    public ProductController(BubbleTeaCafeContext context)
    {
        _context = context;
    }

    // GET: Product
    public async Task<IActionResult> Index()
    {
        var products = await _context.Products
            .Include(p => p.Category)
            .ToListAsync();
        
        return View(products);
    }

    // GET: Product/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var product = await _context.Products
            .Include(p => p.Category)
            .FirstOrDefaultAsync(m => m.ProductId == id);
            
        if (product == null)
        {
            return NotFound();
        }

        return View(product);
    }

    // GET: Product/Category/1
    public async Task<IActionResult> Category(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var category = await _context.Categories
            .FirstOrDefaultAsync(c => c.CategoryId == id);
            
        if (category == null)
        {
            return NotFound();
        }

        var products = await _context.Products
            .Where(p => p.CategoryId == id)
            .Include(p => p.Category)
            .ToListAsync();

        ViewBag.CategoryName = category.Name;
        ViewBag.CategoryDescription = category.Description;
        
        return View(products);
    }

    // GET: Product/Menu
    public async Task<IActionResult> Menu()
    {
        var categories = await _context.Categories
            .Include(c => c.Products)
            .ToListAsync();
            
        return View(categories);
    }
}
