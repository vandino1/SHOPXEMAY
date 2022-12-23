using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HondaVN.Data;
using HondaVN.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using PagedList.Mvc;
using PagedList;
using ClosedXML.Excel;

namespace HondaVN.Controllers
{
  // [Authorize(Roles = "Admin,Editor")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductService productService = new ProductService();

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
          public async Task<IActionResult> Index()
          {        
            return View(await _context.Product.ToListAsync());
          }

        //Export Excel
        public IActionResult DowloadExcel()
        {
            var products = productService.GetProducts();
            string contentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            string fileName = "DSXeMay.xlsx";
            using (var workbook = new XLWorkbook())
            {
                IXLWorksheet worksheet = workbook.Worksheets.Add("Products");
                worksheet.Cell(1, 1).Value = "Id";
                worksheet.Cell(1, 2).Value = "Tên xe";
                worksheet.Cell(1, 3).Value = "Hãng sản xuất";
                worksheet.Cell(1, 4).Value = "Giá";
                worksheet.Cell(1, 5).Value = "Số lượng";
                worksheet.Cell(1, 6).Value = "Mô tả";
                worksheet.Cell(1, 7).Value = "Hình ảnh";
                for (int index = 1; index <= products.Count; index++)
                {
                    worksheet.Cell(index + 1, 1).Value = products[index - 1].Id;
                    worksheet.Cell(index + 1, 2).Value = products[index - 1].Name;
                    worksheet.Cell(index + 1, 3).Value = products[index - 1].Manufacturer;
                    worksheet.Cell(index + 1, 4).Value = products[index - 1].Price;
                    worksheet.Cell(index + 1, 5).Value = products[index - 1].Quantity;
                    worksheet.Cell(index + 1, 6).Value = products[index - 1].Desciption;
                    worksheet.Cell(index + 1, 7).Value = products[index - 1].Image;
                }

                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return File(content, contentType, fileName);
                }

            }
        }
        [HttpGet]
        //Sắp xếp và phân trang sản phẩm
         public async Task<IActionResult> Index(string searchString, string sortOrder, string currentFilter, int? pageNumber) 
         {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["ManafuturerSortParm"] = sortOrder == "manafuturer_asc" ? "manafuturer_desc" : "manafuturer_asc";
            ViewData["SortbyPrice"] = sortOrder == "price_asc" ? "price_desc" : "price_asc";
            ViewData["SortbyQuantity"] = sortOrder == "quantity_asc" ? "quantity_desc" : "quantity_asc";
            
            //ViewData["Getproductdetails"] = searchString;

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            var products = from m in _context.Product
                            select m;

             if (!String.IsNullOrEmpty(searchString))
             {
                //searchString == null co the them vao
                products = products.Where(s => s.Name.Contains(searchString) || s.Manufacturer.Contains(searchString));
             }
            
            switch (sortOrder)
            {
                case "name_desc":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "manafuturer_desc":
                    products = products.OrderByDescending(s => s.Manufacturer);
                    break;
                case "manafuturer_asc":
                    products = products.OrderBy(s => s.Manufacturer);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(s => s.Price);
                    break;
                case "price_asc":
                    products = products.OrderBy(s => s.Price);
                    break;
                case "quantity_desc":
                    products = products.OrderByDescending(s => s.Quantity);
                    break;
                case "quantity_asc":
                    products = products.OrderBy(s => s.Quantity);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 4;
            return View(await PaginatedList<Product>.CreateAsync(products.AsNoTracking(), pageNumber ?? 1, pageSize));
        }
         
        // GET: Products/Details/5
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
               
        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile file, [Bind("Id,Name,Manufacturer,Price,Quantity,Desciption,Image")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Image = Upload(file);
                _context.Add(product);
                await _context.SaveChangesAsync();
                TempData["AlertMessage"] = "Vừa thêm sản phẩm thành công.";
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5       
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile file, int id, [Bind("Id,Name,Manufacturer,Price,Quantity,Desciption,Image")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //Tự save ảnh
                    if (file != null)
                    {
                        product.Image = Upload(file);
                    }
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                    TempData["AlertMessage"] = "Vừa cập nhật sản phẩm thành công.";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5      
        public async Task<IActionResult> Delete(int? id)
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

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            TempData["AlertMessage"] = "Đã xóa sản phẩm.";
            return RedirectToAction(nameof(Index));
        }
        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
        public string Upload(IFormFile file)
        {
            string fn = null;

            if (file != null)
            {
                //Phát sinh tên
                fn = Guid.NewGuid().ToString() + "_" + file.FileName;
                //Chep file về đúng thư mục
                var path = $"wwwroot\\images\\{fn}";
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
            }
            return fn;
        }
    }
}
