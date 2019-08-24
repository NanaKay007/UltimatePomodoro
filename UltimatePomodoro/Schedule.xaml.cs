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
        public Project project;
        public ObservableCollection<Project> projects;
        public DateTime today;
        public string calendarPos = "";

        public Schedule()
        {
            this.InitializeComponent();
            today = DateTime.Today;
            projects = TaskManager.projects;
            if (projects.Count() > 0)
            {
                project = TaskManager.projects.First();
                ProjectName.Text = project.title;
            }
        }


        private void CreateTask_Click(object sender, RoutedEventArgs e)
        {
            string date = String.Format("{0},{1},{2}", datePicker.Date.Day, datePicker.Date.Month, datePicker.Date.Year);
            

            if (datePicker.Date.Date >= today)
            {
                
                Task task = new Task
                {
                    Title = TaskTitle.Text,
                    Description = TaskDescription.Text,
                    tags = Tags.Text,
                    date = date,
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
                    if(project != null)
                    {
                        project.tasks.Add(task);
                    } else
                    {
                        project = new Project { title = "Unsorted Tasks" };
                        projects.Add(project);
                        ProjectName.Text = project.title;
                        project.tasks.Add(task);
                        tasks.ItemsSource = project.tasks;
                    }
                    
                    closeForm();
                }

            }
            else
            {

                MessageDialog dialog = new MessageDialog("Creating tasks before today is not allowed");
                dialog.Title = "Not allowed";
                _ = dialog.ShowAsync();
            }


        }

        private void StartTimer_Click(object sender, RoutedEventArgs e)
        {
            if (datePicker.Date.Date == today)
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
            Button task = (Button)button_sender;
            Task current_task = project.tasks.Where(p => p.id == task.AccessKey.ToString()).First();
            current_task.Pomodoro = timer;
            TaskManager.currentTimer = current_task.Pomodoro;
            TaskManager.currentTask = current_task;

            Frame.Navigate(typeof(Timer));
        }

        private void closeForm()
        {
            NewTaskForm.Visibility = Visibility.Collapsed;
            tasks.Visibility = Visibility.Visible;
        }

        private void NewProject_Click(object sender, RoutedEventArgs e)
        {
            ProjectForm.Visibility = Visibility.Visible;
        }

        private void AddProject_Click(object sender, RoutedEventArgs e)
        {
            Project new_project = new Project { title = ProjectTitle.Text };
            projects.Add(new_project);
            if (project == null)
            {
                project = new_project;
                tasks.ItemsSource = project.tasks;
                ProjectName.Text = project.title;
            }
            
            ProjectForm.Visibility = Visibility.Collapsed;

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ProjectForm.Visibility = Visibility.Collapsed;
            ProjectTitle.Text = "";
        }

        private void ProjectTapped(object sender, TappedRoutedEventArgs e)
        {
            ListViewItem project_item = (ListViewItem)sender;
            project = TaskManager.projects.Where(p => p.id == project_item.AccessKey.ToString()).First();
            ProjectName.Text = project.title;
            tasks.ItemsSource = project.tasks;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            TimeManager timer = new TimeManager();
            Button task = (Button)sender;
            Task current_task = project.tasks.Where(p => p.id == task.AccessKey.ToString()).First();
            if (current_task.id == TaskManager.currentTask.id)
            {
                TaskManager.currentTimer = null;
                TaskManager.currentTask = null;
            }
                
            project.tasks.Remove(current_task);
        }

        private void NewTaskClick(object sender, RoutedEventArgs e)
        {
            NewTaskForm.Visibility = Visibility.Visible;
            tasks.Visibility = Visibility.Collapsed;
        }

        private void DeleteProject(object sender, RoutedEventArgs e)
        {

        }

        private void CancelTaskForm_Click(object sender, RoutedEventArgs e)
        {
            NewTaskForm.Visibility = Visibility.Collapsed;
            tasks.Visibility = Visibility.Visible;
            TaskTitle.Text = "";
            TaskDescription.Text = "";
            Tags.Text = "";
        }
    }


}
