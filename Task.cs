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

        public DateTime Date
        {
            get
            { 
                return date; 
            }

            set 
            {
                if(value.GetType() == date.GetType())
                {
                    date = value;
                }
                
            }
        }
        public string Description {
            get
            {
                return description;
            }
            set
            {
                description = value;
            }
        }
        internal PriorityType Priority
        {
            get
            {
                return priority;
            }
            set 
            { 
                priority = value; 
            }
        }

        public override string ToString()
        {   
            return string.Format("den {0} {1} {2} {3} {4}", Date.Day.ToString(), Date.ToString("MMMM"), Date.Year.ToString().PadRight(13), Priority.ToString().PadRight(26) , Description.PadRight(2));
        }
    }
}
