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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UltimatePomodoro
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            View.Navigate(typeof(TimerView));
            Header.Text = "Timer";
            
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
            ListBoxItem selected = (ListBoxItem) NavigationItems.SelectedItem;

            if (NavigationItems.SelectedIndex.ToString() == "1")
            {
                View.Navigate(typeof(Schedule));
                Header.Text = selected.Name;
            } else if (NavigationItems.SelectedIndex.ToString() == "0")
            {
                View.Navigate(typeof(TimerView));
                Header.Text = selected.Name;
            }
        }
    }
}
