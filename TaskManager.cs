/// Philip Rönnmark 990513-4392 2021-12-12
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A6
{
    /// <summary>
    /// class to store tasks
    /// </summary>
    class TaskManager
    {
        private List<Task> tasks = new List<Task>();

        public  void AddTask(Task t)
        {
            tasks.Add(t);
        }

        public  List<Task> GetTasks()
        {
            return tasks;
        }

        public  Task GetTask(int i)
        {
            return tasks[i];
        }

        public  void deleteTask(int i)
        {
            tasks.RemoveAt(i);
        }

        public  void resetTasks()
        { tasks = new List<Task>(); }

    }
}
