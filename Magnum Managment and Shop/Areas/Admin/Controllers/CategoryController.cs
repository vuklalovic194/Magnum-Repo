using Magnum_Managment_and_Shop.Data;
using Magnum_Managment_and_Shop.Models;
using Microsoft.AspNetCore.Mvc;

namespace Magnum_Managment_and_Shop.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CategoryController : Controller
	{
		private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        
		public IActionResult Index()
		{
			List<Category> category = _db.Categories.ToList(); 
			return View(category);
		}

		public IActionResult Create()
		{
			Category category = new Category();
			return View(category);
		}

		[HttpPost]
		public IActionResult Create(Category category)
		{
			_db.Categories.Add(category);
			_db.SaveChanges();
			return RedirectToAction("Index");
		}

		

		#region APICALLS

		[HttpGet]
		public IActionResult GetAll() 
		{
			List<Category> category = _db.Categories.ToList();

			return Json(new {data = category});
		}

		//[HttpDelete]
		public IActionResult Delete(int? categoryId)
		{
			Category categoryToDelete= _db.Categories.FirstOrDefault(m => categoryId == m.Id);
			

			if (categoryToDelete == null)
			{
				return Json(new { data = false, message = "Error while deleting" });
			}

			_db.Categories.Remove(categoryToDelete);
			_db.SaveChanges();

			return RedirectToAction(nameof(Index));
		}

		#endregion
	}
}
