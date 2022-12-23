using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HondaVN.Data;
using HondaVN.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HondaVN.Controllers
{

    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Home
        public async Task<IActionResult> Index()
        {
            return View(await _context.Product.ToListAsync());
        }
        [HttpGet]
        public async Task<IActionResult> Index(string searchString, string currentFilter)
        {
                    
            ViewData["CurrentFilter"] = searchString;

            var products = from m in _context.Product
                           select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                //searchString == null co the them vao
                products = products.Where(s => s.Name.Contains(searchString) || s.Manufacturer.Contains(searchString));
            }
            return View(await products.AsNoTracking().ToListAsync());

        }        

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsoncart = session.GetString("shopcart");
            if (jsoncart != null)
            {
                return JsonConvert.DeserializeObject<List<CartItem>>(jsoncart);

            }
            return new List<CartItem>();
        }
        void SaveCartSession(List<CartItem> lst)
        {
            var session = HttpContext.Session;
            string jsoncart = JsonConvert.SerializeObject(lst);
            session.SetString("shopcart", jsoncart);
        }
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove("shopcart");
        }
        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _context.Product
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound("Sản phẩm không tồn tại!");
            }
            //Xử lý cho hàng vào giỏ
            var cart = GetCartItems();
            var caritem = cart.Find(p => p.Product.Id == id);
            if (caritem != null) // mặt hàng đã có trong giỏ tăng số lượng 
            {
                caritem.Quantity++;

            }
            else
            {
                cart.Add(new CartItem() { Quantity = 1, Product = product });
            }
            SaveCartSession(cart);//Lưu vào session
            return RedirectToAction(nameof(Cart));//Chuyển đến trang giỏ hàng
        }
        public IActionResult Cart()
        {
            return View(GetCartItems());
        }

        // GET: Home/Create
        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
        public IActionResult RemoveCart(int id)
        {

            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Product.Id == id);

            if (cartitem != null) // mặt hàng đã có trong giỏ tăng số lượng 
            {
                cart.Remove(cartitem);

            }
            SaveCartSession(cart);//Lưu vào session
            return RedirectToAction(nameof(Cart));//Chuyển đến trang giỏ hàng
        }
        public IActionResult UpdateCart(int id, int quantity)
        {
            var cart = GetCartItems();
            var cartitem = cart.Find(p => p.Product.Id == id);

            if (cartitem != null) // mặt hàng đã có trong giỏ tăng số lượng 
            {
                cartitem.Quantity = quantity;

            }
            SaveCartSession(cart);//Lưu vào session
            return RedirectToAction(nameof(Cart));//Chuyển đến trang giỏ hàng

        }
        public IActionResult DeleteAll()
        {
            ClearCart();
            return RedirectToAction(nameof(Cart));//Chuyển đến trang giỏ hàng
        }
        [Route("checkout.html")]
        public IActionResult Checkout()
        {
            return View(GetCartItems());
        }
        // Lập hóa đơn: lưu hóa đơn, lưu chi tiết hóa đơn
        [HttpPost, ActionName("CreateBill")]
        public async Task<IActionResult> CreateBill(string cusName, string cusPhone, string cusAddress, int billTotal)
        {
            var bill = new Bill();
            bill.Date = DateTime.Now;
            bill.CustomerName = cusName;
            bill.CustomerPhone = cusPhone;
            bill.CustomerAddress = cusAddress;
            // cập nhật tổng tiền hóa đơn ?
            bill.BillTotal = billTotal;

            _context.Add(bill);
            await _context.SaveChangesAsync();

            // thêm chi tiết hóa đơn
            var cart = GetCartItems();

            int amount = 0;
            int total = 0;
            //Lưu hết chi tiết hóa đơn
            foreach (var i in cart)
            {
                var b = new BillDetail();
                b.BillId = bill.BillId;
                b.ProductId = i.Product.Id;
                amount = i.Product.Price * i.Quantity;
                total += amount;
                b.Price = i.Product.Price;
                b.Quantity = i.Quantity;
                b.Amount = amount;
                _context.Add(b);

            }
            await _context.SaveChangesAsync();
            //Xóa giỏ 
            ClearCart();
            return RedirectToAction(nameof(Thank));
        }
        public IActionResult Thank()
        {
            return View();
        }
        //public async Task<IActionResult> Thank()
        //{
        //    var applicationDbContext = _context.Bill.Include(b => b.BillDetails);
        //    return View(await applicationDbContext.ToListAsync());
        //}
    }
}
