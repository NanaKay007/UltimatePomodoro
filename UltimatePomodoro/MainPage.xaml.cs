using System;
using System.Collections.Generic;
using System.ComponentModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UltimatePomodoro
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page , INotifyPropertyChanged
    {
        private string _title;

        public event PropertyChangedEventHandler PropertyChanged;

        public string title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("title");
            }
            
        }


        public void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
            
        }

        public MainPage()
        {
            this.InitializeComponent();
            View.Navigate(typeof(TimerView));
            this.DataContext = this;
            title = "Timer";
            MySplitView.SelectedItem = MySplitView.MenuItems.First();
        }

       
        private void Hamburger_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void NavigationItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
        }

        private void NavigationView_SelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
        {
            
        }

        private void MySplitView_ItemInvoked(NavigationView sender, NavigationViewItemInvokedEventArgs args)
        {
            NavigationViewItem navitem = (NavigationViewItem) sender.SelectedItem;

            if (navitem.Name == "Schedule")
            {
                View.Navigate(typeof(Schedule));

            }
            else if (navitem.Name == "Timer")
            {
                View.Navigate(typeof(TimerView));

            }
            else if (navitem.Name == "History")
            {
                View.Navigate(typeof(History));
            }
            else if (navitem.Name == "Profile")
            {
                View.Navigate(typeof(Profile));
            }
            MySplitView.Header = navitem.Name;
        }
    }
}
