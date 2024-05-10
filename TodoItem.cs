using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TO_DO_LIST
{
    [Serializable]
    public class TodoItem
    {
        public int Id { get; set; } // Primary key
        public string Name { get; set; }
        public string Notes { get; set; }
        public DateTime Time { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return $"{Name} - {Notes} - {Time.ToShortTimeString()} - {Date.ToShortDateString()} ";
        }
    }
}
