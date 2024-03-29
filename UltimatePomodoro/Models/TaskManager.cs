﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UltimatePomodoro.Models
{
    public static class TaskManager
    {
        public static Dictionary<string,DaySchedule> DailyTasks = new Dictionary<string, DaySchedule>();
        public static TimeManager currentTimer;
        public static Task currentTask;
        public static Boolean timerActive = false;
    }

    public class Task : Template10.Validation.ValidatableModelBase
    {
        public string Title { get { return Read<string>(); } set { Write<string>(value); } }
        public string id { get; set; }
        public string Description { get; set; }
        public TimeManager Pomodoro {get; set;}
        public ObservableCollection<string> _tags = new ObservableCollection<string>();
        private string _tags_string;
        public string tags
        {
            get { return Read<string>(); }
            set
            {
                Write<string>(value);
                setTags(tags);
            }
        }
        public void setTags (string text) {
            if (text != "")
            {
                string[] tags_split = text.Split(',');
                foreach (string tag in tags_split)
                {
                    _tags.Add(tag);
                }
            }
            
        }

        
    }

    public class DaySchedule : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged = delegate { };

        public ObservableCollection<Task> tasks = new ObservableCollection<Task>();
        public string date { get; set; }
        public void AddTask(Task task)
        {
            tasks.Add(task);
            this.onPropertyChanged("tasks");
        }
        public void onPropertyChanged([CallerMemberName] string propertyName=null)
        {
            this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
