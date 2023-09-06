using Magnum_Managment_and_Shop.Data;
using Magnum_Managment_and_Shop.Models;
using Magnum_Managment_and_Shop.Models.ViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Magnum_Managment_and_Shop.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ShopManagementController : Controller
	{
		private readonly ApplicationDbContext _db;
		private readonly IWebHostEnvironment _webHostEnvironment;

		public ShopManagementController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
		{
			_db = db;
			_webHostEnvironment = webHostEnvironment;
		}

		//GET
		public async Task<IActionResult> Index (string searchString)
		{
			var objFromDb = from m in _db.Products select m;

			if (!string.IsNullOrEmpty(searchString))
			{
				objFromDb = _db.Products.Where(m => m.Title.Contains(searchString));
				return View(await objFromDb.ToListAsync()); 			
			}

			List<Product> productsFromDb = _db.Products.ToList();
			return View(productsFromDb);
		}


		//GET
		[HttpGet]
		public IActionResult Create()
		{
			ProductVM productVM = new()
			{
				productList = _db.Categories.Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				Product = new Product()
			};
			return View(productVM);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Create(ProductVM productVM, IFormFile? file)
		{

			if (ModelState.IsValid)
			{
				string wwwRootPath = _webHostEnvironment.WebRootPath;

				if (file != null)
				{
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string productPath = Path.Combine(wwwRootPath, @"Images\Products");

					

					using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}

					if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
					{
						var oldImagetPath = Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('\\'));
						if (System.IO.File.Exists(oldImagetPath))
						{
							System.IO.File.Delete(oldImagetPath);
						}
					}

					productVM.Product.ImageUrl = @"\Images\Products\" + fileName;
				}

				_db.Products.Add(productVM.Product);
				TempData["success"] = "Product created successfuly!";
				_db.SaveChanges();
				return RedirectToAction(nameof(Index));
			}

			return View(productVM);
		}

		//GET
		public IActionResult Edit(int? productId)
		{
			ProductVM productVM = new()
			{
				productList = _db.Categories.Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				Product = _db.Products.FirstOrDefault(m => m.Id == productId)
			};
			
			return View(productVM);
		}

		[HttpPost]
		public IActionResult Edit(Product product, IFormFile? file)
		{
			string wwwRootPath = _webHostEnvironment.WebRootPath;

			if (file != null)
			{
				string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
				string productPath = Path.Combine(wwwRootPath, @"Images\Member");

				if (!string.IsNullOrEmpty(product.ImageUrl))
				{
					var oldImage = Path.Combine(wwwRootPath, product.ImageUrl.TrimStart('\\'));

					if (System.IO.File.Exists(oldImage))
					{
						System.IO.File.Delete(oldImage);
					}
				}

				using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
				{
					file.CopyTo(fileStream);
				}

				product.ImageUrl = @"\Images\Member\" + fileName;
			}

			_db.Products.Update(product);
			TempData["success"] = "Product updated successfuly!";
			_db.SaveChanges();
			return RedirectToAction(nameof(Index));
		}

		//GET
		public IActionResult Delete(int? productId)
		{
			ProductVM productVM = new()
			{
				productList = _db.Categories.Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				Product = _db.Products.FirstOrDefault(m => m.Id == productId)
			};
			return View(productVM);
		}

		[HttpPost]
		[ActionName(name: "Delete")]
		public IActionResult DeletePOST(int? productId)
		{
			ProductVM productVM = new()
			{
				productList = _db.Categories.Select(u => new SelectListItem
				{
					Text = u.Name,
					Value = u.Id.ToString()
				}),
				Product = _db.Products.FirstOrDefault(m => m.Id == productId)
			};

			_db.Products.Remove(productVM.Product);
			TempData["success"] = "Product deleted successfuly!";
			_db.SaveChanges();

			return RedirectToAction(nameof(Index));
		}

	}
}
