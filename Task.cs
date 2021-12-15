/// Philip Rönnmark 990513-4392 2021-12-12
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A6
{
    class Task
    {
        private PriorityType priority;
        private DateTime date;
        private string description;

        public DateTime Date { get => date; set => date = value; }
        public string Description { get => description; set => description = value; }
        internal PriorityType Priority { get => priority; set => priority = value; }

        public override string ToString()
        {   
            return string.Format("den {0} {1} {2} {3} {4}", date.Day.ToString(), date.ToString("MMMM"), date.Year.ToString().PadRight(13), priority.ToString().PadRight(26) , description.PadRight(2));
        }
    }
}
