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
    /// Static class to store tasks
    /// </summary>
    static class TaskManager
    {
        private static List<Task> tasks = new List<Task>();

        public static void AddTask(Task t)
        {
            tasks.Add(t);
        }

        public static List<Task> GetTasks()
        {
            return tasks;
        }

        public static Task GetTask(int i)
        {
            return tasks[i];
        }

        public static void deleteTask(int i)
        {
            tasks.RemoveAt(i);
        }

        public static void resetTasks()
        { tasks = new List<Task>(); }

    }
}
