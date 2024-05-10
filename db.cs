using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace TO_DO_LIST
{
    class db : DbContext
    {
        public db() : base("data source=LAPTOP-H65DAKUS\\SQLEXPRESS; initial catalog=   ; integrated security=true ; Trusted_Connection=True;Encrypt=False; MultipleActiveResultSets= true")
        {
        }

        public DbSet<Datacomparer> Datacomparers { get; set; }
        public DbSet<TodoItem> Todoitems { get; set; }
    }
}
