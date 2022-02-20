using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SQLite_entity_core.Data;
using SQLite_entity_core.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace SQLite_entity_core.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //var user = new User();
            //user.Name = "John"; user.UserName = "johndoe"; user.Email = "johndoe@none.com";

            //using ( var db = new SQLiteContext())
            //{
            //    db.Add(user);
            //    db.SaveChanges();
            //}

            var strDate = DateTime.Now;

            var listUser = new List<User>() {
                new User(){ Name = "aaa", UserName ="aaa" + strDate, Email ="aaa" },
                new User(){ Name = "bbb", UserName ="bbb", Email ="bbb" },
                new User(){ Name = "ccc", UserName ="ccc", Email ="ccc" },
                new User(){ Name = "ddd", UserName ="ddd", Email ="ddd" },
                new User(){ Name = "eee", UserName ="eee", Email ="eee" },
                new User(){ Name = "fff", UserName ="fff", Email ="fff" },
                new User(){ Name = "ggg", UserName ="ggg", Email ="ggg" },
            };


            var userCache = new UserCache();
            userCache.JSON = JsonSerializer.Serialize(listUser);

            using (var db = new SQLiteContext())
            {
                var remove = db.UserCache.Where(x => x.Creation < DateTime.Now.AddMinutes(-5) ).ToList();
                db.UserCache.RemoveRange(remove);
                db.SaveChanges();


                db.Add(userCache);
                db.SaveChanges();
            }

            List<User> listUserDeserialize;

            using (var db = new SQLiteContext())
            {
                var userCache2 = db.UserCache.OrderByDescending(x => x.Creation).FirstOrDefault();
                listUserDeserialize = JsonSerializer.Deserialize<List<User>>(userCache.JSON);

            }



            return View();
        }

        public IActionResult Privacy()
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
