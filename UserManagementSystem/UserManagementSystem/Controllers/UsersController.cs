using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Data;
using UserManagementSystem.Models;
using PagedList;

namespace UserManagementSystem.Controllers
{
    public class UsersController : Controller
    {
        private readonly AppDBContext _db;
        public UsersController(AppDBContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Users(string sortOrder, string currentFilter, string searchString, int pageNumber = 1)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FNSortParm = sortOrder == "firstname" ? "firstname_desc" : "firstname";
            ViewBag.LNSortParm = sortOrder == "lastname" ? "lastname_desc" : "lastname";
            ViewBag.USortParm = sortOrder == "username" ? "username_desc" : "username";
            ViewBag.PSortParm = sortOrder == "password" ? "password_desc" : "password";
            ViewBag.ESortParm = sortOrder == "email" ? "email_desc" : "email";
            ViewBag.SSortParm = sortOrder == "status" ? "status_desc" : "status";

            ViewBag.CurrentFilter = searchString;

            var users = from s in _db.Users
                        select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                users = users.Where(x => x.LastName.Contains(searchString)
                                       || x.FirstName.Contains(searchString)
                                       || x.Username.Contains(searchString)
                                       || x.Password.Contains(searchString)
                                       || x.Email.Contains(searchString)
                                       || x.Status.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "lastname_desc":
                    users = users.OrderByDescending(x => x.LastName);
                    break;
                case "firstname":
                    users = users.OrderBy(x => x.FirstName);
                    break;
                case "firstname_desc":
                    users = users.OrderByDescending(x => x.FirstName);
                    break;
                case "username":
                    users = users.OrderBy(x => x.Username);
                    break;
                case "username_desc":
                    users = users.OrderByDescending(x => x.Username);
                    break;
                case "password":
                    users = users.OrderBy(x => x.Password);
                    break;
                case "password_desc":
                    users = users.OrderByDescending(x => x.Password);
                    break;
                case "email":
                    users = users.OrderBy(x => x.Email);
                    break;
                case "email_desc":
                    users = users.OrderByDescending(x => x.Email);
                    break;
                case "status":
                    users = users.OrderBy(x => x.Status);
                    break;
                case "status_desc":
                    users = users.OrderByDescending(x => x.Status);
                    break;
                default: 
                    users = users.OrderBy(s => s.LastName);
                    break;
            }
            return View(await PagingList<Users>.CreateAsync(users, pageNumber,10));
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Users obj)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Users");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Users.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Users obj)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Users");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            var obj = _db.Users.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Users.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Users");
        }
        public IActionResult Permissions(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var obj = _db.Users.Find(id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Permissions(Users obj)
        {
            if (ModelState.IsValid)
            {
                _db.Users.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Users");
            }
            else
            {
                var errors = ModelState.Where(x => x.Value.Errors.Any())
                .Select(x => new { x.Key, x.Value.Errors });
            }
            return View(obj);
        }
    }
}
