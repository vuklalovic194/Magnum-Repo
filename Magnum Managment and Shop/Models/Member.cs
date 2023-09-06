using System.ComponentModel.DataAnnotations;

namespace Magnum_Managment_and_Shop.Models
{
	public class Member
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public int Age { get; set; }
		[Required]
		public string Rank { get; set; }
		[Required]
		public int Sessions { get; set; }
		
		public string? Address { get; set; }

		public string? Phone { get; set; }

		public string? AdditionalInformations { get; set; }

		public string? ImageUrl { get; set; }
	}
}
