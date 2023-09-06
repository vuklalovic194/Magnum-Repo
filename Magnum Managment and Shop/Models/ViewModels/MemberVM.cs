using Magnum_Managment_and_Shop.Models;

namespace Magnum_Managment_and_Shop.Models.ViewModels
{
    public class MemberVM
    {
        public Member Member { get; set; }
        public IEnumerable<Member> Members { get; set; }
    }
}
