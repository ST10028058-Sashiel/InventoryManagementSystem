using Microsoft.AspNetCore.Mvc;
using System.Linq;
using InventoryManagementSystem.Data;
using InventoryManagementSystem.Models;

namespace InventoryManagementSystem.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.ProductID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _context.Update(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/ProcessSale/5
        public IActionResult ProcessSale(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null || product.StockQuantity <= 0)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/ProcessSale
        [HttpPost]
        public IActionResult ProcessSale(int id, int quantity)
        {
            var product = _context.Products.Find(id);
            if (product == null || product.StockQuantity < quantity)
            {
                return BadRequest("Insufficient stock.");
            }

            product -= quantity;
            _context.Update(product);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // GET: Products/Report
        public IActionResult Report()
        {
            var report = _context.Products
                .Select(p => new { p.Name, p.Category, p.StockQuantity })
                .ToList();
            return View(report);
        }
    }
}
