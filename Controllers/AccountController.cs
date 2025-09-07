using Microsoft.AspNetCore.Mvc;
using BubbleTeaCafe.Models;
using BubbleTeaCafe.Data;
using System.Security.Cryptography;
using System.Text;

namespace BubbleTeaCafe.Controllers
{
    public class AccountController : Controller
    {
        private readonly BubbleTeaCafeContext _context;

        public AccountController(BubbleTeaCafeContext context)
        {
            _context = context;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _context.Users.FirstOrDefault(u => u.Email == model.Email);
                if (user != null && VerifyPassword(model.Password, user.PasswordHash))
                {
                    // Đăng nhập thành công
                    TempData["Success"] = "Đăng nhập thành công!";
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Email hoặc mật khẩu không đúng");
            }
            return View(model);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Kiểm tra email đã tồn tại
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError("Email", "Email này đã được sử dụng");
                    return View(model);
                }

                // Tạo user mới
                var user = new User
                {
                    FullName = model.FullName,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber,
                    PasswordHash = HashPassword(model.Password),
                    CreatedAt = DateTime.Now,
                    IsActive = true
                };

                _context.Users.Add(user);
                _context.SaveChanges();

                TempData["Success"] = "Đăng ký thành công! Vui lòng đăng nhập.";
                return RedirectToAction("Login");
            }
            return View(model);
        }

        public IActionResult Logout()
        {
            TempData["Success"] = "Đăng xuất thành công!";
            return RedirectToAction("Index", "Home");
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string hash)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == hash;
        }
    }
}
