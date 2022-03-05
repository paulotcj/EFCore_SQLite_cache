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
        private readonly SQLiteContext _context;

        public HomeController(ILogger<HomeController> logger, SQLiteContext context)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {


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

            //_context.Database.EnsureCreated();
            //_context.SaveChanges();


            var remove = _context.UserCache.Where(x => x.Creation < DateTime.Now.AddMinutes(-5)).ToList();
            _context.UserCache.RemoveRange(remove);
            _context.SaveChanges();

            _context.Add(userCache);
            _context.SaveChanges();


            List<User> listUserDeserialize;

            var userCache2 = _context.UserCache.OrderByDescending(x => x.Creation).FirstOrDefault();
            listUserDeserialize = JsonSerializer.Deserialize<List<User>>(userCache.JSON);




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