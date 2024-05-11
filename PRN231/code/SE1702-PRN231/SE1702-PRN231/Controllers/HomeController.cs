using Microsoft.AspNetCore.Mvc;
using SE1702_PRN231.Models;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;

namespace SE1702_PRN231.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly MySaleDBContext dbContext;

        public List<Product> Products { get; set; }

        public Product CurrentProduct { get; set; }

        public int PickCategory { get; set; }

        public List<Category> Categories { get; set; }

        public List<string> ColumnName { get; set; }

        public HomeController(ILogger<HomeController> logger, MySaleDBContext mySaleDbContext)
        {
            _logger = logger;
            dbContext = mySaleDbContext;
            ColumnName = new List<string>();
        }


        
        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            if (!(product.ProductName.Length == 0 || 
                product.Image.Length == 0 || product.UnitPrice is not decimal || product.UnitsInStock is not int || product.CategoryId == 0))
                
            {
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
            }
            
            
            return RedirectToAction("List");
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product product)
        {
            if (!(product.ProductName.Length == 0 ||
                product.Image.Length == 0 || product.UnitPrice is not decimal || product.UnitsInStock is not int || product.CategoryId == 0))

            {
                var updateProduct = dbContext.Products.FirstOrDefault(p => p.ProductId == product.ProductId);
                if (updateProduct != null)
                {
                    updateProduct.ProductName = product.ProductName;
                    updateProduct.CategoryId = product.CategoryId;
                    updateProduct.Image = product.Image;
                    updateProduct.UnitPrice = product.UnitPrice;
                    updateProduct.UnitsInStock = product.UnitsInStock;
                }
                dbContext.SaveChanges();
            }
            return RedirectToAction("List");
        }
        [HttpPost]
        public IActionResult DeleteProduct(int deleteId)
        {
            var product = dbContext.Products.FirstOrDefault(p => p.ProductId == deleteId);
            dbContext.Products.Remove(product);
            dbContext.SaveChanges();
            return RedirectToAction("List");
        }

        public void Load(int? categoryId)
        {
            if (categoryId != 0 && categoryId != null)
            {
                Products = dbContext.Products.Include(p => p.Category).
                    Where(p => p.CategoryId == categoryId).ToList();
            }
            else
            {
                Products = dbContext.Products.Include(p => p.Category).ToList();
            }
            Categories = dbContext.Categories.ToList();

        }

        public IActionResult List(int? pr, int categoryId = 0)
        {
            
            Load(categoryId);
            Console.WriteLine();
            if (pr != null && pr != 0)
            {
                ViewBag.productId = pr;
                CurrentProduct = dbContext.Products.Include(p => p.Category).Single(p => p.ProductId == pr);
                ViewBag.CurrentProduct = CurrentProduct;

            }
            ViewBag.PickCategory = categoryId;
            ViewBag.Categories = Categories;
            return View(Products);
        }

        
        

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
