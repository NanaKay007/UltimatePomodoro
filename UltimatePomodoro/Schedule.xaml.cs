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
using Windows.UI.Popups;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UltimatePomodoro
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Schedule : Page
    {
        public DaySchedule CurrentTasks;
        public DateTime selected;
        public DateTime today;
        public string calendarPos = "";

        public Schedule()
        {
            this.InitializeComponent();
            today = DateTime.Today;
        }

        private void Scheduler_SelectedDatesChanged(CalendarView sender, CalendarViewSelectedDatesChangedEventArgs args)
        {
            
            try
            {
                selected = Scheduler.SelectedDates.First().DateTime;
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
            if (selected >= today)
            {
                string id = Guid.NewGuid().ToString();


                Task task = new Task {
                    Title = Title.Text,
                    Description = Description.Text,
                    id = id,
                    tags = Tags.Text,
                    Validator = i =>
                    {
                        var t = i as Task;
                        if (string.IsNullOrEmpty(t.Title) || string.IsNullOrWhiteSpace(t.Title))
                        {
                            t.Properties[nameof(t.Title)].Errors.Add("Title is required");
                        }

                        if (string.IsNullOrEmpty(t.tags) || string.IsNullOrWhiteSpace(t.Title))
                        {
                            t.Properties[nameof(t.tags)].Errors.Add("Tags are required to allow tracking of a timed activity");
                        }
                    }
                };

                if (task.Validate())
                {
                    try
                    {
                        CurrentTasks = TaskManager.DailyTasks[calendarPos];
                    }
                    catch
                    {
                        DaySchedule newSchedule = new DaySchedule { date = calendarPos };

                        TaskManager.DailyTasks[calendarPos] = newSchedule;
                        CurrentTasks = newSchedule;

                    }

                    CurrentTasks.AddTask(task);
                    TaskCards.ItemsSource = CurrentTasks.tasks;
                    closeForm();
                }
                
            } else
            {
                
                MessageDialog dialog = new MessageDialog("Creating tasks before today is not allowed");
                dialog.Title = "Not allowed";
                _ = dialog.ShowAsync();
            }

            
        }

        private void CreateTimer_Click(object sender, RoutedEventArgs e)
        {
            if (selected.Date == today)
            {
                Button button_sender = (Button)sender;
                if (TaskManager.currentTask == null)
                {
                    CreateTimer(button_sender);
                } else
                {
                    MessageDialog dialog = new MessageDialog("A timer is currently running. Are you sure you want to replace it with this task's timer?");
                    dialog.Title = "A current timer exists";
                    var ok = new UICommand("OK", cmd => { CreateTimer(button_sender); }) ;
                    var cancel = new UICommand("Cancel");
                    dialog.Commands.Add(ok);
                    dialog.Commands.Add(cancel);
                    dialog.CancelCommandIndex = 1;
                    _ = dialog.ShowAsync();
                }
                
            } else
            {
                MessageDialog dialog = new MessageDialog("This task is not marked for today; Only today's tasks can be timed");
                dialog.Title = "Not allowed";
                _ = dialog.ShowAsync();

            }
            

        }

        private void CreateTimer(Button button_sender)
        {
            TimeManager timer = new TimeManager();
            
            Task task = CurrentTasks.tasks.Where(p => p.id == button_sender.AccessKey).First();
            task.Pomodoro = timer;

            TaskManager.currentTimer = timer;
            TaskManager.currentTask = task;
            
            Frame.Navigate(typeof(Timer));
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

        private void TaskCard_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {

        }
    }


}
