using Microsoft.AspNetCore.Mvc.Rendering;

namespace Magnum_Managment_and_Shop.Models.ViewModels
{
	public class ProductVM
	{
		public Product Product { get; set; }
		public IEnumerable<Product>? Products { get; set;}
		public IEnumerable<SelectListItem>? productList { get; set; }
	}
}
