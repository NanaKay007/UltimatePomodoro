using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using UltimatePomodoro.Models;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UltimatePomodoro
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Schedule : Page
    {
        public ObservableCollection<Task> CurrentTasks = new ObservableCollection<Task>();
        public string calendarPos = "";

        public Schedule()
        {
            this.InitializeComponent();
            CreateTask.Visibility = Visibility.Collapsed;
            
        }

        private void Scheduler_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            var selected = Scheduler.SelectedDates.First().DateTime;
            calendarPos = String.Format("{0}:{1}:{2}", selected.Day, selected.Month, selected.Year);
            CreateTask.Visibility = Visibility.Visible;
            Date.Text = "Tasks for " + calendarPos;
        }

        private void CreateTask_Click(object sender, RoutedEventArgs e)
        {
            Task task = new Task();
            try
            {
                CurrentTasks = TaskManager.tasks[calendarPos];
            }
            catch
            {
                TaskManager.tasks[calendarPos] = new ObservableCollection<Task>();
                CurrentTasks = TaskManager.tasks[calendarPos];
            }

            CurrentTasks.Add(task);
            

        }

        private void StartTimer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            Button button_sender = (Button)sender;
            var selected = Scheduler.SelectedDates;

            CurrentTasks.RemoveAt(int.Parse(button_sender.AccessKey));

             
            

        }
    }
}
