using Magnum_Managment_and_Shop.Data;
using Magnum_Managment_and_Shop.Models;
using Magnum_Managment_and_Shop.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Magnum_Managment_and_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MemberController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ApplicationDbContext _db;
        public MemberController(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _db = db;
        }



        //GET
        public async Task<IActionResult> Index(string searchString)
        {
            if (_db.Members == null)
            {
                return Problem("No members found");
            }

            var membersFromDb = from m in _db.Members select m;


            if (!string.IsNullOrEmpty(searchString))
            {
                membersFromDb = _db.Members.Where(m => m.Name.Contains(searchString));
                return View(await membersFromDb.ToListAsync());
            }

            

            IEnumerable<Member> members = _db.Members.ToList();
            return View(members);
        }


        //GET
        public IActionResult Upsert(int? id)
        {

            Member member = new Member();

            if (id == 0)
            {
                return View();
            }
            else
            {
                Member memberFromDb = _db.Members.FirstOrDefault(m => m.Id == id);
                return View(memberFromDb);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Member member, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"Images\Member");

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                member.ImageUrl = @"\Images\Member\" + fileName;
            }

            _db.Members.Add(member);
            TempData["success"] = "Member created successfuly!";
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        //GET
        public IActionResult Edit(int id)
        {
            Member memberFromDb = _db.Members.FirstOrDefault(m => id == m.Id);


            return View(memberFromDb);
        }

        [HttpPost]
        public IActionResult Edit(Member member, IFormFile? file)
        {
            string wwwRootPath = _webHostEnvironment.WebRootPath;

            if (file != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                string productPath = Path.Combine(wwwRootPath, @"Images\Member");

                if (!string.IsNullOrEmpty(member.ImageUrl))
                {
                    var oldImage = Path.Combine(wwwRootPath, member.ImageUrl.TrimStart('\\'));

                    if (System.IO.File.Exists(oldImage))
                    {
                        System.IO.File.Delete(oldImage);
                    }
                }

                using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                member.ImageUrl = @"\Images\Member\" + fileName;
            }

            _db.Members.Update(member);
            TempData["success"] = "Member updated successfuly!";
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }


        //GET
        public IActionResult Delete(int id)
        {
            Member memberFromDb = _db.Members.FirstOrDefault(m => id == m.Id);

            return View(memberFromDb);
        }


        [HttpPost]
        public IActionResult Delete(int? id)
        {
            Member memberFromDb = _db.Members.FirstOrDefault(m => id == m.Id);
            _db.Members.Remove(memberFromDb);
            TempData["success"] = "Member deleted";
            _db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
    }
}
