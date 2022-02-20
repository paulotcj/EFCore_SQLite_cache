using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SQLite_entity_core.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }

    public class UserCache
    {
        public int ID { get; set; }

        public string JSON { get; set; }

        public DateTime Creation { get; set; } = DateTime.Now;
    }
}
