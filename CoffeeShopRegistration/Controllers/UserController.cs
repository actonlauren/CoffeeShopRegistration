using CoffeeShopRegistration.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Reflection;
using System.Reflection.Metadata;

namespace CoffeeShopRegistration.Controllers
{
    public class UserController : Controller
    {
        // future state, this would be a database
        private IMemoryCache _cache;

        public UserController(IMemoryCache cache)
        {
            _cache = cache;

            if (!_cache.TryGetValue("users", out List<UserViewModel> users))
            {
                users = new List<UserViewModel>();

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(3));

                _cache.Set("users", users, cacheEntryOptions);
            }

            if (!_cache.TryGetValue("ids", out int ids))
            {
                ids = 0;

                var cacheEntryOptions = new MemoryCacheEntryOptions()
                        .SetSlidingExpiration(TimeSpan.FromHours(3));

                _cache.Set("ids", ids, cacheEntryOptions);
            }

            Console.WriteLine("Created in memory");
        }

        public ActionResult List()
        {

            var usersViewModel = _cache.Get("users");

            return View(usersViewModel);
        }

        // GET: UserController/Create
        [HttpGet]
        public ActionResult AddUser()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserViewModel user)
        {
            try
            {
                // simply a placeholder for our database
                if (_cache.TryGetValue("users", out List<UserViewModel> users))
                {
                    if (user.Age < 18)
                    {
                        ModelState.AddModelError("Age", "You must be 18 or older to register");
                        return View("AddUser", user);
                    }
                    if (user.Password.Length < 8)
                    {
                        ModelState.AddModelError("Password", "Password must be at least 8 characters");
                        return View("AddUser", user);
                    }

                    users.Add(user);

                    _cache.Set("ids", user.Email);
                }

                return RedirectToAction(nameof(Welcome), new { email = user.Email });
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Welcome(string email)
        {
            var usersViewModel = _cache.Get("users");
            var userList = usersViewModel as List<UserViewModel>;
            var user = userList?.FirstOrDefault(user => user.Email == email);
            return View(user);
        }


    }
}