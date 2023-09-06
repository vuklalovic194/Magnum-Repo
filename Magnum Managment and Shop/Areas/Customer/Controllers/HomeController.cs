using Magnum_Managment_and_Shop.Data;
using Magnum_Managment_and_Shop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Magnum_Managment_and_Shop.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index (string searchString)
        {
				return View();
		}

		//GET
		public async Task<IActionResult> Shop(string searchString)
        {
            List<Product> products = _db.Products.ToList();

            var productFromDb = from db in _db.Products select db;
			if (!string.IsNullOrEmpty(searchString))
			{
				productFromDb = _db.Products.Where(m => m.Title.Contains(searchString));
				return View(await productFromDb.ToListAsync());
			}

			return View(products);
        }

        public IActionResult CaL()
        {
            return View();
        }

        public IActionResult About()
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