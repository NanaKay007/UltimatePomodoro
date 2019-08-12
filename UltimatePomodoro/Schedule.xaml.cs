﻿using System;
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
        public DaySchedule CurrentTasks;
        
        public string calendarPos = "";

        public Schedule()
        {
            this.InitializeComponent();
        }

        private void Scheduler_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            try
            {
                
                var selected = Scheduler.SelectedDates.First().DateTime;
                NewTaskButton.Visibility = Visibility.Visible;
                calendarPos = String.Format("{0}:{1}:{2}", selected.Day, selected.Month, selected.Year);
                try
                {
                    CurrentTasks = TaskManager.DailyTasks[calendarPos];
                    TaskCards.ItemsSource = CurrentTasks.tasks;
                }
                catch
                {
                    CurrentTasks = null;
                    TaskCards.ItemsSource = null;
                }
                Date.Text = "Tasks for " + calendarPos;
            }
            catch
            {
                Date.Text = "Please select a day on the calendar";
                NewTaskButton.Visibility = Visibility.Collapsed;
            }
            
            
        }

        private void CreateTask_Click(object sender, RoutedEventArgs e)
        {
            string id = Guid.NewGuid().ToString();

            Task task = new Task { Title = Title.Text, Description = Description.Text ,id = id};
            task.setTags(Tags.Text);

            try
            {
                CurrentTasks = TaskManager.DailyTasks[calendarPos];
            }
            catch
            {
                DaySchedule newSchedule = new DaySchedule { date = calendarPos };
                
                TaskManager.DailyTasks[calendarPos] = newSchedule;
                CurrentTasks =  newSchedule;

            }
            
            CurrentTasks.AddTask(task);
            TaskCards.ItemsSource = CurrentTasks.tasks;
            closeForm();
        }

        private void CreateTimer_Click(object sender, RoutedEventArgs e)
        {

            var selected = Scheduler.SelectedDates.First().DateTime;
            var today = DateTime.Today;

            if (selected.Date == today)
            {
                TimeManager timer = new TimeManager();
                Button button_sender = (Button)sender;
                Task task = CurrentTasks.tasks.Where(p => p.id == button_sender.AccessKey).First();
                task.Pomodoro = timer;

                TaskManager.currentTimer = timer;
                TaskManager.currentTask = task;
                var datacontext = (sender as Button).DataContext;
                MainPage mainpage = datacontext as MainPage;
                if (mainpage != null)
                {
                    mainpage.title = "Timer";
                }
                Frame.Navigate(typeof(Timer));
            } else
            {
                Date.Text = "This task is not marked for today; Only today's tasks can be timed";
            }
            

        }

        private void DeleteTask_Click(object sender, RoutedEventArgs e)
        {
            Button button_sender = (Button)sender;
            var selected = Scheduler.SelectedDates; 

            foreach(Task task in CurrentTasks.tasks)
            {
                if (task.id == button_sender.AccessKey)
                {
                    CurrentTasks.tasks.Remove(task);
                    TaskManager.currentTimer = null;
                    break;
                }
            }

        }

        private void NewTaskButton_Click(object sender, RoutedEventArgs e)
        {
            NewTaskButton.Visibility = Visibility.Collapsed;
            NewTaskForm.Visibility = Visibility.Visible;

        }

        private void CloseFormButton_Click(object sender, RoutedEventArgs e)
        {
            closeForm();
        }

        private void closeForm()
        {
            NewTaskForm.Visibility = Visibility.Collapsed;
            Title.Text = "";
            Description.Text = "";
            NewTaskButton.Visibility = Visibility.Visible;
        }
    }


}
