using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace Magnum_Managment_and_Shop.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }
        
        [ForeignKey(nameof(CategoryId))]
        [ValidateNever]
        public Category? Category { get; set; }
		public int CategoryId { get; set; }


	}
}
