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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UltimatePomodoro
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProjectView : Page
    {
        public Project project;

        public ProjectView()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            
            project = TaskManager.projects.Where(p => p.id == e.Parameter.ToString()).First();
            ProjectName.Text = project.title;
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void NewTaskClick(object sender, RoutedEventArgs e)
        {
            NewTaskForm.Visibility = Visibility.Visible;
            tasks.Visibility = Visibility.Collapsed;
        }

        private void CreateTask_Click(object sender, RoutedEventArgs e)
        {
            string date = String.Format("{0},{1},{2}", datePicker.Date.Day, datePicker.Date.Month, datePicker.Date.Year);
            Task task = new Task { Title = TaskTitle.Text, tags = Tags.Text, Description = TaskDescription.Text, date = date };
            project.tasks.Add(task);
            NewTaskForm.Visibility = Visibility.Collapsed;
            tasks.Visibility = Visibility.Visible;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            NewTaskForm.Visibility = Visibility.Collapsed;
            tasks.Visibility = Visibility.Visible;
            TaskTitle.Text = "";
            TaskDescription.Text = "";
            Tags.Text = "";
            
        }

        private void StartTimer_Click(object sender, RoutedEventArgs e)
        {
            TimeManager timer = new TimeManager();
            Button task = (Button)sender;
            Task current_task = project.tasks.Where(p => p.id == task.AccessKey.ToString()).First();
            current_task.Pomodoro = timer;
            TaskManager.currentTimer = current_task.Pomodoro;
            TaskManager.currentTask = current_task;
            
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            TimeManager timer = new TimeManager();
            Button task = (Button)sender;
            Task current_task = project.tasks.Where(p => p.id == task.AccessKey.ToString()).First();
            project.tasks.Remove(current_task);
            TaskManager.currentTimer = null;
            TaskManager.currentTask = null;
        }
    }
}
