using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UltimatePomodoro.Models
{
    public static class TaskManager
    {
        public static Dictionary<string, ObservableCollection<Task>> tasks = new Dictionary<string, ObservableCollection<Task>>();

        
    }

    public class Task
    {
        public string Title { get; set; }
        public int id = TaskManager.tasks.Count;
        public string Description { get; set; }
        public TimeManager Pomodoro {get; set;}

    }
}
