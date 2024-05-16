using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using slot_4.Models;

namespace slot_4.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly MySaleDBContext _context = new MySaleDBContext();

        [HttpGet]
        public IActionResult Get()
        {
            var categories = _context.Categories.Select(c => new
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
            }).ToList();
            return Ok(categories);

        }

        // api/get/1



        // Báo not found nếu không có
        [HttpGet("id")]
        public IActionResult Get(int id)
        {
            var category = _context.Categories.Select(c => new
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
            }).FirstOrDefault(s => s.CategoryId == id);
            if (category == null) return NotFound("Product is not existed");
            return Ok(category);

        }

        [HttpPost]
        public IActionResult Post(Category cat)
        {
            var c = _context.Categories.FirstOrDefault(s => s.CategoryName == cat.CategoryName);
            if (c != null) return BadRequest("Existed Category");
            if (cat.CategoryName == "") return BadRequest("Not Empty Name");
            _context.Categories.Add(cat);
            _context.SaveChanges();
            return Created("Created Successfully", cat);
        }

        // api/post/category



        // check name có empty

        [HttpPut]
        public IActionResult Put(Category cat)
        {
            var c = _context.Categories.FirstOrDefault(s => s.CategoryName == cat.CategoryName);

            if (c == null) return NotFound("No Matched Category");
            c.CategoryName = cat.CategoryName;
            _context.SaveChanges();
            return Ok(c);
        }


        // api/put/category
        // check exists

        // api/delete/id

        [HttpDelete("id")]
        public IActionResult Delete(int id)
        {
            var c = _context.Categories
                .Include(p => p.Products)
                .FirstOrDefault(s => s.CategoryId == id);

            if (c == null) return NotFound("No Matched Delete Category");
            if (c.Products.Count > 0) return BadRequest("There's product existed in the category");
            _context.Categories.Remove(c);
            _context.SaveChanges();
            return Ok(c);
        }

        // check products exist, check categories exists


    }
}
