using Microsoft.EntityFrameworkCore;
using SQLite_entity_core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SQLite_entity_core.Data
{
    public class SQLiteContext : DbContext
    {

        //------------

        public SQLiteContext(DbContextOptions options) : base(options)
        {
            this.Database.EnsureCreated();
            this.SaveChanges();
        }
        protected SQLiteContext()
        {
        }

        //------------


        public DbSet<User> User { get; set; }
        public DbSet<UserCache> UserCache { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlite(@"Data Source=.\\SQLiteDB.db");
    }
}
