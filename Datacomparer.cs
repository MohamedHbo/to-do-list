using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO_DO_LIST
{
    public class Datacomparer : IComparer<TodoItem>
    {
        public int Id { get; set; } // Primary key

        public int Compare(TodoItem x, TodoItem y)
        {
            return x.Date.CompareTo(y.Date);
        }
    }
}
