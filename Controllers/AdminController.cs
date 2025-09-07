using Microsoft.AspNetCore.Mvc;
using BubbleTeaCafe.Models;

namespace BubbleTeaCafe.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin/Login
        public IActionResult Login()
        {
            // Check if already logged in
            if (HttpContext.Session.GetString("AdminLoggedIn") == "true")
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        // POST: Admin/Login
        [HttpPost]
        public IActionResult Login(AdminLoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Simple authentication (in real app, use proper authentication)
                if (model.Username == "admin" && model.Password == "admin123")
                {
                    HttpContext.Session.SetString("AdminLoggedIn", "true");
                    HttpContext.Session.SetString("AdminUsername", model.Username);

                    return RedirectToAction("Index");
                }
                else
                {
                    ViewData["ErrorMessage"] = "Tên đăng nhập hoặc mật khẩu không đúng!";
                }
            }

            return View(model);
        }

        // GET: Admin/Logout
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        // Check authentication for all admin actions
        private bool IsAuthenticated()
        {
            return HttpContext.Session.GetString("AdminLoggedIn") == "true";
        }

        public IActionResult Index()
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            var model = new AdminDashboardViewModel
            {
                TotalProducts = 156,
                TotalOrders = 89,
                TotalUsers = 1234,
                TotalRevenue = 2400000,
                TodayOrders = 89,
                TodayRevenue = 2400000
            };

            return View(model);
        }

        public IActionResult Products()
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            return View();
        }

        public IActionResult Orders()
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            return View();
        }

        public IActionResult Users()
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            return View();
        }

        public IActionResult Settings()
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            return View();
        }

        public IActionResult Categories()
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            return View();
        }

        public IActionResult Reports()
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            return View();
        }

        public IActionResult Test()
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            return View();
        }

        public IActionResult AddProduct()
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            return View();
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                // Xử lý thêm sản phẩm ở đây
                return RedirectToAction("Products");
            }
            return View(product);
        }

        public IActionResult EditProduct(int id)
        {
            if (!IsAuthenticated())
                return RedirectToAction("Login");

            // Tìm sản phẩm theo id
            var product = new Product { ProductId = id, Name = "Sample Product" };
            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(Product product)
        {
            if (ModelState.IsValid)
            {
                // Xử lý cập nhật sản phẩm ở đây
                return RedirectToAction("Products");
            }
            return View(product);
        }

        public IActionResult DeleteProduct(int id)
        {
            // Xử lý xóa sản phẩm ở đây
            return RedirectToAction("Products");
        }
    }
}
